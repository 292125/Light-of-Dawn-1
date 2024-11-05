using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsMenu;
    public void OnNewGameClicked()
    {
        Debug.Log("New Game");
    }

    public void OnContinueClicked()
    {
        Debug.Log("Continue");
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsMenu.SetActive(!SettingsMenu.activeSelf);
        }
    }
}
