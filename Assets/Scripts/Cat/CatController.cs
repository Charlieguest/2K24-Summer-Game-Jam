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
    [SerializeField] private float m_catSpeed = 1f;
    [SerializeField] private float m_catJumpHeight = 10f;
    private float m_acceleration;

    [SerializeField] private float m_Sensitivity;

    private Vector3 m_forceDirection = Vector3.zero;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_catInput = new CatControllerInput();
    }

    private void OnEnable()
    {
        m_catInput.DefaultCat.Move.started += CatMove;
        m_catInput.DefaultCat.Jump.started += CatJump;
        m_catInput.Enable();

    }

    private void OnDisable()
    {
        m_catInput.DefaultCat.Move.started -= CatMove;
        m_catInput.DefaultCat.Jump.started -= CatJump;
        m_catInput.Disable();
    }

    private void FixedUpdate()
    {
            m_rb.AddForce(m_forceDirection, ForceMode.Impulse);
    }

    private void CatJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            m_rb.AddForce(transform.up * m_catJumpHeight, ForceMode.Impulse);
        }
    }

    private void CatMove(InputAction.CallbackContext context)
    {
        m_forceDirection += context.ReadValue<Vector2>().x * transform.right * m_catSpeed;
        m_forceDirection += context.ReadValue<Vector2>().y * transform.forward * m_catSpeed;

        if (context.canceled)
            m_forceDirection = Vector3.zero;
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
