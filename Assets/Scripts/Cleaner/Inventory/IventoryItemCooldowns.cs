using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IventoryItemCooldowns : MonoBehaviour
{
	[Header("Brush Cooldowns")]
	[Space]
	public bool m_CanBash;
	public bool m_CanStun;

	[Header("Bottle Cooldowns")]
	[Space]
	public bool m_CanFire;

	public Coroutine c_BashCooldown;
	public Coroutine c_StunCooldown;
	public Coroutine c_FireCooldown;

	// ------------------------------------ //
	// The point of this script is to keep the timers going when you switch weapons and //
	// the object previously held gets disabled //
	// ------------------------------------ //

	void Awake()
	{
		m_CanStun = true;
	}

	#region Brush Cooldowns
	public void CoolBash()
	{
		if(c_BashCooldown == null)
		{
			c_BashCooldown = StartCoroutine(c_CoolingBash());
		}
	}

	public void CoolStun()
	{
		if (c_StunCooldown == null)
		{
			c_StunCooldown = StartCoroutine(c_CoolingStun());
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

	#endregion

	#region Spray Bottle Cooldowns

	public void CoolFire()
	{
		if(c_FireCooldown == null)
		{
			c_FireCooldown = StartCoroutine(FireCoolingDown());
		}
	}

	IEnumerator FireCoolingDown()
	{
		yield return new WaitForSeconds(0.7f);
		m_CanFire = true;
		c_FireCooldown = null;
	}
	#endregion
}
