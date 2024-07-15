using UnityEngine.InputSystem;
using UnityEngine;
using System.ComponentModel.Design;

public class CharacterSelector : PlayerInputManager
{
	[Header("Player Controller Prefabs")]
	[Space]
	public GameObject CleanerPrefab;
	public GameObject CatPrefab;

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
			player1 = Instantiate(CleanerPrefab, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity);
		}
		else if(GameManager.m_Character1Selection == GameManager.CharacterSelection.Cat)
		{
			player1 = Instantiate(CatPrefab, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity);
		}
	}

	public void Start()
	{
		if(GameManager.m_Character2Selection == GameManager.CharacterSelection.Cleaner)
		{
			playerPrefab = CleanerPrefab;
			JoinPlayer(-1, -1, "GamePad", pairWithDevice: Gamepad.all[0]);
			m_Player2 = GameObject.FindGameObjectWithTag("Player2");

			m_Player2.transform.position = m_Player2SpawnLocation.position;
			Debug.Log("Pos set");
		}
		else if (GameManager.m_Character2Selection == GameManager.CharacterSelection.Cat)
		{
			playerPrefab = CatPrefab;
			JoinPlayer(-1, -1, "GamePad", pairWithDevice: Gamepad.all[0]);
			m_Player2 = GameObject.FindGameObjectWithTag("Player2");

			m_Player2.transform.position = m_Player2SpawnLocation.position;
			Debug.Log("Pos set");
		}
	}

	public void SetPos()
	{
		m_Player2 = GameObject.FindGameObjectWithTag("Player2");

		m_Player2.transform.position = m_Player2SpawnLocation.position;
		Debug.Log("Pos set");
	}
	
}
