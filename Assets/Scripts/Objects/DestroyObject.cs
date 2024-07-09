using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //Attach to object that is to be destroyed
    private Rigidbody hitObjectRB;

    private Vector3 objectVelocity;

    [SerializeField] private GameObject crackedVersion;

    // Start is called before the first frame update
    void Start()
    {
        objectVelocity = new Vector3(0, 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject)
        {
            if(collision.gameObject.GetComponent<Rigidbody>())
            {
                hitObjectRB = collision.gameObject.GetComponent<Rigidbody>();

                if (hitObjectRB.velocity.x > objectVelocity.x || hitObjectRB.velocity.y > objectVelocity.y || hitObjectRB.velocity.z > objectVelocity.z ||
                hitObjectRB.velocity.x < objectVelocity.x || hitObjectRB.velocity.y < objectVelocity.y || hitObjectRB.velocity.z < objectVelocity.z)
                {
                    BreakItem();
                }
            }
            
        }
    }

    void BreakItem()
    {
        if ((crackedVersion != null))
        {
            Instantiate(crackedVersion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
