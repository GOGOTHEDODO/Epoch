using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private string selectedWeapon;

    void Start()
    {
        // Gets the selected weapon but will default to sword of needed
        Debug.Log("Hello there");
        animator = GetComponent<Animator>();
        selectedWeapon = PlayerPrefs.GetString("SelectedWeapon");
    }

    // Name weapon animations like Bow_Light Sword_Heavy (you get the idea)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Left Click = Light Attack
        {
            animator.SetTrigger(selectedWeapon + "_Light");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) // Right Click = Heavy Attack
        {
            animator.SetTrigger(selectedWeapon + "_Heavy");
        }
        if (Input.GetKeyDown(KeyCode.E)) // E = Special Attack
        {
            animator.SetTrigger(selectedWeapon + "_Special");
        }
    }
}