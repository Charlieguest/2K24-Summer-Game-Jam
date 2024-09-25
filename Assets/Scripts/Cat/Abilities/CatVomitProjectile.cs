using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatVomitProjectile : MonoBehaviour
{
	[Header("Collision Checking")]
	[Space]
	[SerializeField] private int[] m_LayersToIgnore;
	[SerializeField] private LayerMask m_Mask;
	[SerializeField] private GameObject m_VomitDecal;

	void Update()
	{
		RaycastHit hit;
		//Contantly checking collision of projectile using casts
		//Done this way to get the hit point to spawn decal at
		if (Physics.SphereCast(transform.position, transform.localScale.x / 2, transform.forward, out hit, 1.0f, m_Mask, QueryTriggerInteraction.UseGlobal))
		{
			
			GameObject vomitDecal = Instantiate(m_VomitDecal, hit.point, Quaternion.identity);

			//Setting the look rotation for the decal to be the negative normal of the hit object
			//e.g the opposite of the hit object's side's facing direction.
			Quaternion lookRoation = Quaternion.LookRotation(-hit.normal);

			//This way it's much less likely for the decals appearance to be skewed.
			vomitDecal.transform.rotation = lookRoation;

			Destroy(gameObject);
		}
	}

	void OnDrawGizmos()
	{
		// Draw a green sphere at the transform's position
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, transform.localScale.x / 2);
	}
}
