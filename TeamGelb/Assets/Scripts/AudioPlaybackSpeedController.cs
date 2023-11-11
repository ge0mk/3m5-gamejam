using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlaybackSpeedController : MonoBehaviour
{
    public AudioMixerGroup pitchBendGroup;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / audioSource.pitch);
    }
}
