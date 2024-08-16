using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletProjectile : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Dead");
		Destroy(gameObject);
	}
}
