using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRB;

    private Vector3 movementInput;
    private Vector3 movement;

    private int playerSpeed = 70;


    void Awake()
    {
        player = this.gameObject;
        playerRB = player.GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector3>();

        movement = new Vector3(movementInput.x, 0, movementInput.z);

        if (context.canceled)
        {
            movementInput = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        
        playerRB.AddForce(((movement.x * playerRB.transform.right) + (movement.z * playerRB.transform.forward)) * playerSpeed);
    }
}
