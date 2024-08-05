using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
	[SerializeField] private GameObject[] m_LoadOut;

	[SerializeField] private float m_MovementInput;
	[SerializeField] private int m_LoadoutIndex;

	// We need this set to see what to call the interact events on
	public GameObject m_ActiveWeapon;

	void Start()
	{
		m_LoadoutIndex = 1;
		m_ActiveWeapon = m_LoadOut[m_LoadoutIndex];
	}

	public void OnLoadoutSwitch(InputAction.CallbackContext context)
	{
		m_MovementInput = context.ReadValue<float>();

		if (context.performed)
		{
			// Checking if you have switched positive or negative 
			// if negative you multiply by -1 
			// if positive you multiply by 1
			m_LoadoutIndex *= (int)m_MovementInput;

			//--checking against modified loadout \/
			//-- the switch allows us to determine if we are going negative or positive with our selection --//
			//-- (left and right ) --// 
			//-----------------------//
			//-----------------------//
			//-- this is important as once you reach the limits of the loadout --//
			//-- instead of being stuck you switch to the weapon at the other end of the scale --//

			switch (m_LoadoutIndex)
			{
				case -3:
					m_LoadoutIndex *= (int)m_MovementInput;
					m_LoadOut[m_LoadoutIndex].SetActive(false);
					m_LoadoutIndex--;
					m_LoadOut[m_LoadoutIndex].SetActive(true);
					m_ActiveWeapon = m_LoadOut[m_LoadoutIndex];
					break;

				case -2:
					m_LoadoutIndex *= (int)m_MovementInput;
					m_LoadOut[m_LoadoutIndex].SetActive(false);
					m_LoadoutIndex--;
					m_LoadOut[m_LoadoutIndex].SetActive(true);
					m_ActiveWeapon = m_LoadOut[m_LoadoutIndex];
					break;

				case -1:
					m_LoadoutIndex *= (int)m_MovementInput;
					m_LoadOut[m_LoadoutIndex].SetActive(false);
					m_LoadoutIndex--;
					m_LoadOut[m_LoadoutIndex].SetActive(true);
					m_ActiveWeapon = m_LoadOut[m_LoadoutIndex];
					break;

				case 0:
					if (m_MovementInput > 0)
					{
						m_LoadOut[m_LoadoutIndex].SetActive(false);
						m_LoadoutIndex++;
						m_LoadOut[m_LoadoutIndex].SetActive(true);
						m_ActiveWeapon = m_LoadOut[m_LoadoutIndex];
					}
					else if (m_MovementInput < 0)
					{
						m_LoadOut[m_LoadoutIndex].SetActive(false);
						m_LoadoutIndex += 3;
						m_LoadOut[m_LoadoutIndex].SetActive(true);
						m_ActiveWeapon = m_LoadOut[m_LoadoutIndex];
					}
					break;

				case 1:
					m_LoadOut[m_LoadoutIndex].SetActive(false);
					m_LoadoutIndex++;
					m_LoadOut[m_LoadoutIndex].SetActive(true);
					m_ActiveWeapon = m_LoadOut[m_LoadoutIndex];
					break;

				case 2:
					m_LoadOut[m_LoadoutIndex].SetActive(false);
					m_LoadoutIndex++;
					m_LoadOut[m_LoadoutIndex].SetActive(true);
					m_ActiveWeapon = m_LoadOut[m_LoadoutIndex];
					break;

				case 3:
					m_LoadOut[m_LoadoutIndex].SetActive(false);
					m_LoadoutIndex -= 3;
					m_LoadOut[m_LoadoutIndex].SetActive(true);
					m_ActiveWeapon = m_LoadOut[m_LoadoutIndex];
					break;

				default:
					Debug.LogError("Loadout system broken");
					break;
			}
		}
	}
}


