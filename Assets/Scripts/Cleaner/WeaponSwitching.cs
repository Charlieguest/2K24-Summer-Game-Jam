using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
	[SerializeField] private GameObject[] m_LoadOut;

	[SerializeField] private float m_MovementInput;
	[SerializeField] private int m_LoadoutIndex;

	void Start()
	{
		m_LoadoutIndex = 1;
	}

	public void OnLoadoutSwitch(InputAction.CallbackContext context)
	{
		m_MovementInput = context.ReadValue<float>();

		if (context.performed)
		{
			/*-- if movement input is negative and we've not already hit the bottom item/weapon --*/
			if (m_MovementInput < 0 && m_LoadoutIndex > 0)
			{
				//Hiding last item in loadout before showing next one
				m_LoadOut[m_LoadoutIndex].SetActive(false);
				m_LoadoutIndex--;
				m_LoadOut[m_LoadoutIndex].SetActive(true);
			}

			/*-- if movement input is posetive and we've not already hit the top item/weapon --*/
			if (m_MovementInput > 0 && m_LoadoutIndex < 3)
			{
				//Hiding last item in loadout before showing next one
				m_LoadOut[m_LoadoutIndex].SetActive(false);
				m_LoadoutIndex++;
				m_LoadOut[m_LoadoutIndex].SetActive(true);
			}
		}
	}
}
