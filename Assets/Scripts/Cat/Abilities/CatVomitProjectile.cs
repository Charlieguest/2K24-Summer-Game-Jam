using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.HighDefinition;
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
			
			GameObject vomitDecalContainer = Instantiate(m_VomitDecal, hit.point, Quaternion.identity);

			//Setting the look rotation for the decal to be the negative normal of the hit object
			//e.g the opposite of the hit object's side's facing direction.
			Quaternion lookRoation = Quaternion.LookRotation(-hit.normal);

			//This way it's much less likely for the decals appearance to be skewed.
			vomitDecalContainer.transform.rotation = lookRoation;

			//Get child decal
			GameObject vomitDecal = vomitDecalContainer.transform.GetChild(0).gameObject;
			
			//Getting decal component to set decal size
			DecalProjector decalProjector = vomitDecal.GetComponent<DecalProjector>();
			BoxCollider decalTrigger = vomitDecal.GetComponent<BoxCollider>();

			float decalSize = Random.Range(0.5f, 1.5f);
			float decalRotation = Random.Range(0.0f, 360.0f);

			decalProjector.size = new Vector3(decalSize, decalSize, 0.05f);

			//Trigger size being set here for cleaner mop interaction
			decalTrigger.size = new Vector3(decalSize * 0.75f, decalSize * 0.75f, 0.05f);

			//Setting roll rotation of decal
			vomitDecal.transform.localRotation = Quaternion.Euler(0, 0, decalRotation); 

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
