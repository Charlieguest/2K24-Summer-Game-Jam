using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
	[SerializeField] private Rigidbody playerRB;

	[SerializeField] private Transform m_CameraTransform;

    private Vector3 movementInput;
    private Vector3 movement;

    private int playerSpeed = 5;

	private float SprintMultiplier = 1.75f;

    void Awake()
    {
		player = this.gameObject;
        playerRB = player.GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        ///movement = new Vector3(movementInput.x, 0, movementInput.z);

        if (context.canceled)
        {
            movementInput = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
		Vector3 move = m_CameraTransform.forward * movementInput.y + m_CameraTransform.right * movementInput.x;
		move.y = 0f;
		playerRB.AddForce(move.normalized * playerSpeed, ForceMode.VelocityChange);
	}
}
