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
	public static bool m_CatPlayer2;

	[Header("Universal UI")]
	[Space]

	[SerializeField] private PanelSettings m_CatPanelSettings;
	[SerializeField] private PanelSettings m_CleanerPanelSettings;

	[Header("Cat UI")]
	[Space]
	//controlling UI interactions between player and UI in the gamemode
	[SerializeField] private UIDocument m_CatVomit;
	[SerializeField] private VisualElement m_ProgressBarContainer;
	[SerializeField] private VisualElement m_ProgressBar;
	public VisualElement m_Progress;

	public void Awake()
	{
		//Querying the progress bar container and setting
		//initial visibility to zero.
		m_ProgressBarContainer = m_CatVomit.rootVisualElement.Q("ProgressBarContainer");
		m_ProgressBar = m_CatVomit.rootVisualElement.Q("ProgressBarBase");
		m_Progress = m_CatVomit.rootVisualElement.Q("Progress");
		m_ProgressBarContainer.visible = false;


		// Setting the UI to fit split screen in once the actual level begins
		// This will not do anything in the selection screen.
		if (m_CatPlayer2)
		{
			m_ProgressBar.style.marginLeft = Length.Percent(0);
			m_ProgressBar.style.marginRight = Length.Percent(3);
			m_ProgressBar.style.alignSelf = Align.FlexEnd;
		}
		else
		{
			m_ProgressBar.style.marginLeft = Length.Percent(3);
			m_ProgressBar.style.marginRight = Length.Percent(0);
			m_ProgressBar.style.alignSelf = Align.FlexStart;
		}


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
				}
				else
				{
					m_Character2Selection = CharacterSelection.Cleaner;
					//Setting display of cat cleaner to the correct split screen
					
				}
				break;
			case -1:
				if (!isPlayer2)
				{
					m_Character1Selection = CharacterSelection.Cat;
					//Setting display of cat UI to the correct split screen
					m_CatPlayer2 = false;
				}
				else
				{
					m_Character2Selection = CharacterSelection.Cat;
					//Setting display of cat UI to the correct split screen
					m_CatPlayer2 = true;
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
		m_Progress.style.height = Length.Percent(100);
	}

	//Updating charge bar with calculated value
	public void UpdateVomitChargeBar(float percentage)
	{
		m_Progress.style.height = Length.Percent(percentage);
	}
}
