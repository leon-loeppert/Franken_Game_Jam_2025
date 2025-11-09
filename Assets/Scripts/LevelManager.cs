using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject activeGameScreen;
    [SerializeField] private GameObject gameGridObject;
    [SerializeField] private GameObject lvlCompleteScreen;

    [SerializeField] private float waitTime = 3f;

    [SerializeField] private AudioClip successSound;
    [SerializeField] private float successVolume = 1;
    [SerializeField] private GameObject successFX;

    
    public void CompleteLevel()
    {
        StartCoroutine(ChangeScreen());

        //Play SoundFX & VFX
        SoundFXManager.instance.PlaySoundFXClip(successSound, transform, successVolume);
        successFX.SetActive(true);
    }

    IEnumerator ChangeScreen()
    {
        //Wait until SoundFX and VFX are complete
        yield return new WaitForSeconds(waitTime);

        //Disable game-relevant gameobjects
        activeGameScreen.SetActive(false);
        gameGridObject.SetActive(false);

        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            tile.gameObject.SetActive(false);
        }

        foreach (Item item in FindObjectsOfType<Item>())
        {
            item.gameObject.SetActive(false);
        }

        GameObject[] stringList = GameObject.FindGameObjectsWithTag("String");

        foreach (GameObject stringGO in stringList)
        {
            stringGO.SetActive(false);
        }

        //Enable level complete screen
        lvlCompleteScreen.SetActive(true);
    }
}
