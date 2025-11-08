using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip buttonHoverFX;
    [SerializeField] private AudioClip buttonClickFX;
    [SerializeField] private float buttonHoverVolume = 1f;
    [SerializeField] private float buttonClickVolume = 1f;

    public void PlayHoverFX()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonHoverFX, transform, buttonHoverVolume);
    }

    public void PlayClickFX()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonClickFX, transform, buttonClickVolume);
    }
}
