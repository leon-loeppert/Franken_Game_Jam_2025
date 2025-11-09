using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBracelet : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemOrder;
    //[SerializeField] private GridManager gridScript;

    [SerializeField] private Transform socketOne;
    [SerializeField] private Transform socketTwo;
    [SerializeField] private Transform socketThree;
    [SerializeField] private Transform socketFour;

    private void OnEnable()
    {
        //assign list through gridmanager

        foreach(GameObject itemGO in itemOrder)
        {
            itemGO.GetComponent<RotateObject>().stopRotate = true;
            //item.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            itemGO.transform.Find("BeadTransform").transform.localRotation = new Quaternion(0f,0f,0f,0f);
            itemGO.transform.Find("BeadTransform").transform.localScale = new Vector3(1f, 1f, 1f);
        }

        Instantiate(itemOrder[0], socketOne);
        Instantiate(itemOrder[1], socketTwo);
        Instantiate(itemOrder[2], socketThree);
        Instantiate(itemOrder[3], socketFour);
    }
}
