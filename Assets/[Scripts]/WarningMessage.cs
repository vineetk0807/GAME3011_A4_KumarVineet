using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMessage : MonoBehaviour
{
    public GameObject warningCanvas;

    /// <summary>
    /// Function to disable menu warning
    /// </summary>
    public void Button_OKButtonPress()
    {
        warningCanvas.SetActive(false);
    }
}
