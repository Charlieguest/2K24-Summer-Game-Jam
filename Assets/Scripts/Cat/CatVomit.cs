using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatVomit : MonoBehaviour, IVomitable
{
	[Header("Gamemode")]
	[Space]

	[SerializeField] private GameManager m_GameManager;

	void Awake()
	{
		GameObject managerObject = GameObject.FindWithTag("GameController");

		m_GameManager = managerObject.GetComponent<GameManager>();
	}

	public void VomitStart()
	{
		Debug.Log("Vomit Begin");
		m_GameManager.ShowVomitChargeBar();
	}
}
