using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// At the start of the game this script runs based on what the player chooses to use for the run
// Very good chance this will need to be changed, im just guessing
public class WeaponSelect : MonoBehaviour
{
    // We are gonna have 3 buttons, bow, sword, spear, and the player will select one, this script will then set the players weapon
    public void SelectWeapon(string weaponName)
    {
        Debug.Log("GREETINGS");
        PlayerPrefs.SetString("SelectedWeapon", weaponName);
        Debug.Log("Selected Weapon: " + weaponName);
        SceneManager.LoadScene("TestMap");
    }

}
