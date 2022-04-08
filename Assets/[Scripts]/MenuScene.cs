using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    /// <summary>
    /// Load Play Scene
    /// </summary>
    public void Button_LoadPlayScene(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                Data.difficulty = Difficulty.EASY;
                break;

            case 1:
                Data.difficulty = Difficulty.NORMAL;
                break;

            case 2:
                Data.difficulty = Difficulty.HARD;
                break;

            default:
                Data.difficulty = Difficulty.NORMAL;
                break;

        }

        SceneManager.LoadScene(1);
    }
}
