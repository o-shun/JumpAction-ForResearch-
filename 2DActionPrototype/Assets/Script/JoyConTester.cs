using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyConTester : MonoBehaviour
{
    public float h1;
    public float v1;

    public float h2;
    public float v2;

    void Start()
    {
    }

    void Update()
    {
        // Joy-Con(R)
        //h1 = Input.GetAxis("Horizontal1");
        //v1 = Input.GetAxis("Vertical1");

        // Joy-Con(L)
        //h2 = Input.GetAxis("Horizontal2");
        //v2 = Input.GetAxis("Vertical2");
    }

    void OnGUI()
    {
        for (int i = (int)KeyCode.Joystick1Button0; i <= (int)KeyCode.Joystick2Button19; i++)
        {
            if (Input.GetKey((KeyCode)i))
                GUILayout.Label(((KeyCode)i).ToString() + " is pressed.");
        }
        GUILayout.Label((h1).ToString() + "、" + (v1).ToString());
    }
}