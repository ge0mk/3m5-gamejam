using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public float bpm = 40.0f;
    public float windowSize = 0.5f;
    public float beatOffset = 0.0f;

    public AudioMixerGroup pitchBendGroup;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / audioSource.pitch);
    }

    public void StartAudio()
    {
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public float CurrentTime()
    {
        return audioSource.time;
    }

    public float GetSecondsPerBeat()
    {
        return 1.0f / (bpm / 60.0f);
    }

    public float GetProgress()
    {
        return audioSource.time % GetSecondsPerBeat();
    }

    public float GetWindowSize()
    {
        return windowSize;
    }
}
