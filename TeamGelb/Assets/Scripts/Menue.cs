using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Gianni.Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menue : MonoBehaviour
{
    public GameObject Player1, Player2;
    public float dollySpeed;
    public CinemachineVirtualCamera LevelCam;
    private CinemachineTrackedDolly dolly;
    public TMP_Text GoalText;
    public GameObject MainUI;

    public AudioSource mainMenuAudio;
    public AudioSource finishAudio;
    public AudioController mainAudioController;

    // Start is called before the first frame update
    void Start()
    {
        CinemachineBrain.SoloCamera = LevelCam; // to set it
        dolly = LevelCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        GoalText.gameObject.SetActive(false);

        mainMenuAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        dolly.m_PathPosition += dollySpeed * Time.deltaTime;
    }

    public void ButtonEventStart()
    {
        mainMenuAudio.Stop();

        Player1.SetActive(true);
        Player2.SetActive(true);
        MainUI.SetActive(false);
        CinemachineBrain.SoloCamera = null; // to clear it

        mainAudioController.StartAudio();
    }

    public void Goal(Transform player)
    {
        mainAudioController.StopAudio();

        GoalText.gameObject.SetActive(true);
        LevelCam.LookAt = player;
        GoalText.text += " " + player.name;
        Player1.SetActive(false);
        Player2.SetActive(false);
        CinemachineBrain.SoloCamera = LevelCam;

        finishAudio.Play();
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
