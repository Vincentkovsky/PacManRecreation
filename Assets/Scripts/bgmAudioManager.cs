using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmAudioManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip[] clips;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if(!bgm.isPlaying){
            bgm.clip = clips[1];
            bgm.Play();
        }
    }
}
