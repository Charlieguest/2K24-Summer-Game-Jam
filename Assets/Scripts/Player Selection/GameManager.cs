using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
	
	public enum CharacterSelection
	{
		Idle,
		Cat,
		Cleaner
	};

	public static CharacterSelection m_Character1Selection;
	public static CharacterSelection m_Character2Selection;

	[Header("Universal UI")]
	[Space]

	[SerializeField] private PanelSettings m_CatPanelSettings;
	[SerializeField] private PanelSettings m_CleanerPanelSettings;

	[Header("Cat UI")]
	[Space]
	//controlling UI interactions between player and UI in the gamemode
	[SerializeField] private UIDocument m_CatVomit;
	[SerializeField] private VisualElement m_ProgressBarContainer;

	public void Awake()
	{
		//Querying the progress bar container and setting
		//initial visibility to zero.
		m_ProgressBarContainer = m_CatVomit.rootVisualElement.Q("ProgressBarContainer");
		m_ProgressBarContainer.visible = false;
	}

	public void UpdateCharacterSelection(int selection, bool isPlayer2)
	{
		// This bit is to change state of each player enum.
		// each player enum is a static variable that will save over scene loads.
		// Allows setup of each player based on their selection
		switch (selection)
		{
			case 0:
				if (!isPlayer2)
				{
					m_Character1Selection = CharacterSelection.Idle;

				}
				else
				{
					m_Character2Selection = CharacterSelection.Idle;
				}
				break;
			case 1:
				if (!isPlayer2)
				{
					m_Character1Selection = CharacterSelection.Cleaner;
					//Setting display of cleaner UI to the correct split screen
					m_CleanerPanelSettings.targetDisplay = 1;
				}
				else
				{
					m_Character2Selection = CharacterSelection.Cleaner;
					//Setting display of cat cleaner to the correct split screen
					m_CleanerPanelSettings.targetDisplay = 2;
				}
				break;
			case -1:
				if (!isPlayer2)
				{
					m_Character1Selection = CharacterSelection.Cat;
					//Setting display of cat UI to the correct split screen
					m_CatPanelSettings.targetDisplay = 1;
				}
				else
				{
					m_Character2Selection = CharacterSelection.Cat;
					//Setting display of cat UI to the correct split screen
					m_CatPanelSettings.targetDisplay = 2;
				}
				break;
			default:
				if (!isPlayer2)
				{
					m_Character1Selection = CharacterSelection.Idle;
				}
				else
				{
					m_Character2Selection = CharacterSelection.Idle;
				}
				break;
		}
	}

	public void ShowVomitChargeBar()
	{
		m_ProgressBarContainer.visible = true;
	}
}
