using System;
using System.Collections;
using UnityEngine;

public class Brush_Interaction : MonoBehaviour, IInteractable
{
	[Header("Brush Variables")]
	[Space]

	[SerializeField] private bool m_CanBash;
	[SerializeField] private bool m_CanStun;

	[SerializeField] private float m_MeleeRange;
	[SerializeField] private float m_MeleeForce;
	[SerializeField] private float m_MeleeForceHeight;
	[SerializeField] private int m_LayerMask;

	[Header("Camera")]
	[Space]

	[SerializeField] private Camera m_Camera;

	Coroutine c_BashCooldown;
	Coroutine c_StunCooldown;

	public void Awake()
	{
		m_CanBash = true;
		m_CanStun = true;
		m_LayerMask = 1 << 6;
	}

	public void Interact()
	{
		if (m_CanBash)
		{
			Bash();
		}
	}

	public void Bash()
	{
		m_CanBash = false;

		//TODO: Perform Bash

		RaycastHit hit;
		if(Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_MeleeRange, m_LayerMask))
		{
			Debug.DrawRay(m_Camera.transform.position, m_Camera.transform.forward, Color.green);

			if(hit.rigidbody != null)
			{

				//If stunnable object hit
				IStunnable stunnable = hit.collider.GetComponent<IStunnable>();
				if (stunnable != null && m_CanStun)
				{
					stunnable.Stun();
					AddForce(hit, stunnable);
					m_CanStun = false;
					c_StunCooldown = StartCoroutine(c_CoolingStun());
				}

				AddForce(hit, stunnable);
			}
		}
		
		if (c_BashCooldown == null)
		{
			c_BashCooldown = StartCoroutine(c_CoolingBash());
		}
	}

	public void AddForce(RaycastHit hit, IStunnable stunnable)
	{
		if (m_CanStun || stunnable == null)
		{
			hit.rigidbody.AddForceAtPosition(
				new Vector3(hit.collider.transform.position.x - m_Camera.transform.position.x,
				m_MeleeForceHeight, hit.collider.transform.position.z - m_Camera.transform.position.z) * m_MeleeForce,
				hit.point);
		}
	}

	IEnumerator c_CoolingBash()
	{
		yield return new WaitForSeconds(0.5f);
		m_CanBash = true;
		c_BashCooldown = null;
	}

	IEnumerator c_CoolingStun()
	{
		yield return new WaitForSeconds(8.0f);
		m_CanStun = true;
		c_StunCooldown = null;
	}
}
