using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathplane : MonoBehaviour
{
    /// <summary>
    /// Deathplane handles destroying the character fall down
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            Destroy(other.gameObject);
        }
    }
}
