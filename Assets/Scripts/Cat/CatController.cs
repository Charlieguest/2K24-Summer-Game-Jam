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
    [SerializeField] private float m_catJumpHeight = 10f;
    private float m_acceleration;

    [SerializeField] private float m_Sensitivity;
	
	[Header("Input")]
	[Space]
	[SerializeField] private Vector3 movementInput;
	[SerializeField] private Transform m_CameraTransform;

	private Vector3 m_forceDirection = Vector3.zero;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_catInput = new CatControllerInput();
    }
	
	/*
    private void OnEnable()
    {
        m_catInput.DefaultCat.Move.started += CatMove;
        m_catInput.DefaultCat.Move.canceled += CatStop;
        m_catInput.DefaultCat.Jump.started += CatJump;
        m_catInput.Enable();
    }

    private void OnDisable()
    {
        m_catInput.DefaultCat.Move.started -= CatMove;
        m_catInput.DefaultCat.Jump.started -= CatJump;
        m_catInput.DefaultCat.Move.canceled -= CatStop;
        m_catInput.Disable();
    }
	*/

	public void CatJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            m_rb.AddForce(transform.up * m_catJumpHeight, ForceMode.Impulse);
        }
    }

    public void CatMove(InputAction.CallbackContext context)
    {
		movementInput = context.ReadValue<Vector2>();

		Debug.Log("Input Recorded");


		if (context.canceled)
		{
			movementInput = Vector3.zero;
		}

		//m_forceDirection += context.ReadValue<Vector2>().x * transform.right * m_catSpeed;
		//m_forceDirection += context.ReadValue<Vector2>().y * transform.forward * m_catSpeed;
	}

    private void FixedUpdate()
    {
		Vector3 move = m_CameraTransform.forward * movementInput.y + m_CameraTransform.right * movementInput.x;
		move.y = 0f;
		m_rb.AddForce(move.normalized * m_catSpeed, ForceMode.VelocityChange);
	}

	public void CatStop(InputAction.CallbackContext context)
    {
        Debug.Log("Cancelled");
		movementInput = Vector3.zero;
	}

    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1f))
        {
            Debug.Log("Hello");
            return true;
        }
        else
            return false;
    }
}
