using UnityEngine;

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
				}
				else
				{
					m_Character2Selection = CharacterSelection.Cleaner;
				}
				break;
			case -1:
				if (!isPlayer2)
				{
					m_Character1Selection = CharacterSelection.Cat;
				}
				else
				{
					m_Character2Selection = CharacterSelection.Cat;
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
}
