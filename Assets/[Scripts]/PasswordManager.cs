using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordManager : MonoBehaviour
{
    public Dictionary<string, string> PasswordWithHint;

    public List<string> passwords = new List<string>();
    public List<string> passwordHints = new List<string>();

    // Singleton
    private static PasswordManager _instance;

    public static PasswordManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        PasswordWithHint = new Dictionary<string, string>();
       
        for (int i = 0; i < passwords.Count; i++)
        {
            if (passwords.Count > 0)
            {
                PasswordWithHint.Add(passwords[i], passwordHints[i]);
            }
        }
    }
}
