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

	Coroutine c_FireCooldown;

	public void Interact()
	{
		GameObject Projectile = Instantiate(m_DropletProjectile, transform.position, Quaternion.identity);
	}
}
