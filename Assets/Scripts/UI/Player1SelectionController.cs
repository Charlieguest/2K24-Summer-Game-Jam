using System;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine;


public class Player1SelectionController : MonoBehaviour
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

		if (m_IsPlayer2)
		{
			playerContainer.style.marginTop = 200;
			m_UISelector = GameObject.FindGameObjectWithTag("UI Selection Controller").GetComponent<PlayerSelectionIcon>();
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
			if(m_Selection == -1)
			{
				m_Selection = 0;
				OnPlayerSelection?.Invoke(m_Selection, m_PlayerIcon);
			}
			else
			{
				m_Selection = 1;
				OnPlayerSelection?.Invoke(m_Selection, m_PlayerIcon);
			}
		}
		else if(selectionInput < 0.0f)
		{
			if (m_Selection == 1)
			{
				m_Selection = 0;
				OnPlayerSelection?.Invoke(m_Selection, m_PlayerIcon);
			}
			else
			{
				m_Selection = -1;
				OnPlayerSelection?.Invoke(m_Selection, m_PlayerIcon);
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