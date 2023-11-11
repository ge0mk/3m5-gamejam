using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private InputSystem input_system;

    // Start is called before the first frame update
    void Start() {
        input_system = GetComponent<InputSystem>();
    }

    // Update is called once per frame
    void Update() {}

    void OnGUI() {
        float window_width = Screen.width * input_system.window_size / input_system.GetSecondsPerBeat();
        float window_left_edge = Screen.width / 2.0f - window_width / 2.0f;
        GUI.Box(new Rect(window_left_edge, 0, window_width, 50), "");

        float progress = (input_system.GetProgress() + 0.5f + input_system.window_size / 2.0f) % 1.0f;
        GUI.Box(new Rect(Screen.width * progress - 5, 0, 5, 50), "");
    }
}
