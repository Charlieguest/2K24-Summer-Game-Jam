using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatVomitProjectile : MonoBehaviour
{
	[Header("Collision Checking")]
	[Space]
	[SerializeField] private int[] m_LayersToIgnore;

	void OnTriggerEnter(Collider other)
	{
		//Checking tags to make sure that the projectiles don't collide
		//with the player or each other
		if(other.gameObject.tag != "VomitProjectile" &&
			other.gameObject.tag != "CatPlayer" &&
			 other.gameObject.tag != "CatPlayer2")
		{

			Debug.Log("Projectile created");

			//Checking against each layer in the LayersToIgnore array
			//so that a decal won't spawn on objects I don't want it to
			for (int i = 0; i <= m_LayersToIgnore.Length - 1; i++)
			{
				if (other.gameObject.layer != m_LayersToIgnore[i]) { continue; }
				else { return; }
			}

			RaycastHit hit;
			if (Physics.SphereCast(transform.position, transform.localScale.x, transform.forward, out hit))
			{
				Debug.Log("Splat created");
			}

			Destroy(gameObject);
		}
	}

	void OnDrawGizmos()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, transform.localScale.x);
	}
}
