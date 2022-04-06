using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

                if (password.Length % 3 == 0)
                {
                    if (CheckPassword())
                    {
                        Debug.Log("On the right track");
                    }
                    else
                    {
                        Debug.Log("Nope");
                    }
                }
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


    /// <summary>
    /// Checks password when it progresses
    /// </summary>
    /// <returns></returns>
    public bool CheckPassword()
    {
        bool check = true;

        char[] passChar = password.ToCharArray();
        char[] passCurrentChar = GameManager.GetInstance().currentPassword.ToCharArray();


        for (int i = 0; i < passChar.Length; i++)
        {
            check = passCurrentChar[i].Equals(passChar[i]);

            if (!check)
            {
                break;
            }
        }

        return check;
    }

}
