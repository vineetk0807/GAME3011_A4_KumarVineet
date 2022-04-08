using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public TextMeshProUGUI TMP_GameEndState;

    private void OnEnable()
    {
        switch (Data.gameEndState)
        {
            case 0:
                TMP_GameEndState.text = "You have successfully unlocked the password!";
                break;

            case 1:
                TMP_GameEndState.text = "You have failed to unlock the password!";
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Changes scene to main menu
    /// </summary>
    public void Button_MainMenu()
    {
        Data.ResetData();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Quit game
    /// </summary>
    public void Button_QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
