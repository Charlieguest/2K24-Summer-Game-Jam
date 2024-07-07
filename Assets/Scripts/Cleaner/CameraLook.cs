using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    private Vector2 mouseInput;

	[SerializeField] private float mouseSensitivity = 0.5f;

	public float MinYLookAngle = -90.0f;
	public float MaxYLookAngle = 90.0f;

	[SerializeField] private GameObject playerBody;

	// X rotation
    private float m_Yaw = 0.0f;

	// Y rotation
	private float m_Pitch = 0.0f;
    
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

	public void FixedUpdate()
	{
		transform.Rotate(Vector3.up, mouseInput.x * mouseSensitivity * Time.deltaTime);

		m_Pitch -= mouseInput.y * mouseSensitivity * Time.deltaTime;
		m_Pitch = Mathf.Clamp(m_Pitch, -80f, 80f);

		transform.localEulerAngles = new Vector3(m_Pitch, transform.localEulerAngles.y, 0f);
	}


    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }
}
