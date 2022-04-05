using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    public char Character;

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
                Debug.Log(Character);
            }
        }
    }
}
