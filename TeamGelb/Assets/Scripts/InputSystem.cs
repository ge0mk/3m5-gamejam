using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public float bpm = 40.0f;
    public float window_size = 0.5f;

    public bool isValid { get; set; }

    private AudioSource audio_source;

    private float forward_delta = 0.0f;
    private float horizontal_delta = 0.0f;

    private bool ignore_w = false;
    private bool ignore_a = false;
    private bool ignore_s = false;
    private bool ignore_d = false;

    private bool w, a, s, d;

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
        return current_time - prev_time;
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
