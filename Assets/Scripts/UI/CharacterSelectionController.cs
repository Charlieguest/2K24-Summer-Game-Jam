using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterSelectionController : MonoBehaviour
{
	[SerializeField] private bool m_BothSelected;
	[SerializeField] private int m_Selection;

	public void Start()
	{
		m_Selection = 0;
		Debug.Log(m_Selection);
	}

	public void Select(InputAction.CallbackContext context)
	{
		float selectionInput = context.ReadValue<float>();

		if (selectionInput > 0.0f)
		{
			if(m_Selection == -1)
			{
				m_Selection = 0;
			}
			else
			{
				m_Selection = 1;
			}
			Debug.Log(m_Selection);
		}
		else if(selectionInput < 0.0f)
		{
			if (m_Selection == 1)
			{
				m_Selection = 0;
			}
			else
			{
				m_Selection = -1;
			}
			Debug.Log(m_Selection);
		}
	}

	public void Confirm(InputAction.CallbackContext context)
	{
		if (context.performed && m_BothSelected)
		{
			Debug.Log("Confirm");
		}
	}
}