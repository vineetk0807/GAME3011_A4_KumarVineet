using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTitleEffect : MonoBehaviour
{
    public TextMeshProUGUI TMP_GameTitle;
    private string gameTitle = "MATRICES";
    public float timerSpecialCharacter = 0.2f;
    private string tempString = "";

    public int holdingCharacter = 0;
    public int tempCharacter = 0;

    // Start is called before the first frame update
    void Start()
    {
        tempString = gameTitle;
        StartCoroutine(EncryptDecrypt());
    }

    /// <summary>
    /// Coroutine to encrypt/decrypt characters
    /// </summary>
    /// <returns></returns>
    IEnumerator EncryptDecrypt()
    {
        while (this.gameObject)
        {
            // cannot accept the same character
            while (tempCharacter == holdingCharacter)
            {
                tempCharacter = Random.Range(0, gameTitle.Length);
            }

            // got a new one
            holdingCharacter = tempCharacter;

            // starting over
            tempString = gameTitle;
            
            // Replace that character in string
            tempString = tempString.Replace(tempString[holdingCharacter], GenerateRandomCharacters());

            // set text
            TMP_GameTitle.text = tempString;
            
            yield return new WaitForSeconds(timerSpecialCharacter);
            TMP_GameTitle.text = gameTitle;
            yield return new WaitForSeconds(2f);
        }
    }

    /// <summary>
    /// Generate random characters
    /// </summary>
    /// <returns></returns>
    public char GenerateRandomCharacters()
    {
        if (Random.Range(0, 5 % 2) == 0)
        {
            return (char)Random.Range('!', '/');
        }
        else
        {
            return (char)Random.Range('[', '`');
        }
        
    }
}
