using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public TextMeshProUGUI TMP_GameEndState;

    public List<AudioClip> audioClips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        switch (Data.gameEndState)
        {
            case 0:
                TMP_GameEndState.text = "You have successfully unlocked the password!";
                audioSource.clip = audioClips[0];
                audioSource.Play();
                break;

            case 1:
                TMP_GameEndState.text = "You have failed to unlock the password!";
                audioSource.clip = audioClips[1];
                audioSource.Play();
                break;

        }
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
