using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class CharacterSelector : PlayerInputManager
{
	public GameObject CleanerPrefab;
	public GameObject CatPrefab;

	[SerializeField] private GameManager m_GameManager;

	public void Awake()
	{
		m_GameManager = GetComponent<GameManager>();
	}

	public void Start()
	{
		if(GameManager.m_Character1Selection == GameManager.CharacterSelection.Cleaner)
		{
			playerPrefab = CleanerPrefab;
			Debug.Log("works");
		}
	}
}
