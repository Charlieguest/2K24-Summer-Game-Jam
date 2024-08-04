using UnityEngine.InputSystem;
using UnityEngine;

public class CleanerInteract : MonoBehaviour
{
	[Header("Interaction Variables")]
	[Space]
	[SerializeField] private WeaponSwitching m_weaponSwitching;

	public void Start()
	{
		m_weaponSwitching = GetComponent<WeaponSwitching>();
	}

	public void OnInteractPrimary(InputAction.CallbackContext context)
	{
		if (context.performed)
		{

		}
	}
}
