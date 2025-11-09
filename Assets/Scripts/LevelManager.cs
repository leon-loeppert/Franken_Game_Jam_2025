using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject activeGameScreen;
    [SerializeField] private GameObject gameGridObject;
    [SerializeField] private GameObject lvlCompleteScreen;

    public void CompleteLevel()
    {
        activeGameScreen.SetActive(false);
        gameGridObject.SetActive(false);

        foreach (Tile tile in Resources.FindObjectsOfTypeAll(typeof(Tile)) as Tile[])
        {
            tile.gameObject.SetActive(false);
        }

        foreach (Item item in Resources.FindObjectsOfTypeAll(typeof(Item)) as Item[])
        {
            item.gameObject.SetActive(false);
        }

        lvlCompleteScreen.SetActive(true);
    }
}
