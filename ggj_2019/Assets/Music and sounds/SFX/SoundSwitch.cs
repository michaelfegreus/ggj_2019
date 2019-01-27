using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSwitch : MonoBehaviour {
    private bool inBox;
    public float fadeTime;
    public float volume;
    private float timer = 0.0f;
    public float waitTime;

    private void Start()
    {
        GetComponent<AudioSource>().volume = 1f;
        inBox = true;
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        inBox = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        inBox = false;
    }

    private void Update()
    {
        if (inBox)
        {
            while (GetComponent<AudioSource>().volume < volume)
            {
                GetComponent<AudioSource>().volume += .01f;
                timer += Time.deltaTime;
                timer = timer - waitTime;
            }
        }
        else
        {
            while (GetComponent<AudioSource>().volume > 0)
            {
                GetComponent<AudioSource>().volume -= .001f;
                timer += Time.deltaTime;
                timer = timer - waitTime;
            }
        }


        //if (inBox)
        //GetComponent<AudioSource>().volume = Mathf.Lerp(0f, volume, fadeTime);
        //else
        //GetComponent<AudioSource>().volume = Mathf.Lerp(volume, 0f, fadeTime);
    }
}
