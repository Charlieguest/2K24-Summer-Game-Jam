using UnityEngine.InputSystem;
using UnityEngine;
using System.ComponentModel.Design;

public class CharacterSelector : PlayerInputManager
{
	[Header("Player Controller Prefabs")]
	[Space]
	public GameObject CleanerPrefabP1;
	public GameObject CatPrefabP1;

	[Space]

	public GameObject CleanerPrefabP2;
	public GameObject CatPrefabP2;

	[Header("Game Manager")]
	[Space]
	[SerializeField] private GameManager m_GameManager;

	[Header("Player 2 spawn transform")]
	[Space]
	[SerializeField] private GameObject m_Player2;

	[SerializeField] private Transform m_Player1SpawnLocation;
	[SerializeField] private Transform m_Player2SpawnLocation;

	public void Awake()
	{
		m_GameManager = GetComponent<GameManager>();
		GameObject player1;

		if (GameManager.m_Character1Selection == GameManager.CharacterSelection.Cleaner)
		{
			player1 = Instantiate(CleanerPrefabP1, m_Player1SpawnLocation.position, Quaternion.identity);
		}
		else if(GameManager.m_Character1Selection == GameManager.CharacterSelection.Cat)
		{
			player1 = Instantiate(CatPrefabP1, m_Player1SpawnLocation.position, Quaternion.identity);
		}
	}

	public void Start()
	{
		if(GameManager.m_Character2Selection == GameManager.CharacterSelection.Cleaner)
		{
			playerPrefab = CleanerPrefabP2;
			JoinPlayer(-1, -1, "GamePad", pairWithDevice: Gamepad.all[0]);
			m_Player2 = GameObject.FindGameObjectWithTag("Player2");

			m_Player2.transform.position = m_Player2SpawnLocation.position;
			Debug.Log("Pos set");
		}
		else if (GameManager.m_Character2Selection == GameManager.CharacterSelection.Cat)
		{
			playerPrefab = CatPrefabP2;
			JoinPlayer(-1, -1, "GamePad", pairWithDevice: Gamepad.all[0]);
			m_Player2 = GameObject.FindGameObjectWithTag("Player2");

			m_Player2.transform.position = m_Player2SpawnLocation.position;
			Debug.Log("Pos set");
		}
	}
}
