using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Difficulty
{
    EASY,
    NORMAL,
    HARD
}

public class GameManager : MonoBehaviour
{
    // Singleton reference
    private static GameManager _instance;
    public static GameManager GetInstance()
    {
        return _instance;
    }

    public string currentPassword = "";

    [Header("Difficulty")] 
    public Difficulty difficulty;

    // Encryption/Decryption speed
    [Header("Encryption Speed")]
    public float encryptionSpeedCharacter = 1f;
    public float encryptionSpeedSymbol = 1f;
    public float lessTimer_characterSpeed = 2f;
    public float lessTimer_symbolSpeed = 2f;

    [Header("Timer")]
    public float timerCounter = 0f;
    public int timer = 0;
    public List<int> difficultyTimer;
    public TextMeshProUGUI TMP_timer;

    [Header("Game Stats")] 
    public bool isPlaying = true;
    public bool isOutOfTimer = false;
    public bool hasWon = false;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
 
    // Update is called once per frame
    void Update()
    {
        //  Countdown timer
        if (isPlaying && !isOutOfTimer)
        {
            timerCounter += Time.deltaTime;

            if (timerCounter >= 1)
            {
                timerCounter = 0f;
                timer--;

                TMP_timer.text = timer.ToString();

                // Change color
                TMP_timer.color = Color.Lerp(Color.red, Color.green, (float)timer / (float)difficultyTimer[(int)difficulty]);

                // if timer goes to 0, out of timer
                if (timer <= 0)
                {
                    isOutOfTimer = true;
                    isPlaying = false;
                }

                if (timer == 30)
                {
                    encryptionSpeedCharacter = lessTimer_characterSpeed;
                }
            }
        }


        // --
    }



    /// <summary>
    /// Generates Password
    /// </summary>
    /// <returns></returns>
    public string GeneratePassword()
    {
        int index = Random.Range(0, PasswordManager.GetInstance().passwords.Count);
        currentPassword = (PasswordManager.GetInstance().passwords[index]);
        return currentPassword;
    }


    /// <summary>
    /// Checks the password sent
    /// </summary>
    /// <param name="password"></param>
    public void CheckPassword(string password, CursorBehaviour hackingTerminal)
    {
        if (password.Equals(currentPassword))
        {
            isPlaying = false;
            hasWon = true;
        }
        else
        {
            isPlaying = false;
            hasWon = false;
        }

        CompleteHacking(hackingTerminal);
    }


    /// <summary>
    /// Finish Game
    /// </summary>
    private void CompleteHacking(CursorBehaviour hackingTerminal)
    {
        if (hasWon)
        {
            hackingTerminal.NarrateText("System hack completed. Well done!");
        }
        else
        {
            hackingTerminal.NarrateText("Wrong Password! Nice knowing you.. I am off. GLHF with the security..");
        }
    }

    /// <summary>
    /// Initialize function
    /// </summary>
    private void Initialize()
    {
        // Difficulty settings
        switch (difficulty)
        {
            case Difficulty.EASY:
                timer = difficultyTimer[(int)Difficulty.EASY];
                break;

            case Difficulty.NORMAL:
                timer = difficultyTimer[(int)Difficulty.NORMAL];
                break;

            case Difficulty.HARD:
                timer = difficultyTimer[(int)Difficulty.HARD];
                break;

            default:
                timer = difficultyTimer[(int)Difficulty.EASY];
                break;
        }

        // Set timer text
        TMP_timer.text = timer.ToString();
    }


    /// <summary>
    /// Reset function get reset values
    /// </summary>
    private void ResetFunction()
    {

    }
}
