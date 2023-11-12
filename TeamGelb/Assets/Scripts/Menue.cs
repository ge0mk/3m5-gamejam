using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Gianni.Helper;
using UnityEngine;
using UnityEngine.UI;

public class Menue : MonoBehaviour
{
    public GameObject Player;
    public float dollySpeed;
    public CinemachineVirtualCamera LevelCam;
    private CinemachineTrackedDolly dolly;
    public GameObject GoalText;
    public GameObject MainUI;
    // Start is called before the first frame update
    void Start()
    {
        CinemachineBrain.SoloCamera = LevelCam; // to set it
        dolly = LevelCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        GoalText.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        dolly.m_PathPosition += dollySpeed * Time.deltaTime;
    }
    public void ButtonEventStart()
    {
        Player.SetActive(true);
        MainUI.SetActive(false);
        CinemachineBrain.SoloCamera = null; // to clear it
    }
    public void Goal()
    {
        GoalText.SetActive(true);
        LevelCam.LookAt = Player.transform;
        CinemachineBrain.SoloCamera = LevelCam;
    }
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
