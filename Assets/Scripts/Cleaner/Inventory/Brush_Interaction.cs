using System;
using System.Collections;
using UnityEngine;

public class Brush_Interaction : MonoBehaviour, IInteractable
{

	[SerializeField] private float m_MeleeRange;
	[SerializeField] private float m_MeleeForce;
	[SerializeField] private float m_MeleeForceHeight;
	[SerializeField] private int m_LayerMask;

	[Header("Camera")]
	[Space]

	[SerializeField] private Camera m_Camera;

	[Header("Cooldown reference")]
	[Space]

	[SerializeField] private IventoryItemCooldowns m_ItemCooldown;

	public void Awake()
	{
		m_ItemCooldown = gameObject.GetComponentInParent<IventoryItemCooldowns>();
		m_ItemCooldown.m_CanBash = true;
		m_LayerMask = 1 << 6;
	}

	public void Interact()
	{
		if (m_ItemCooldown.m_CanBash)
		{
			Bash();
		}
	}

	public void Bash()
	{
		m_ItemCooldown.m_CanBash = false;

		//TODO: Perform Bash

		RaycastHit hit;
		if(Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_MeleeRange, m_LayerMask))
		{
			Debug.DrawRay(m_Camera.transform.position, m_Camera.transform.forward, Color.green);

			if(hit.rigidbody != null)
			{

				//If stunnable object hit
				IStunnable stunnable = hit.collider.GetComponent<IStunnable>();
				if (stunnable != null && m_ItemCooldown.m_CanStun)
				{
					stunnable.Stun();
					AddForce(hit, stunnable);
					m_ItemCooldown.m_CanStun = false;
					m_ItemCooldown.CoolStun();
				}

				AddForce(hit, stunnable);
			}
		}
		
		m_ItemCooldown.CoolBash();
	}

	// If it is the cat then can you stun it?
	// or if it has no stunnable interface i.e. basic objects
	public void AddForce(RaycastHit hit, IStunnable stunnable)
	{
		if (m_ItemCooldown.m_CanStun || stunnable == null)
		{
			hit.rigidbody.AddForceAtPosition(
				new Vector3(hit.collider.transform.position.x - m_Camera.transform.position.x,
				m_MeleeForceHeight, hit.collider.transform.position.z - m_Camera.transform.position.z) * m_MeleeForce,
				hit.point);
		}
	}
}
