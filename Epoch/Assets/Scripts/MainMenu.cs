using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   //creates a parameter for a game object in unity so it knows what panel to activate.
   public GameObject settingsPanel;
   //Start Game moves to the scene with the ID of 1 in the build menu under the files tab. 
   public void StartGame() {
    SceneManager.LoadScene(1);
   }
   //opens the settings panel.
   public void OpenSettings() {
      settingsPanel.SetActive(true);
   }
   //closes the settings panel.
   public void CloseSettings() {
      settingsPanel.SetActive(false);
   }

   //When the game is properly built the ExitGame function will quit out of the application, for now it just sends a console message.
   public void ExitGame() {
    Debug.Log("Quiting Game");
    Application.Quit();
   }
}
