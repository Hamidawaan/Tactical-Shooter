/*
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<Weapon> weapons;
    public int currentWeaponIndex = 0;

    private void Start()
    {
        // Deactivate all weapons except the first one
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(i == currentWeaponIndex);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchToNextWeapon();
        }
    }

    private void SwitchToNextWeapon()
    {
        // Deactivate the current weapon
        weapons[currentWeaponIndex].gameObject.SetActive(false);

        // Increment the weapon index
        currentWeaponIndex++;

        // Wrap around to the first weapon if we exceed the list size
        if (currentWeaponIndex >= weapons.Count)
        {
            currentWeaponIndex = 0;
        }

        // Activate the new weapon
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }
}
*/