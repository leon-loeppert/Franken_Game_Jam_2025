using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 20f;
    void Update()
    {
        this.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime); 
    }
}
