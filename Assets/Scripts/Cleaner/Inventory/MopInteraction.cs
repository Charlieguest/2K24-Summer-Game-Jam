using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopInteraction : MonoBehaviour, IInteractable
{
	[Header("Mop Specific")]
	[Space]
	[SerializeField] private float m_MeleeRange;
	[SerializeField] private int m_LayerMask;

	[SerializeField] private GameObject m_Camera;


	[SerializeField] private IventoryItemCooldowns m_ItemCooldown;

	public void Awake()
	{
		m_ItemCooldown = gameObject.GetComponentInParent<IventoryItemCooldowns>();
	}

	public void Interact()
	{
		if (m_ItemCooldown.m_CanMop == true)
		{
			Mop();
		}
	}

	public void Mop()
	{
		m_ItemCooldown.m_CanMop = false;
		
		RaycastHit hit;

		if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_MeleeRange, m_LayerMask))
		{
			RaycastHit[] sphereHit;

			sphereHit = Physics.SphereCastAll(hit.point, 0.5f, m_Camera.transform.forward, 0.01f, m_LayerMask, QueryTriggerInteraction.UseGlobal);

		}

		m_ItemCooldown.CoolMop();
	}
}
