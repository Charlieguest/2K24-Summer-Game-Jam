using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player1SelectionController : MonoBehaviour
{

	public event Action OnPlayer2Creation;
	public event Action<int> OnPlayerSelection;

	[Header("Selection Position Vars")]
	[Space]
	[SerializeField] private bool m_BothSelected;
	[SerializeField] private int m_Selection;

	[Header("Player Specific Vars")]
	[Space]
	[SerializeField] private bool m_Player1Created;

	public void Start()
	{
		if (m_Player1Created)
		{
			// Attempted to get player two working here, think I will have to create a second script for a second controller

			// TODO:

			// Create new controller

			// Set it up basically the same way I set it up here Just invoking the Event on startup there instead of below here.
			Debug.Log($"Player 2 is created {m_Player1Created}");
			OnPlayer2Creation?.Invoke();
		}

		m_Player1Created = true;

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