using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    public char Character;
    public TextMeshProUGUI TMP_PasswordInput;
    private string password = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Character != '\0')
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                password += Character;
                TMP_PasswordInput.text = password;
            }
        }
    }


    /// <summary>
    /// Backspace the text
    /// </summary>
    public void BackspaceFunction()
    {
        string tempString = "";

        // get all characters except last one, and reassign string
        for (int i = 0; i < password.Length - 1; i++)
        {
            tempString += password[i];
        }

        TMP_PasswordInput.text = password = tempString;
    }



    /// <summary>
    /// Sends the password
    /// </summary>
    public void SendFunction()
    {
        GameManager.GetInstance().CheckPassword(password);
    }
}
