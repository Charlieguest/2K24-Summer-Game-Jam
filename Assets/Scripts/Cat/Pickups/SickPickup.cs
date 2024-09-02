using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickPickup : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		transform.RotateAround(transform.position, Vector3.right, 200 * Time.deltaTime);
		transform.RotateAround(transform.position, Vector3.down, 200 * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		// Is collider Cat?
		if (other.gameObject.tag == "CatPlayer" ||
			other.gameObject.tag == "CatPlayer2")
		{
			// Does cat have vomitable interface?
			IVomitable vomitable = other.GetComponent<IVomitable>();
			if (vomitable != null)
			{
				//If so
				//Start Vomit process
				vomitable.VomitStart();
			}
			Destroy(gameObject);
		}
	}
}
