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

	[SerializeField] private float m_FireRate;
	[SerializeField] private bool m_CanFire;

	[Header("Fire Position")]
	[Space]

	[SerializeField] private Transform m_FirePos;

	Coroutine c_FireCooldown;


	public void Awake()
	{
		m_CanFire = true;
	}

	public void Interact()
	{
		if(m_CanFire)
		{
			GameObject projectile = Instantiate(m_DropletProjectile, transform.position, Quaternion.identity);

			Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

			projectileRB.AddForce(m_FirePos.forward * m_DropletSpeed);

			m_CanFire = false;
			if(c_FireCooldown == null)
			{
				c_FireCooldown = StartCoroutine(FireCoolingDown());
			}
		}
	}

	//Adding fire to spray bottle
	IEnumerator FireCoolingDown()
	{
		yield return new WaitForSeconds(m_FireRate);

		m_CanFire = true;
		c_FireCooldown = null;
	}
}
