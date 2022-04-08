using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileBehaviour : MonoBehaviour
{
    // Tile movement speed
    public float movementSpeed = -10f;
    
    // Text of the tile
    public TextMeshProUGUI TMP_character;
    public char holdingCharacter = 'Z';

    private void Awake()
    {
        holdingCharacter = GenerateRandomAlphabets();
        TMP_character.text = holdingCharacter.ToString();

        if (GameManager.GetInstance().difficulty != Difficulty.EASY)
        {
            StartCoroutine(EncryptDecrypt());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetInstance().isPlaying)
        {
            transform.position += new Vector3(0f, movementSpeed * Time.deltaTime, 0f);
        }

        if (GameManager.GetInstance().currentPasswordHelpList.Count > 0 && holdingCharacter == GameManager.GetInstance().currentPasswordHelpList[0])
        {
            TMP_character.color = Color.blue;
        }
    }


    /// <summary>
    /// Coroutine to encrypt/decrypt characters
    /// </summary>
    /// <returns></returns>
    IEnumerator EncryptDecrypt()
    {
        while (this.gameObject)
        {
            TMP_character.text = GenerateRandomNumbersAndSpecialCharacters().ToString();
            yield return new WaitForSeconds(GameManager.GetInstance().encryptionSpeedSymbol);
            TMP_character.text = holdingCharacter.ToString();
            yield return new WaitForSeconds(GameManager.GetInstance().encryptionSpeedCharacter);

            if (!GameManager.GetInstance().isPlaying)
            {
                break;
            }
        }
    }

    
    /// <summary>
    /// Random number is referred from ASCII Table
    /// https://www.rapidtables.com/code/text/ascii-table.html
    /// </summary>
    /// <returns></returns>
    public char GenerateRandomCharacter()
    {
        return (char)Random.Range('#', 'z');
    }


    /// <summary>
    /// Generates numbers and special character
    /// </summary>
    /// <returns></returns>
    public char GenerateRandomNumbersAndSpecialCharacters()
    {
        return (char)Random.Range('#', '@');
    }


    /// <summary>
    /// Generate alphabets
    /// </summary>
    /// <returns></returns>
    public char GenerateRandomAlphabets()
    {
        if (Random.Range(2, 6) % 2 == 0)
        {
            return (char)Random.Range('A', '[');
        }
        else
        {
            return (char)Random.Range('a', '{');
        }
    }

    /// <summary>
    /// Using stay to get better way to detect collision
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cursor"))
        {
            CursorBehaviour cursor = other.gameObject.GetComponent<CursorBehaviour>();
            cursor.Character = holdingCharacter;
        }
    }

    /// <summary>
    /// If cursor is not there, make the character null
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cursor"))
        {
            CursorBehaviour cursor = other.gameObject.GetComponent<CursorBehaviour>();
            cursor.Character = '\0';
        }
    }
}
