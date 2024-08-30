using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatCamera : MonoBehaviour
{
    [SerializeField] private float m_f_cameraSpeed = 120.0f;
    [SerializeField] private GameObject m_cameraFollowObject;
	[SerializeField] private GameObject m_cameraSwivelBase;

	private Vector3 m_followPos;
    [SerializeField] private float m_f_clampAngle = 60.0f;
    [SerializeField] private float m_f_inputSensitivity = 20.0f;
    private float m_f_mouseX;
    private float m_f_mouseY;
    private float m_f_rotY = 0.0f;
    private float m_f_rotX = 0.0f;

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

    private void Update()
    {
		m_f_rotY += m_f_mouseX * m_f_inputSensitivity * Time.deltaTime;
		m_f_rotX += m_f_mouseY * m_f_inputSensitivity * Time.deltaTime;

		m_f_rotX = Mathf.Clamp(m_f_rotX, -m_f_clampAngle, m_f_clampAngle);

		Quaternion localRot = Quaternion.Euler(m_f_rotX, m_f_rotY, 0.0f);
		m_cameraSwivelBase.transform.rotation = localRot;

		Transform target = m_cameraFollowObject.transform;

        float step = m_f_cameraSpeed * Time.deltaTime;
		m_cameraSwivelBase.transform.position = Vector3.MoveTowards(m_cameraSwivelBase.transform.position, target.position, step);
    }
}
