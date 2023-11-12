using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Gianni.Helper;
using UnityEngine;

public class Menue : MonoBehaviour
{
    public GameObject Player;
    public float dollySpeed;
    public CinemachineVirtualCamera LevelCam;
    private CinemachineTrackedDolly dolly;
    // Start is called before the first frame update
    void Start()
    {
        CinemachineBrain.SoloCamera = LevelCam; // to set it
        dolly = LevelCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        
    }

    // Update is called once per frame
    void Update()
    {
        dolly.m_PathPosition += dollySpeed * Time.deltaTime;
    }
    public void ButtonEventStart()
    {
        Player.SetActive(true);
        gameObject.SetActive(false);
        CinemachineBrain.SoloCamera = null; // to clear it
    }
}
