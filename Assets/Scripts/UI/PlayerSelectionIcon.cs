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
		m_characterSelector = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSelectionController>();
		m_characterSelector.OnPlayerSelection += Handle_PlayerSelection;

		uiPlayerContainer = GetComponent<UIDocument>();

		TemplateContainer PlayerContainer = m_PlayerIconTemplate.Instantiate();

		uiPlayerContainer.rootVisualElement.Q("IconContainer").Add(PlayerContainer);

		m_Player1Icon = uiPlayerContainer.rootVisualElement.Q("Player1Icon");
	}

	public void OnDisable()
	{
		m_characterSelector.OnPlayerSelection -= Handle_PlayerSelection;
	}

	public void Handle_PlayerSelection(int Selection)
	{
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

	public void MoveLeft()
	{
		m_Player1Icon.style.marginRight = 100;
		m_Player1Icon.style.marginLeft = 0;

	}

	public void MoveRight()
	{
		m_Player1Icon.style.marginRight = 0;
		m_Player1Icon.style.marginLeft = 100;
	}

	public void MoveCenter()
	{
		m_Player1Icon.style.marginLeft = 0;
		m_Player1Icon.style.marginRight = 0;
	}
}
