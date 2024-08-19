using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBottle_Interaction : MonoBehaviour, IInteractable
{
	[Header("Bottle Shoot Variables")]
	[Space]

	[SerializeField] private float m_DropletSpeed;
	[SerializeField] private GameObject m_DropletProjectile;

	[Header("Fire Position")]
	[Space]

	[SerializeField] private Transform m_FirePos;

	[Header("Cooldown reference")]
	[Space]

	[SerializeField] private IventoryItemCooldowns m_ItemCooldown;


	public void Awake()
	{
		m_ItemCooldown = gameObject.GetComponentInParent<IventoryItemCooldowns>();
		m_ItemCooldown.m_CanFire = true;
	}

	public void Interact()
	{
		if(m_ItemCooldown.m_CanFire)
		{
			GameObject projectile = Instantiate(m_DropletProjectile, transform.position, Quaternion.identity);

			Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

			projectileRB.AddForce(m_FirePos.forward * m_DropletSpeed);

			m_ItemCooldown.m_CanFire = false;
			m_ItemCooldown.CoolFire();
		}
	}
}
