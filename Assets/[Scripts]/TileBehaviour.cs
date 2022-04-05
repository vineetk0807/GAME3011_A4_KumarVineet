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

    // Encryption/Decryption speed
    public float encryptionSpeed = 1f;

    // Text of the tile
    public TextMeshProUGUI TMP_character;
    public char holdingCharacter = 'Z';

    private void Awake()
    {
        holdingCharacter = GenerateRandomAlphabets();
        TMP_character.text = holdingCharacter.ToString();

        StartCoroutine(EncryptDecrypt());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f,movementSpeed * Time.deltaTime,0f);
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
            yield return new WaitForSeconds(encryptionSpeed);
            TMP_character.text = holdingCharacter.ToString();
            yield return new WaitForSeconds(encryptionSpeed);
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
            return (char)Random.Range('A', 'Z');
        }
        else
        {
            return (char)Random.Range('a', 'z');
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
