using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class MopInteraction : MonoBehaviour, IInteractable
{
	[Header("Mop Specific")]
	[Space]
	[SerializeField] private float m_MeleeRange;
	[SerializeField] private LayerMask m_LayerMask;

	[SerializeField] private GameObject m_Camera;


	[SerializeField] private IventoryItemCooldowns m_ItemCooldown;

	public void Awake()
	{
		m_ItemCooldown = gameObject.GetComponentInParent<IventoryItemCooldowns>();
		m_ItemCooldown.m_CanMop = true;
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

		// Completing the actual mop function

		if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_MeleeRange))
		{
			//Stor an array for all splats about to be hit with sphere trace
			RaycastHit[] sphereHit;

			sphereHit = Physics.SphereCastAll(hit.point, 0.75f, m_Camera.transform.forward, 0.01f, m_LayerMask, QueryTriggerInteraction.UseGlobal);

			Debug.Log(sphereHit.Length + " splats found");

			foreach (RaycastHit splat in sphereHit)
			{
				//Getting and modifying the components of each splat
				DecalProjector decalProjector = splat.collider.GetComponent<DecalProjector>();
				BoxCollider decalTrigger = splat.collider.GetComponent<BoxCollider>();
				if (decalProjector != null)
				{
					//changing the size and opacity 
					decalProjector.size = new Vector3(decalProjector.size.x * 0.75f, decalProjector.size.y * 0.75f, 1.0f);
					decalTrigger.size = new Vector3(decalTrigger.size.x * 0.5f, decalTrigger.size.y * 0.5f, 1.0f);

					decalProjector.fadeFactor -= 0.3f;

					// If the opacity is low enough, just delete the decal
					if (decalProjector.fadeFactor <= 0.3f)
					{
						Destroy(splat.collider.transform.parent.gameObject);
					}
				}
			}
		}

		m_ItemCooldown.CoolMop();
	}
}
