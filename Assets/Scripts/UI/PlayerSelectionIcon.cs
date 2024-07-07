using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSelectionIcon : MonoBehaviour
{

	[Header("Character Script Variables")]
	[Space]
	[SerializeField] private PlayerSelectionController m_Character1Selector;
	[SerializeField] private PlayerSelectionController m_Character2Selector;

	[Header("Selection Variables Variables")]
	[Space]
	public bool m_CatSelected;
	public bool m_CleanerSelected;


	public void Start()
	{
		//Finding the player 1 game object and listening to the event broadcast
		m_Character1Selector = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSelectionController>();
		m_Character1Selector.OnPlayerSelection += Handle_PlayerSelection;
	}

	public void OnDisable()
	{
		//Removing listeners from events
		m_Character1Selector.OnPlayerSelection -= Handle_PlayerSelection;
		if(m_Character2Selector != null)
		{
			m_Character2Selector.OnPlayerSelection -= Handle_PlayerSelection;
		}
	}

	public void Handle_Player2Creation()
	{
		//Finding the player 2 game object and listening to the event broadcast
		m_Character2Selector = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerSelectionController>();
		m_Character2Selector.OnPlayerSelection += Handle_PlayerSelection;
	}

	//Handle the UI instantiation in the player so you can pass the UI icons
	//you want to move without having to write two seperate functions for each.
	public void Handle_PlayerSelection(int Selection, VisualElement PlayerIcon)
	{
		// Actioning player Selection based on input

		switch (Selection)
		{
			case 0:
				MoveCenter(PlayerIcon);
			break;
			case 1:
				MoveRight(PlayerIcon);
			break;
			case -1:
				MoveLeft(PlayerIcon);
			break;
			default:
				MoveCenter(PlayerIcon); 
			break;
		}
	}

	//Moving Player Icon Left
	public void MoveLeft(VisualElement icon)
	{
		if(icon.ClassListContains("IconCenter"))
		{
			icon.RemoveFromClassList("IconCenter");
		}

		icon.AddToClassList("IconLeft");
		m_CatSelected = true;
	}

	//Moving Player Icon Right
	public void MoveRight(VisualElement icon)
	{
		if (icon.ClassListContains("IconCenter"))
		{
			icon.RemoveFromClassList("IconCenter");
		}

		icon.AddToClassList("IconRight");
		m_CleanerSelected = true;
	}

	//Moving Player Icon Center
	public void MoveCenter(VisualElement icon)
	{
		if (icon.ClassListContains("IconLeft") || icon.ClassListContains("IconRight"))
		{
			icon.RemoveFromClassList("IconLeft");

			icon.RemoveFromClassList("IconRight");
		}

		icon.AddToClassList("IconCenter");
	}
}
