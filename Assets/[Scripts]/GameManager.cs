using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // Singleton reference
    private static GameManager _instance;
    public static GameManager GetInstance()
    {
        return _instance;
    }

    public string currentPassword = "";
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
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
    public void CheckPassword(string password)
    {
        if (password.Equals(currentPassword))
        {
            Debug.Log("Match!");
        }
        else
        {
            Debug.Log("Not Matching");
        }
    }
}
