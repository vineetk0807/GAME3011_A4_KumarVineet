using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<char> currentPasswordHelpList;
    public string whatsTypedOut = "";

    [Header("Difficulty")] 
    public Difficulty difficulty;

    public float hardDifficultyTimeCounter = 0f;
    public float hardDifficultyTimer = 0f;
    public bool hardModeActive = false;

    public TextMeshProUGUI TMP_Difficulty;

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
    public bool panicMode = false;

    [Header("End Scene")] 
    public GameObject EndScene;

    private void Awake()
    {
        _instance = this;
        difficulty = Data.difficulty;
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
                    Data.gameEndState = 1;
                    EndScene.SetActive(true);
                }

                if (timer == 30)
                {
                    encryptionSpeedCharacter = lessTimer_characterSpeed;
                    StartPanicMode();
                }
            }

            if (difficulty == Difficulty.HARD)
            {
                hardDifficultyTimeCounter += Time.deltaTime;

                if (hardDifficultyTimeCounter > 10f)
                {
                    hardDifficultyTimeCounter = 0f;
                    hardModeActive = !hardModeActive;
                }
            }
        }

        // --
    }

    /// <summary>
    /// Panic mode func loads the remaining characters of a password
    /// </summary>
    private void StartPanicMode()
    {
        char[] tempCharArray = whatsTypedOut.ToCharArray();

        List<int> index = new List<int>();
        for (int i = 0; i < currentPassword.Length; i++)
        {
            if (i < whatsTypedOut.Length)
            {
                continue;
            }
            else
            {
                currentPasswordHelpList.Add(currentPassword[i]);
            }
        }

        panicMode = true;
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
    public void SubmitPassword(string password, CursorBehaviour hackingTerminal)
    {
        if (password.Equals(currentPassword))
        {
            isPlaying = false;
            hasWon = true;
            Data.gameEndState = 0;
        }
        else
        {
            isPlaying = false;
            hasWon = false;
            Data.gameEndState = 1;
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

        StartCoroutine(EndSceneCoroutine());
    }

    /// <summary>
    /// Coroutine that starts end scene
    /// </summary>
    /// <returns></returns>
    IEnumerator EndSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        EndScene.SetActive(true);
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
                TMP_Difficulty.text = "Difficulty: EASY";
                break;

            case Difficulty.NORMAL:
                timer = difficultyTimer[(int)Difficulty.NORMAL];
                TMP_Difficulty.text = "Difficulty: NORMAL";
                break;

            case Difficulty.HARD:
                timer = difficultyTimer[(int)Difficulty.HARD];
                TMP_Difficulty.text = "Difficulty: HARD";
                break;

            default:
                timer = difficultyTimer[(int)Difficulty.EASY];
                TMP_Difficulty.text = "Difficulty: EASY";
                break;
        }

        // Set timer text
        TMP_timer.text = timer.ToString();

        // Set new password list
        currentPasswordHelpList = new List<char>();
    }


    /// <summary>
    /// Reset function get reset values
    /// </summary>
    private void ResetFunction()
    {

    }
}
