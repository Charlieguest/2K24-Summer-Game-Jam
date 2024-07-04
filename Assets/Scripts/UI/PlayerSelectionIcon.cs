using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSelectionIcon : MonoBehaviour
{
	[Header("UI Variables")]
	[Space]
	private UIDocument uiPlayerContainer;

	[SerializeField] private VisualTreeAsset m_PlayerIconTemplate;

	[SerializeField] private VisualElement m_Player1Icon;
	[SerializeField] private VisualElement m_Player2Icon;

	[Header("Character Script Variables")]
	[Space]
	[SerializeField] private CharacterSelectionController m_characterSelector;

	public void Start()
	{
		//Finding the player 1 game object and listening to the event broadcast
		m_characterSelector = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSelectionController>();
		m_characterSelector.OnPlayerSelection += Handle_PlayerSelection;

		uiPlayerContainer = GetComponent<UIDocument>();

		//Creating Player 1 Icon Container
		TemplateContainer playerContainer = m_PlayerIconTemplate.Instantiate();

		//Adding it to the UI document
		uiPlayerContainer.rootVisualElement.Q("IconContainer").Add(playerContainer);

		//Saving its instance to a variable
		m_Player1Icon = uiPlayerContainer.rootVisualElement.Q("Player1Icon");
	}

	public void OnDisable()
	{
		//Removing listener from to event
		m_characterSelector.OnPlayerSelection -= Handle_PlayerSelection;
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
			Debug.Log("Removed class");
		}

		m_Player1Icon.AddToClassList("IconLeft");
		Debug.Log("Added Class");
	}

	//Moving Player Icon Right
	public void MoveRight()
	{
		if (m_Player1Icon.ClassListContains("IconCenter"))
		{
			m_Player1Icon.RemoveFromClassList("IconCenter");
			Debug.Log("Removed class");
		}

		m_Player1Icon.AddToClassList("IconRight");
		Debug.Log("Added Class");
	}

	//Moving Player Icon Center
	public void MoveCenter()
	{
		if (m_Player1Icon.ClassListContains("IconLeft") || m_Player1Icon.ClassListContains("IconRight"))
		{
			m_Player1Icon.RemoveFromClassList("IconLeft");
			Debug.Log("Removed class");

			m_Player1Icon.RemoveFromClassList("IconRight");
			Debug.Log("Removed class");
		}

		m_Player1Icon.AddToClassList("IconCenter");
	}
}
