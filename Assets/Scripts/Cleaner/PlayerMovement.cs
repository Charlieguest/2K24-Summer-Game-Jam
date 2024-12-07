using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("Player Components")]
	[Space]
    [SerializeField] private GameObject player;
	[SerializeField] private Rigidbody playerRB;
	private Vector3 movementInput;

	[Header("Movement Variables")]
	[Space]
	[SerializeField] private Transform m_CameraTransform;
	[SerializeField] private int playerSpeed = 5;

	[Header("Jump Variables")]
	[Space]
	[SerializeField] private bool m_IsGrounded;
	[SerializeField] private bool m_JumpCheckActive;

	[SerializeField] private float m_JumpForce = 350f;
	[SerializeField] private float m_MinFallSpeed = -6f;
	[SerializeField] private float m_MaxFallSpeed = -8f;

	//Coroutines for Jump QOL
	Coroutine c_CleanerJumpVelocityCoroutine;
	Coroutine c_CleanerAntiGravCoroutine;


	void Awake()
    {
		player = this.gameObject;
        playerRB = player.GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        if (context.canceled)
        {
            movementInput = Vector3.zero;
        }
    }

	public void OnJump(InputAction.CallbackContext context)
	{
		if (context.performed && m_IsGrounded)
		{
			playerRB.AddForce(transform.up * m_JumpForce, ForceMode.Impulse);
			if (c_CleanerJumpVelocityCoroutine == null)
			{
				m_JumpCheckActive = true;
				c_CleanerJumpVelocityCoroutine = StartCoroutine(c_CleanerJumpVelocityUpdate());
			}
		}
	}

	void FixedUpdate()
    {
		Vector3 move = m_CameraTransform.forward * movementInput.y + m_CameraTransform.right * movementInput.x;
		move.y = 0f;
		playerRB.AddForce(move.normalized * playerSpeed, ForceMode.VelocityChange);

		m_IsGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.5f);
	}

	IEnumerator c_CleanerJumpVelocityUpdate()
	{
		while (m_JumpCheckActive)
		{
			if (playerRB.velocity.y <= -1.0 && !m_IsGrounded)
			{
				if (c_CleanerAntiGravCoroutine == null)
				{
					playerRB.velocity = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z);
					c_CleanerAntiGravCoroutine = StartCoroutine(c_CleanerAntiGravUpdate());
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator c_CleanerAntiGravUpdate()
	{
		yield return new WaitForSeconds(0.05f);

		if (c_CleanerJumpVelocityCoroutine != null)
		{
			StopCoroutine(c_CleanerJumpVelocityCoroutine);
		}

		m_JumpCheckActive = false;
		c_CleanerJumpVelocityCoroutine = null;

		playerRB.velocity = new Vector3(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, m_MinFallSpeed, m_MaxFallSpeed), playerRB.velocity.z);

		c_CleanerAntiGravCoroutine = null;
	}
}
