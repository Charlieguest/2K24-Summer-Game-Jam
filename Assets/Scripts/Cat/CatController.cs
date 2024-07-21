using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatController : MonoBehaviour
{
    private Rigidbody m_rb;
    private CatControllerInput m_catInput;

    [Header("Movement")]
	[Space]
	[SerializeField] private float m_catSpeed = 0.3f;
    [SerializeField] private float m_catJumpHeight = 200f;
    private float m_acceleration;

    [SerializeField] private float m_Sensitivity;
	
	[Header("Input")]
	[Space]
	private Vector3 movementInput;
	[SerializeField] private Transform m_CameraTransform;

	private Vector3 m_forceDirection = Vector3.zero;

	[Header("Jump Variables")]
	[Space]
	[SerializeField] private bool m_IsGrounded;
	[SerializeField] private bool m_JumpCheckActive;

	[SerializeField] private float m_MinFallSpeed = -6f;
	[SerializeField] private float m_MaxFallSpeed = -8f;

	//Coroutines for Jump QOL
	Coroutine c_JumpVelocityCoroutine;
	Coroutine c_AntiGravCoroutine;

	private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_catInput = new CatControllerInput();
    }
	
	public void CatJump(InputAction.CallbackContext context)
    {
        if (context.performed && m_IsGrounded)
        {
            m_rb.AddForce(transform.up * m_catJumpHeight, ForceMode.Impulse);
			if(c_JumpVelocityCoroutine == null)
			{
				m_JumpCheckActive = true;
				c_JumpVelocityCoroutine = StartCoroutine(c_JumpVelocityUpdate());
			}
        }
    }

    private void FixedUpdate()
    {
		Vector3 move = m_CameraTransform.forward * movementInput.y + m_CameraTransform.right * movementInput.x;
		move.y = 0f;
		m_rb.AddForce(move.normalized * m_catSpeed, ForceMode.VelocityChange);

		//Check for grounded moved here as we need to
		//know constantly if we're grounded or not
		m_IsGrounded = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 0.5f);
	}

    public void CatMove(InputAction.CallbackContext context)
    {
		movementInput = context.ReadValue<Vector2>();

		Debug.Log("Input Recorded");

		if (context.canceled)
		{
			movementInput = Vector3.zero;
		}
	}

	public void CatStop(InputAction.CallbackContext context)
    {
		movementInput = Vector3.zero;
	}

	// Checking Velocity is below a certain threshold before applying apex
	IEnumerator c_JumpVelocityUpdate()
	{
		while (m_JumpCheckActive)
		{
			if (m_rb.velocity.y <= -1.0 && !m_IsGrounded)
			{
				if(c_AntiGravCoroutine == null)
				{
					m_rb.velocity = new Vector3(m_rb.velocity.x, 0, m_rb.velocity.z);
					c_AntiGravCoroutine = StartCoroutine(c_AntiGravUpdate());
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}

	// Waiting a small amount of time before clamping velocity
	IEnumerator c_AntiGravUpdate()
	{
		yield return new WaitForSeconds(0.1f);

		if (c_JumpVelocityCoroutine != null)
		{
			StopCoroutine(c_JumpVelocityCoroutine);
		}

		m_JumpCheckActive = false;
		c_JumpVelocityCoroutine = null;

		m_rb.velocity = new Vector3(m_rb.velocity.x, Mathf.Clamp(m_rb.velocity.y, m_MinFallSpeed, m_MaxFallSpeed), m_rb.velocity.z);

		c_AntiGravCoroutine = null;
	}
}
