using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float xAngle=1f;
    [SerializeField] float yAngle=1f;
    [SerializeField] float zAngle=1f;
    void Update()
    {
        transform.Rotate(xAngle, yAngle, zAngle);      
    }
}
