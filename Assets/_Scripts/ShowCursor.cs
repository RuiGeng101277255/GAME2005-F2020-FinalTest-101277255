using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : MonoBehaviour
{
    public bool showCursor;
    public bool cursorLock;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = showCursor;
        if(!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        //Debug.Log(showCursor);
    }
}
