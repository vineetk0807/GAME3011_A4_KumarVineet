using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class HackingSystem : MonoBehaviour
{
    // Tile prefab
    public GameObject tileGameObject;

    // Movement speed
    [Header("Movement Speed")]
    public float movementSpeed = -5f;

    // Spawn point
    [Header("Spawn Point")] 
    public List<Transform> spawnPoints;

    public float spawnDelay = 2f;
    public float spawnTimeCounter = 0f;

    [Header("Password and Hint")]
    public TextMeshProUGUI TMP_Password;
    public TextMeshProUGUI TMP_PasswordHint;

    private bool firstAndThird = true;

    // Start is called before the first frame update
    void Start()
    {
        LoadPassword();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetInstance().isPlaying)
        {
            spawnTimeCounter += Time.deltaTime;

            if (spawnTimeCounter >= spawnDelay)
            {
                spawnTimeCounter = 0f;
                SpawnTiles();
            }
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPassword();
        }
    }


    /// <summary>
    /// Spawns the tiles from 4 spawn points
    /// </summary>
    private void SpawnTiles()
    {
        if (GameManager.GetInstance().difficulty == Difficulty.HARD && GameManager.GetInstance().hardModeActive)
        {
            if (firstAndThird)
            {
                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    if (i == 0 || i == 2)
                    {
                        GameObject tempTile = Instantiate(tileGameObject, spawnPoints[i].position, Quaternion.identity);
                        tempTile.GetComponent<Transform>().parent = spawnPoints[i];
                        TileBehaviour tile = tempTile.GetComponent<TileBehaviour>();
                        tile.movementSpeed = movementSpeed;
                    }
                }
            }
            else
            {
                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    if (i == 1 || i == 3)
                    {
                        GameObject tempTile = Instantiate(tileGameObject, spawnPoints[i].position, Quaternion.identity);
                        tempTile.GetComponent<Transform>().parent = spawnPoints[i];
                        TileBehaviour tile = tempTile.GetComponent<TileBehaviour>();
                        tile.movementSpeed = movementSpeed;
                    }
                }
            }
        }
        else
        {
            foreach (var spawnPoint in spawnPoints)
            {
                GameObject tempTile = Instantiate(tileGameObject, spawnPoint.position, Quaternion.identity);
                tempTile.GetComponent<Transform>().parent = spawnPoint;
                TileBehaviour tile = tempTile.GetComponent<TileBehaviour>();
                tile.movementSpeed = movementSpeed;
            }
        }
    }



    /// <summary>
    /// Loads password onto the screen
    /// </summary>
    public void LoadPassword()
    {
        // generate password using game manager
        string generatedPassword = GameManager.GetInstance().GeneratePassword();

        // Encrypt the password
        TMP_Password.text = EncryptPassword(generatedPassword);

        // Load the password hint
        TMP_PasswordHint.text = PasswordManager.GetInstance().PasswordWithHint[GameManager.GetInstance().currentPassword];
    }


    /// <summary>
    /// Encrypt password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string EncryptPassword(string password)
    {
        char[] generatedPasswordArray = password.ToCharArray();

        // based on difficulty
        int numberOfCharactersEncrypted = 3 ;

        List<int> indicesForEncrypting = new List<int>();

        // While count is less than the difficulty based number
        while (indicesForEncrypting.Count < 3)
        {
            int index = Random.Range(0, password.Length - 1);
            if (!indicesForEncrypting.Contains(index))
            {
                indicesForEncrypting.Add(index);
            }
        }

        // Check for casewise substitution , ^ for uppercase, * for lowercase
        foreach (var index in indicesForEncrypting)
        {
            // Upper case
            if (generatedPasswordArray[index] >= 65 && generatedPasswordArray[index] <= 90)
            {
                generatedPasswordArray[index] = '^';
            }
            else
            {
                generatedPasswordArray[index] = '*';
            }
        }

        return generatedPasswordArray.ArrayToString();
    }
}
