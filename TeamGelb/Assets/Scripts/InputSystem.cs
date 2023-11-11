using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public float bpm = 40.0f;
    public float window_size = 0.5f;

    private float timer = 0.0f;
    private float forward_delta = 0.0f;
    private float horizontal_delta = 0.0f;

    private bool ignore_w = false;
    private bool ignore_a = false;
    private bool ignore_s = false;
    private bool ignore_d = false;

    private bool w, a, s, d;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!InsideInputWindow()) {
            UpdateIgnoredInputs();
        } else {
            UpdateInputs();
        }

        if (timer > GetSecondsPerBeat()) {
            timer -= GetSecondsPerBeat();
            UpdateDeltas();
            ClearInputs();
        }
    }

    float GetSecondsPerBeat()
    {
        return 1.0f / (bpm / 60.0f);
    }

    bool InsideInputWindow()
    {
        return timer > GetSecondsPerBeat() - window_size && timer <= GetSecondsPerBeat();
    }

    void UpdateIgnoredInputs()
    {
        ignore_w = Input.GetKey("w");
        ignore_a = Input.GetKey("a");
        ignore_s = Input.GetKey("s");
        ignore_d = Input.GetKey("d");
    }

    void UpdateInputs()
    {
        w |= Input.GetKey("w") && !ignore_w;
        a |= Input.GetKey("a") && !ignore_a;
        s |= Input.GetKey("s") && !ignore_s;
        d |= Input.GetKey("d") && !ignore_d;
    }

    void UpdateDeltas()
    {
        float accelerate = w ? 1.0f : 0.0f;
        float left = a ? 1.0f : 0.0f;
        float brake = s ? 1.0f : 0.0f;
        float right = d ? 1.0f : 0.0f;

        forward_delta = accelerate - brake;
        horizontal_delta = right - left;
    }

    void ClearInputs()
    {
        ignore_w = false;
        ignore_a = false;
        ignore_s = false;
        ignore_d = false;

        w = false;
        a = false;
        s = false;
        d = false;
    }

    float GetForwardDelta()
    {
        return forward_delta;
    }

    float GetHorizontalDelta()
    {
        return horizontal_delta;
    }

    void OnGUI() {
        GUI.color = Color.black;
        GUI.Box(new Rect(0, 0, Screen.width * timer / GetSecondsPerBeat(), 50), "");
        GUI.Box(new Rect(Screen.width - Screen.width * window_size / GetSecondsPerBeat(), 0, Screen.width * window_size / GetSecondsPerBeat(), 50), "");
        GUI.Label(new Rect(0, 50, 200, 100), "" + forward_delta + " | " + horizontal_delta);
    }
}
