using System;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine;


public class PlayerSelectionController : MonoBehaviour
{
	public event Action<int, VisualElement> OnPlayerSelection;

	[Header("UI Variables")]
	[Space]
	private UIDocument uiPlayerContainer;

	[SerializeField] private VisualTreeAsset m_PlayerIconTemplate;
	[SerializeField] private VisualElement m_PlayerIcon;
	[SerializeField] private String m_IconElementName;

	[Header("Selection Position Vars")]
	[Space]
	[SerializeField] private bool m_BothSelected;
	[SerializeField] private int m_Selection;

	[Header("Player Specific Vars")]
	[Space]
	[SerializeField] private bool m_IsPlayer2;

	[SerializeField] private PlayerSelectionIcon m_UISelector;

	public enum CharacterSelection
	{
		Idle,
		Cat,
		Cleaner
	};

	public static CharacterSelection m_Character1Selection;
	public static CharacterSelection m_Character2Selection;

	public void Start()
	{
		uiPlayerContainer = GetComponent<UIDocument>();

		//Creating Player Icon Container
		TemplateContainer playerContainer = m_PlayerIconTemplate.Instantiate();
		playerContainer.style.marginBottom = 200;

		//Adding it to the UI document
		uiPlayerContainer.rootVisualElement.Q("IconContainer").Add(playerContainer);

		//Saving its instance to a variable
		m_PlayerIcon = uiPlayerContainer.rootVisualElement.Q($"{m_IconElementName}");

		//Saving UI controller ready for calling functions on.
		m_UISelector = GameObject.FindGameObjectWithTag("UI Selection Controller").GetComponent<PlayerSelectionIcon>();

		if (m_IsPlayer2)
		{
			playerContainer.style.marginTop = 200;
			m_UISelector.Handle_Player2Creation();
		}


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
			// Checking if current selection is left so we know to move into the centre
			// first before moving right.
			if(m_Selection == -1)
			{
				m_Selection = 0;
				OnPlayerSelection?.Invoke(m_Selection, m_PlayerIcon);
				m_UISelector.m_CatSelected = false;
			}
			else if (m_UISelector.m_CleanerSelected == false)
			{
				m_Selection = 1;
				OnPlayerSelection?.Invoke(m_Selection, m_PlayerIcon);
			}
		}
		else if(selectionInput < 0.0f)
		{
			// Checking if current selection is right so we know to move into the centre
			// first before moving left.
			if (m_Selection == 1)
			{
				m_Selection = 0;
				OnPlayerSelection?.Invoke(m_Selection, m_PlayerIcon);
				m_UISelector.m_CleanerSelected = false;
			}
			else if(m_UISelector.m_CatSelected == false)
			{
				m_Selection = -1;
				OnPlayerSelection?.Invoke(m_Selection, m_PlayerIcon);
			}
		}

		// This last bit is to change state of each player enum.
		// each player enum is a static variable that will save over scene loads.
		// Allows setup of each player based on their selection
		switch (m_Selection)
		{
			case 0:
				if (!m_IsPlayer2)
				{
					m_Character1Selection = CharacterSelection.Idle;
					Debug.Log(m_Character1Selection);
				}
				else
				{
					m_Character2Selection = CharacterSelection.Idle;
					Debug.Log(m_Character2Selection);
				}
			break;
			case 1:
				if (!m_IsPlayer2)
				{
					m_Character1Selection = CharacterSelection.Cleaner;
					Debug.Log(m_Character1Selection);
				}
				else
				{
					m_Character2Selection = CharacterSelection.Cleaner;
					Debug.Log(m_Character2Selection);
				}
			break;
			case -1:
				if (!m_IsPlayer2)
				{
					m_Character1Selection = CharacterSelection.Cat;
					Debug.Log(m_Character1Selection);
				}
				else
				{
					m_Character2Selection = CharacterSelection.Cat;
					Debug.Log(m_Character2Selection);
				}
			break;
			default:
				if (!m_IsPlayer2)
				{
					m_Character1Selection = CharacterSelection.Idle;
					Debug.Log(m_Character1Selection);
				}
				else
				{
					m_Character2Selection = CharacterSelection.Idle;
					Debug.Log(m_Character2Selection);
				}
			break;
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