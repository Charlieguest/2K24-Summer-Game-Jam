using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatCamera : MonoBehaviour
{
    [SerializeField] private GameObject m_cameraFollowObject;
	[SerializeField] private GameObject m_cameraSwivelBase;

	private Vector3 m_followPos;
    [SerializeField] private float m_f_clampAngle = 60.0f;
    [SerializeField] private float m_f_inputSensitivity = 20.0f;
    private float m_f_mouseX;
    private float m_f_mouseY;
    private float m_f_rotY = 0.0f;
    private float m_f_rotX = 0.0f;

	// Camera Reference to change body
	[SerializeField] private GameObject m_Camera;

    private void Start()
    {
        Vector3 rot = m_cameraSwivelBase.transform.localRotation.eulerAngles;
        m_f_rotY = rot.y;
        m_f_rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Handle_LookPerformed(InputAction.CallbackContext context)
    {
        m_f_mouseX = context.ReadValue<Vector2>().x;
        m_f_mouseY = context.ReadValue<Vector2>().y;
	}

    private void LateUpdate()
    {
		//Getting the yaw and pitch
		m_f_rotY += m_f_mouseX * m_f_inputSensitivity * Time.deltaTime;
		m_f_rotX += m_f_mouseY * m_f_inputSensitivity * Time.deltaTime;

		//clamping the pitch
		m_f_rotX = Mathf.Clamp(m_f_rotX, -m_f_clampAngle, m_f_clampAngle);
		
		m_cameraSwivelBase.transform.eulerAngles = new Vector3(m_f_rotX, m_f_rotY, 0.0f);
		
		Transform target = m_cameraFollowObject.transform;

		m_cameraSwivelBase.transform.position = target.position - m_cameraSwivelBase.transform.forward;

		//---------------------------//
		//--- Rotate Player Body ---//
		//-------------------------//

		// Getting the normalised vector projection of where we want to look
		Vector3 CatTurnProjection = Vector3.ProjectOnPlane(m_Camera.transform.forward, transform.parent.up);

		// Getting the rotation required to turn 
		Quaternion targetCatRot = Quaternion.LookRotation(CatTurnProjection, transform.parent.up);

		//--- Debug Only ---//
		///Drawing a line to show where the looking direction is
		Debug.DrawLine(transform.position, transform.position + CatTurnProjection * 50, Color.red);

		// Rotate to that direction
		transform.rotation = targetCatRot;
	}
}
