using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public bool isValid { get; set; }

    public AudioController audioController;

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

    private float prevTime = 0.0f;
    private float currentTime = 0.0f;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        currentTime = audioController.CurrentTime();
        if (currentTime < prevTime)
        { // song restarted
            prevTime = 0f;
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
            prevTime += GetSecondsPerBeat();
            UpdateDeltas();
            ClearInputs();
        }
    }

    public float GetSecondsPerBeat()
    {
        return audioController.GetSecondsPerBeat();
    }

    public float TimeSinceLastUpdate()
    {
        return (currentTime + audioController.beatOffset * GetSecondsPerBeat()) - prevTime;
    }

    public float GetProgress()
    {
        return TimeSinceLastUpdate() / GetSecondsPerBeat();
    }

    public float GetWindowSize()
    {
        return audioController.windowSize;
    }

    bool InsideInputWindow()
    {
        return TimeSinceLastUpdate() > GetSecondsPerBeat() - GetWindowSize() && TimeSinceLastUpdate() <= GetSecondsPerBeat();
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
