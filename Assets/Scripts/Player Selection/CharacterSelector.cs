using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterSelector : PlayerInputManager
{
	public GameObject m_TestCharacter;

	public GameManager m_GameManager;

	public void Awake()
	{
		m_GameManager = GetComponent<GameManager>();
	}

	public void Start()
	{


		playerPrefab = m_TestCharacter;

	}
}
