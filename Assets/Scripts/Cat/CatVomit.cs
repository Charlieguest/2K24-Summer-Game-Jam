using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CatVomit : MonoBehaviour, IVomitable
{
	[Header("Gamemode")]
	[Space]

	[SerializeField] private GameManager m_GameManager;

	[Header("Vomit Specific")]
	[Space]

	[SerializeField] private float m_VomitChargeTime;
	[SerializeField] private float m_VomitChargeMax = 5.0f;
	[SerializeField] private float m_ChargeTimeMultiplier;

	Coroutine c_ChargeBarTimer;

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
			Debug.Log("Slow Performed");
			m_ChargeTimeMultiplier = 0.3f;
		}
		
		if (context.canceled)
		{
			Debug.Log("Slow Cancelled");
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
	}
}
