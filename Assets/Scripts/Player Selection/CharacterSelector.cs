using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterSelector : PlayerInputManager
{
	public GameObject CleanerPrefab;
	public GameObject CatPrefab;

	[SerializeField] private GameManager m_GameManager;

	public void Awake()
	{
		m_GameManager = GetComponent<GameManager>();
		GameObject player1;

		if (GameManager.m_Character1Selection == GameManager.CharacterSelection.Cleaner)
		{
			Debug.Log("P1 Cleaner");
			player1 = Instantiate(CleanerPrefab, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity);
		}
		else if(GameManager.m_Character2Selection == GameManager.CharacterSelection.Cat)
		{
			Debug.Log("P1 Cat");
			player1 = Instantiate(CatPrefab, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity);
		}
	}

	public void Start()
	{
		if(GameManager.m_Character2Selection == GameManager.CharacterSelection.Cleaner)
		{
			playerPrefab = CleanerPrefab;
			Debug.Log("P2 Cleaner");
			JoinPlayer(-1, -1, "GamePad", pairWithDevice: Gamepad.all[0]);
		}
		else if (GameManager.m_Character2Selection == GameManager.CharacterSelection.Cat)
		{
			playerPrefab = CatPrefab;
			Debug.Log("P2 Cat");
			JoinPlayer(-1, -1, "GamePad", pairWithDevice: Gamepad.all[0]);
		}

	}

	
}
