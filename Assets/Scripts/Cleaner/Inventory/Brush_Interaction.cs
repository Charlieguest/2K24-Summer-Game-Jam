using System;
using System.Collections;
using UnityEngine;

public class Brush_Interaction : MonoBehaviour, IInteractable
{
	[Header("Brush Variables")]
	[Space]

	[SerializeField] private bool m_CanBash;
	[SerializeField] private float m_MeleeRange;
	[SerializeField] private float m_MeleeForce;
	[SerializeField] private int m_LayerMask;

	[Header("Camera")]
	[Space]

	[SerializeField] private Camera m_Camera;

	Coroutine c_BashCooldown;

	public void Awake()
	{
		m_CanBash = true; 
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
				if (stunnable != null)
				{
					stunnable.Stun();
				}

				hit.rigidbody.AddForceAtPosition(m_Camera.transform.forward * m_MeleeForce, hit.point);
			}
		}
		
		if (c_BashCooldown == null)
		{
			c_BashCooldown = StartCoroutine(c_CoolingBash());
		}
	}

	IEnumerator c_CoolingBash()
	{
		yield return new WaitForSeconds(0.5f);
		m_CanBash = true;
		c_BashCooldown = null;
	}
}
