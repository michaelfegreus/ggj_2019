using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class DrunkCamera : MonoBehaviour {

    int DrunkLevel = 0;
    private float WorkingDrunkLevel;
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Use this for initialization
    void Start () {
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
		DrunkLevel = GAME_manager.Instance.globalVariables.alcoholPoints;

                float WorkingDrunkLevel = (float)DrunkLevel;
                WorkingDrunkLevel = WorkingDrunkLevel * .13f;
                virtualCameraNoise.m_AmplitudeGain = WorkingDrunkLevel;
                virtualCameraNoise.m_FrequencyGain = WorkingDrunkLevel;
    }
}