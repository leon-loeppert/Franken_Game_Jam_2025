using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public bool audioMuted;
    public float currentMasterVol;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        currentMasterVol = level;
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
    }

    public void MuteUnmuteMasterVolume()
    {
        print("button click");
        if (!audioMuted)
        {
            print("mute that thang");
            audioMixer.SetFloat("masterVolume", -80);
            audioMuted = true;
        }

        else
        {
            print("lets hear that thang");
            audioMixer.SetFloat("masterVolume", Mathf.Log10(currentMasterVol) * 20f);
            audioMuted = false;
        }
        
    }
}
