using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBox : MonoBehaviour
{

    [Header("Cursor Box")] 
    public GameObject cursorBox;

    [Header("Limit")]
    public Vector3 startPosition;

    [Header("Limit")]
    public Vector2 verticalLimit;
    public Vector2 horizontalLimit;

    [Header("Factors to move")] 
    public float verticalFactor;
    public float horizontalFactor;
    
    [Header("Keycodes for Up and Down")]
    public KeyCode UpKeyCode1;
    public KeyCode UpKeyCode2;
    public KeyCode DownKeyCode1;
    public KeyCode DownKeyCode2;


    [Header("Keycodes for Right and Left")]
    public KeyCode RightKeyCode1;
    public KeyCode RightKeyCode2;
    public KeyCode LeftKeyCode1;
    public KeyCode LeftKeyCode2;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetInstance().isPlaying)
        {
            if (Input.GetKeyDown(UpKeyCode1) || Input.GetKeyDown(UpKeyCode2))
            {
                MoveLockPoint(1);
            }
            else if (Input.GetKeyDown(DownKeyCode1) || Input.GetKeyDown(DownKeyCode2))
            {
                MoveLockPoint(-1);
            }

            if (Input.GetKeyDown(RightKeyCode1) || Input.GetKeyDown(RightKeyCode2))
            {
                MoveCursor(1);
            }
            else if (Input.GetKeyDown(LeftKeyCode1) || Input.GetKeyDown(LeftKeyCode2))
            {
                MoveCursor(-1);
            }
        }
    }

    /// <summary>
    /// MoveLockPoint function based on direction, -ve, +ve
    /// </summary>
    /// <param name="direction"></param>
    private void MoveLockPoint(int direction)
    {
        if (direction > 0)
        {
            if (transform.position.y <= verticalLimit.y)
            {
                transform.position += new Vector3(0f, verticalFactor);
            }
        }
        else
        {
            if (transform.position.y >= verticalLimit.x)
            {
                transform.position += new Vector3(0f, -verticalFactor);
            }
        }
    }

    /// <summary>
    /// Moving Cursor handles the cursor box movement
    /// </summary>
    /// <param name="direction"></param>
    private void MoveCursor(int direction)
    {
        if (direction > 0)
        {
            if (cursorBox.transform.position.x <= horizontalLimit.y)
            {
                cursorBox.transform.position += new Vector3(horizontalFactor, 0f);
            }
        }
        else
        {
            if (cursorBox.transform.position.x >= horizontalLimit.x)
            {
                cursorBox.transform.position += new Vector3(-horizontalFactor, 0f);
            }
        }
    }
}
