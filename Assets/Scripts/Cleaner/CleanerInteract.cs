using UnityEngine.InputSystem;
using UnityEngine;

public class CleanerInteract : MonoBehaviour
{
	[Header("Interaction Variables")]
	[Space]
	private WeaponSwitching m_weaponSwitching;
	[SerializeField] private GameObject m_ActiveWeapon;

	public void Start()
	{
		m_weaponSwitching = GetComponent<WeaponSwitching>();
	}

	public void OnInteractPrimary(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("Happens");
			m_ActiveWeapon = m_weaponSwitching.m_ActiveWeapon;
			IInteractable interactable = m_ActiveWeapon.GetComponent<IInteractable>();
			if (interactable != null)
			{
				interactable.Interact();
			}
		}
	}
}
