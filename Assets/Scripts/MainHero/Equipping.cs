using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipping : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] private Animator mainHeroAnimator;

    [Header("Arms")]
    [SerializeField] private GameObject arms;

    [Header("Weapons")]
    [SerializeField] private List<GameObject> weapons;
    private bool withWeapon = false;

    void Update()
    {
        //пистолет
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }

        //Хак пистолет
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.F) && withWeapon)
        {
            HideWeapon();
        }
    }

    private void EquipWeapon(int weaponID)
    {
        for (int index = 0; index < weapons.Count; index++)
        {
            if (weapons[index].activeSelf && weapons[index].GetComponent<Shooting>().weaponID == weaponID)
            {
                break;
            }
            else
            {
                if (weapons[index].GetComponent<Shooting>().weaponID != weaponID)
                {
                    weapons[index].SetActive(false);
                }
                else
                {
                    weapons[index].SetActive(true);

                    mainHeroAnimator.SetBool("With Weapon", true);
                    withWeapon = true;
                }
            }
        }
    }

    private void HideWeapon()
    {
        for (int index = 0; index < weapons.Count; index++)
        {
            weapons[index].SetActive(false);
        }

        mainHeroAnimator.SetBool("With Weapon", false);
        withWeapon = false;
    }

    public void ActivateArms()
    {
        if (!arms.activeSelf)
        {
            arms.SetActive(true);
        }
    }

    public void DeactivateArms()
    {
        if (arms.activeSelf)
        {
            arms.SetActive(false);
        }
    }
}