using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource backgroundMusic;
    public AudioClipValue BGMvalue;

    // Start is called before the first frame update
    void Start()
    {
        ChangeBGMScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Change audio when location change in same scene
    public void ChangeBGM(AudioClip music)
    {
        if (backgroundMusic.clip != music)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = music;
            backgroundMusic.Play();
        }
    }

    // Change audio when scene transition
    public void ChangeBGMScene()
    {
        if (backgroundMusic.clip != BGMvalue)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = BGMvalue.initialValue;
            backgroundMusic.Play();
        }
    }
}
