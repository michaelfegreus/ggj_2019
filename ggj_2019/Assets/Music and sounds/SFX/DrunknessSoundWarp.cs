using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunknessSoundWarp : MonoBehaviour {

    AudioSource audioSource;
    public int DrunkStat;
    private float WorkingDrunkLevel;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 1;
    }
	
	// Update is called once per frame
	void Update () {
        WorkingDrunkLevel = (float)DrunkStat;
        audioSource.pitch = 1.0f - (0.07f * WorkingDrunkLevel);
    }
}
