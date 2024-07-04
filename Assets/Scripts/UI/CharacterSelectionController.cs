using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterSelectionController : MonoBehaviour
{

	public event Action<int> OnPlayerSelection;

	[SerializeField] private bool m_BothSelected;
	[SerializeField] private int m_Selection;

	public void Start()
	{
		m_Selection = 0;
	}

	//Firing selcection events to update UI based off of player input 
	// ***** -1 being left "CAT" ***** //
	// ***** 0 being left "NUETRAL" ** //
	// ***** 1 being left "CLEANER" ** //
	public void Select(InputAction.CallbackContext context)
	{
		float selectionInput = context.ReadValue<float>();

		if (selectionInput > 0.0f)
		{
			if(m_Selection == -1)
			{
				m_Selection = 0;
				OnPlayerSelection?.Invoke(m_Selection);
			}
			else
			{
				m_Selection = 1;
				OnPlayerSelection?.Invoke(m_Selection);
			}
		}
		else if(selectionInput < 0.0f)
		{
			if (m_Selection == 1)
			{
				m_Selection = 0;
				OnPlayerSelection?.Invoke(m_Selection);
			}
			else
			{
				m_Selection = -1;
				OnPlayerSelection?.Invoke(m_Selection);
			}
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