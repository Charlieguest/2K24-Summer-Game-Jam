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
	[SerializeField] private GameObject m_DropletProjectileDud;

	[SerializeField] private DropletProjectile m_DropletProjectileScript;

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
			GameObject projectile;
			if (m_ItemCooldown.m_CanStun == true)
			{
				projectile = Instantiate(m_DropletProjectile, transform.position, Quaternion.identity);

				//Getting droplet script on projectile
				//Then adding listener to Droplet event
				m_DropletProjectileScript = projectile.GetComponent<DropletProjectile>();
				m_DropletProjectileScript.onProjecitleStun += StartProjecileCooldown;

			}
			// If stun is on cooldown just fire duds
			else
            {
				projectile = Instantiate(m_DropletProjectileDud, transform.position, Quaternion.identity);
				Debug.Log("Firing Duds");
			}
            
			Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

			projectileRB.velocity = m_FirePos.forward * m_DropletSpeed;

			m_ItemCooldown.m_CanFire = false;
			m_ItemCooldown.CoolFire();
		}
	}

	// Making it so we can't stun the cat until
	// our universal stun timer has completed
	public void StartProjecileCooldown()
	{
		m_ItemCooldown.m_CanStun = false;
		m_ItemCooldown.CoolStun();
	}
}
