using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterSelectionController : MonoBehaviour
{
	[SerializeField] private bool m_BothSelected;

	public void Select(InputAction.CallbackContext context)
	{
		float selectionInput = context.ReadValue<float>();

		if (selectionInput > 0.0f)
		{
			Debug.Log("Posetive");
		}
		else if(selectionInput < 0.0f)
		{
			Debug.Log("Oder Negative");
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