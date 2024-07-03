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
    [SerializeField] private float m_catSpeed = 10f;
    [SerializeField] private float m_catJumpHeight = 10f;
    private float m_acceleration;

    [SerializeField] private float m_Sensitivity;

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
        if(m_acceleration > 0)
        {
           
        }
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
        m_acceleration = context.ReadValue<float>();
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 1f, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            Debug.Log("Hello");
            return true;
        }
        else
            return false;
    }
}
