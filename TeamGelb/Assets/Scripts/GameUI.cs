using System.Collections;
using System.Collections.Generic;
using Gianni.Helper;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private static Color wrong_input_color = Color.red;
    private static Color correct_input_color = Color.green;
    private static Color no_input_color = Color.grey;

    private InputSystem input_system;
    private GUIStyle currentStyle = null;
    private Color current_color { get; set; }


    private void Awake()
    {
        current_color = no_input_color;
    }

    // Start is called before the first frame update
    void Start()
    {
        input_system = GetComponent<InputSystem>();
    }


    void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            UpdateCurrentStyle(input_system.isValid);
            this.InvokeWait(0.5f, ResetStyle);
        }
        InitStyles();

        float window_width = Screen.width * input_system.window_size / input_system.GetSecondsPerBeat();
        float window_left_edge = Screen.width / 2.0f - window_width / 2.0f;
        GUI.Box(new Rect(window_left_edge, 0, window_width, 50), "");

        float progress = (input_system.GetProgress() + 0.5f + input_system.window_size / 2.0f) % 1.0f;
        GUI.Box(new Rect(Screen.width * progress - 5, 0, 5, 50), "", currentStyle);

        GUI.Button(new Rect(10, 10, 70, 30), "Menu");
    }

    private void InitStyles()
    {
        currentStyle = new GUIStyle(GUI.skin.box);
        currentStyle.normal.background = MakeTex(2, 2, current_color);
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    private void UpdateCurrentStyle(bool validInput)
    {
        if (validInput)
        {
            current_color = correct_input_color;
        }
        else
        {
            current_color = wrong_input_color;
        }
    }

    private void ResetStyle()
    {
        current_color = no_input_color;
    }


}
