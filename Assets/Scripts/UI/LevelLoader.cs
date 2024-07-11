using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	[Header("UI Variables")]
	[Space]
	[SerializeField] private UIDocument m_SelectionUIDocument;
	[SerializeField] private PlayerSelectionIcon m_UISelector;

	public void Awake()
	{
		m_SelectionUIDocument = GetComponent<UIDocument>();
		m_UISelector = GameObject.FindGameObjectWithTag("UI Selection Controller").GetComponent<PlayerSelectionIcon>();
	}

	public void Start()
	{
		Button startButton = m_SelectionUIDocument.rootVisualElement.Q<Button>();
		startButton.RegisterCallback<ClickEvent>(BeginLevel);
	}

	public void BeginLevel(ClickEvent evt)
	{
		if(m_UISelector.m_BothSelected)
		{
			SceneManager.LoadScene("Split Screen Test");
		}
	}

}
