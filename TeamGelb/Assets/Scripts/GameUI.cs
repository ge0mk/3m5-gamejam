using System.Collections;
using System.Collections.Generic;
using Gianni.Helper;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public float cursor_offset = 0.0f;

    public float ScreenWidth = 0.5f;
    public float ScreenPositionOffset = 0.0f;

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
        bool pressedValidKey =
            Input.GetKey(input_system.UpInput) ||
            Input.GetKey(input_system.LeftInput) ||
            Input.GetKey(input_system.DownInput) ||
            Input.GetKey(input_system.RightInput);
        if (pressedValidKey)
        {
            UpdateCurrentStyle(input_system.isValid);
            this.InvokeWait(0.2f, ResetStyle);
        }
        InitStyles();

        float window_width = Screen.width * input_system.GetWindowSize() / input_system.GetSecondsPerBeat() * ScreenWidth;
        float window_left_edge = (Screen.width / 2 - window_width) * (1.0f + ScreenPositionOffset * 5.0f) / 2.0f;
        GUI.Box(new Rect(window_left_edge, 0, window_width, 50), "");

        float progress = (input_system.GetProgress() + 0.5f + input_system.GetWindowSize() / 2.0f + cursor_offset) % 1.0f;
        GUI.Box(new Rect(Screen.width * ScreenWidth * progress - 5 + ScreenPositionOffset * 2.0f * Screen.width * ScreenWidth, 0, 5, 50), "", currentStyle);
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
