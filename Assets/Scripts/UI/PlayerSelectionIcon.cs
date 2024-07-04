using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSelectionIcon : MonoBehaviour
{
	[Header("UI Variables")]
	[Space]
	private UIDocument uiPlayerContainer;

	[SerializeField] private VisualTreeAsset m_Player1IconTemplate;
	[SerializeField] private VisualTreeAsset m_Player2IconTemplate;

	[SerializeField] private VisualElement m_Player1Icon;
	[SerializeField] private VisualElement m_Player2Icon;

	[Header("Character Script Variables")]
	[Space]
	[SerializeField] private Player1SelectionController m_characterSelector;

	public void Start()
	{
		//Finding the player 1 game object and listening to the event broadcast
		m_characterSelector = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1SelectionController>();
		m_characterSelector.OnPlayerSelection += Handle_PlayerSelection;
		m_characterSelector.OnPlayer2Creation += Handle_Player2Creation;

		uiPlayerContainer = GetComponent<UIDocument>();

		//Creating Player 1 Icon Container
		TemplateContainer playerContainer = m_Player1IconTemplate.Instantiate();

		//Adding it to the UI document
		uiPlayerContainer.rootVisualElement.Q("IconContainer").Add(playerContainer);

		//Saving its instance to a variable
		m_Player1Icon = uiPlayerContainer.rootVisualElement.Q("Player1Icon");
	}

	public void OnDisable()
	{
		//Removing listeners from events
		m_characterSelector.OnPlayerSelection -= Handle_PlayerSelection;
		m_characterSelector.OnPlayer2Creation -= Handle_Player2Creation;
	}

	public void Handle_Player2Creation()
	{
		Debug.Log("Event connected");

		//Creating Player 1 Icon Container
		TemplateContainer playerContainer = m_Player2IconTemplate.Instantiate();

		//Adding it to the UI document
		uiPlayerContainer.rootVisualElement.Q("IconContainer").Add(playerContainer);

		//Saving its instance to a variable
		m_Player2Icon = uiPlayerContainer.rootVisualElement.Q("Player2Icon");
	}

	public void Handle_PlayerSelection(int Selection)
	{
		// Actioning player Selection based on input
		switch (Selection)
		{
			case 0:
				MoveCenter();
			break;
			case 1:
				MoveRight();
				break;
			case -1:
				MoveLeft();
				break;
			default:
				MoveCenter(); 
			break;
		}
	}

	//Moving Player Icon Left
	public void MoveLeft()
	{
		if(m_Player1Icon.ClassListContains("IconCenter"))
		{
			m_Player1Icon.RemoveFromClassList("IconCenter");
		}

		m_Player1Icon.AddToClassList("IconLeft");
	}

	//Moving Player Icon Right
	public void MoveRight()
	{
		if (m_Player1Icon.ClassListContains("IconCenter"))
		{
			m_Player1Icon.RemoveFromClassList("IconCenter");
		}

		m_Player1Icon.AddToClassList("IconRight");
	}

	//Moving Player Icon Center
	public void MoveCenter()
	{
		if (m_Player1Icon.ClassListContains("IconLeft") || m_Player1Icon.ClassListContains("IconRight"))
		{
			m_Player1Icon.RemoveFromClassList("IconLeft");

			m_Player1Icon.RemoveFromClassList("IconRight");
		}

		m_Player1Icon.AddToClassList("IconCenter");
	}
}
