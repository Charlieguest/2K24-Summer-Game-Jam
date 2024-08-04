using System.Collections;
using UnityEngine;

public class Brush_Interaction : MonoBehaviour, IInteractable
{
	[SerializeField] private bool m_CanBash;

	Coroutine c_BashCooldown;

	public void Awake()
	{
		m_CanBash = true; 
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
		Debug.Log("Bash");
		
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
