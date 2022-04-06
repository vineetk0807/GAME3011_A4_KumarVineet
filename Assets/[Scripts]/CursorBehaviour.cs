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

    [Header("Operator Text")] 
    public float secondsPerCharacter = 0.1f;
    public TextMeshProUGUI TMP_Operator;
    private bool isNarrationOn = false;

    private int hackProgress = 0;
    private string[] operatorString = new[] 
        {"Start the decryption, I have frozen the system.",
            "You are on the right track. Go!" ,
            "Almost there!", 
            "Come on! Don't give up now. Focus!", 
            "Nearly Done! GO!"};

    private string incorrectText = "Something seems off between the last 2 inputs. Backspace and try again.";

    private IEnumerator narrationCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        narrationCoroutine = AnimateTextCoroutine(operatorString[hackProgress]);
        StartCoroutine(narrationCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if (Character != '\0')
        {
            if (Input.GetKeyDown(KeyCode.Return) && GameManager.GetInstance().isPlaying)
            {
                password += Character;
                TMP_PasswordInput.text = password;

                // check if you are beyond length
                if (password.Length > GameManager.GetInstance().currentPassword.Length)
                {
                    if (!isNarrationOn)
                    {
                        isNarrationOn = true;
                        narrationCoroutine = AnimateTextCoroutine("Do you think this is a joke? That's not right.");
                        StartCoroutine(narrationCoroutine);
                    }
                    else
                    {
                        isNarrationOn = true;
                        StopCoroutine(narrationCoroutine);
                        narrationCoroutine = AnimateTextCoroutine("Do you think this is a joke? That's not right.");
                        StartCoroutine(narrationCoroutine);
                    }

                    return;
                }

                if (password.Length % 2 == 0)
                {
                    if (CheckPassword())
                    {
                        hackProgress++;
                        if (!isNarrationOn)
                        {
                            isNarrationOn = true;
                            narrationCoroutine = AnimateTextCoroutine(operatorString[hackProgress]);
                            StartCoroutine(narrationCoroutine);
                        }
                        else
                        {
                            isNarrationOn = true;
                            StopCoroutine(narrationCoroutine);
                            narrationCoroutine = AnimateTextCoroutine(operatorString[hackProgress]);
                            StartCoroutine(narrationCoroutine);
                        }

                        Debug.Log("On the right track");
                    }
                    else
                    {
                        if (!isNarrationOn)
                        {
                            isNarrationOn = true;
                            StartCoroutine(AnimateTextCoroutine(incorrectText));
                        }
                        else
                        {
                            isNarrationOn = true;
                            StopCoroutine(narrationCoroutine);
                            StartCoroutine(AnimateTextCoroutine(incorrectText));
                        }
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

        // update progress
        hackProgress--;
        if (hackProgress <= 0)
        {
            operatorString[0] = "Let's start the decryption again.";
            hackProgress = 0;
        }
    }



    /// <summary>
    /// Sends the password
    /// </summary>
    public void SendFunction()
    {
        GameManager.GetInstance().CheckPassword(password, this);
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

    /// <summary>
    /// Animate Text
    /// </summary>
    /// <param name="message"></param>
    /// <param name="secondsPerCharacter"></param>
    /// <returns></returns>
    IEnumerator AnimateTextCoroutine(string message)
    {
        TMP_Operator.text = "";

        for (int currentChar = 0; currentChar < message.Length; currentChar++)
        {
            TMP_Operator.text += message[currentChar];
            yield return new WaitForSeconds(secondsPerCharacter);
        }
        
        StopCoroutine(narrationCoroutine);
        isNarrationOn = false;
    }

    public void NarrateText(string message)
    {
        if (!isNarrationOn)
        {
            isNarrationOn = true;
            narrationCoroutine = AnimateTextCoroutine(message);
            StartCoroutine(narrationCoroutine);
        }
        else
        {
            StopCoroutine(narrationCoroutine);
            isNarrationOn = true;
            narrationCoroutine = AnimateTextCoroutine(message);
            StartCoroutine(narrationCoroutine);
        }
    }

}
