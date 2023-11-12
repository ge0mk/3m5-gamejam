using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public float bpm = 40.0f;
    public float window_size = 0.5f;
    public float beat_offset = 0.0f;

    public bool isValid { get; set; }

    private AudioSource audio_source;

    private float forward_delta = 0.0f;
    private float horizontal_delta = 0.0f;

    public KeyCode UpInput;
    public KeyCode DownInput;
    public KeyCode LeftInput;
    public KeyCode RightInput;

    private bool ignore_up = false;
    private bool ignore_left = false;
    private bool ignore_down = false;
    private bool ignore_right = false;

    private bool up, left, down, right;

    private float prev_time = 0.0f;
    private float current_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        current_time = audio_source.time;
        if (current_time < prev_time)
        { // song restarted
            prev_time = 0f;
        }

        bool isValid = InsideInputWindow();
        if (!isValid)
        {
            UpdateIgnoredInputs();
        }
        else
        {
            UpdateInputs();
        }
        GiveInputFeedback(isValid);

        if (TimeSinceLastUpdate() > GetSecondsPerBeat())
        {
            prev_time += GetSecondsPerBeat();
            UpdateDeltas();
            ClearInputs();
        }
    }

    public float GetSecondsPerBeat()
    {
        return 1.0f / (bpm / 60.0f);
    }

    public float TimeSinceLastUpdate()
    {
        return (current_time + beat_offset * GetSecondsPerBeat()) - prev_time;
    }

    public float GetProgress()
    {
        return TimeSinceLastUpdate() / GetSecondsPerBeat();
    }

    bool InsideInputWindow()
    {
        return TimeSinceLastUpdate() > GetSecondsPerBeat() - window_size && TimeSinceLastUpdate() <= GetSecondsPerBeat();
    }

    void UpdateIgnoredInputs()
    {
        ignore_up = Input.GetKey(UpInput);
        ignore_left = Input.GetKey(LeftInput);
        ignore_down = Input.GetKey(DownInput);
        ignore_right = Input.GetKey(RightInput);
    }

    void UpdateInputs()
    {
        up |= Input.GetKey(UpInput) && !ignore_up;
        left |= Input.GetKey(LeftInput) && !ignore_left;
        down |= Input.GetKey(DownInput) && !ignore_down;
        right |= Input.GetKey(RightInput) && !ignore_right;
    }

    void UpdateDeltas()
    {
        float accelerate = up ? 1.0f : 0.0f;
        float left = this.left ? 1.0f : 0.0f;
        float brake = down ? 1.0f : 0.0f;
        float right = this.right ? 1.0f : 0.0f;

        forward_delta = accelerate - brake;
        horizontal_delta = right - left;
    }

    void ClearInputs()
    {
        ignore_up = false;
        ignore_left = false;
        ignore_down = false;
        ignore_right = false;

        up = false;
        left = false;
        down = false;
        right = false;
    }

    public float GetForwardDelta()
    {
        var result = forward_delta;
        forward_delta = 0;
        return result;
    }

    public float GetHorizontalDelta()
    {
        var result = horizontal_delta;
        horizontal_delta = 0;
        return result;
    }

    private void GiveInputFeedback(bool isValid)
    {
        this.isValid = isValid;
    }
}
