using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CatVomit : MonoBehaviour, IVomitable
{
	[Header("Gamemode")]
	[Space]

	[SerializeField] private GameManager m_GameManager;

	[Header("Vomit Charge Specific")]
	[Space]

	private float m_VomitChargeTime;
	[SerializeField] private float m_VomitChargeMax = 5.0f;
	[SerializeField] private float m_ChargeTimeMultiplier;

	[Header("Vomit Projectile Specific")]
	[Space]

	[SerializeField] private int m_VomitAmmo;
	[SerializeField] private GameObject m_VomitProjectilePrefab;

	[SerializeField] private Transform m_FirePos;
	[SerializeField] private Transform m_Camera;
	[SerializeField] private float m_ProjectileSpeed;

	Coroutine c_ChargeBarTimer;
	Coroutine c_CatVomitBegin;

	void Awake()
	{
		m_ChargeTimeMultiplier = 1.0f;

		GameObject managerObject = GameObject.FindWithTag("GameController");

		m_GameManager = managerObject.GetComponent<GameManager>();
	}

	public void VomitStart()
	{
		Debug.Log("Vomit Begin");
		m_GameManager.ShowVomitChargeBar();
		//Resettting charge value
		m_VomitChargeTime = m_VomitChargeMax;

		if (c_ChargeBarTimer == null)
		{
			c_ChargeBarTimer = StartCoroutine(c_ChargingBar());
		}
	}

	public void SlowCharge(InputAction.CallbackContext context)
	{
		if(context.performed)
		{
			m_ChargeTimeMultiplier = 0.3f;
		}
		
		if (context.canceled)
		{
			m_ChargeTimeMultiplier = 1.0f;
		}
	}

	IEnumerator c_ChargingBar()
	{
		while (m_VomitChargeTime >= 0)
		{
			yield return new WaitForEndOfFrame();
			
			//Decreasing timer
			m_VomitChargeTime -= Time.deltaTime * m_ChargeTimeMultiplier;

			//Calculating percentage value to change the styles to
			float chargePercentage = (m_VomitChargeTime / m_VomitChargeMax) * 100;

			//Updating Charge bar in game manager
			m_GameManager.UpdateVomitChargeBar(chargePercentage);

		}

		c_ChargeBarTimer = null;

		//Begin actual vomit ability 
		while (c_CatVomitBegin == null)
		{
			m_VomitAmmo = 30;
			c_CatVomitBegin = StartCoroutine(c_CatVomiting());
		}
	}

	IEnumerator c_CatVomiting()
	{
		while (m_VomitAmmo > 0)
		{

			// Instantiating projectile
			for (int i = 0; i <= 3; i++)
			{
				GameObject vomitProjectile = Instantiate(m_VomitProjectilePrefab, m_FirePos.position, Quaternion.identity);

				Rigidbody projectileRB = vomitProjectile.GetComponent<Rigidbody>();

				vomitProjectile.transform.forward = m_Camera.forward;

				//Pitch
				float pitchOffset = Random.Range(-15.0f, 15.0f);
				//Yaw
				float yawOffset = Random.Range(-15.0f, 15.0f);

				//Rotating projectile for bullet spread effect
				vomitProjectile.transform.Rotate(pitchOffset, yawOffset, 0, 0);

				projectileRB.velocity = vomitProjectile.transform.forward * m_ProjectileSpeed;
			}

			m_VomitAmmo--;
			yield return new WaitForSeconds(0.05f);
		}
		
		c_CatVomitBegin = null;
	}
}
