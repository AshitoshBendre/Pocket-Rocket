using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Range(0, 10)]
    public float speed = 0;
    // [Range(-1, 1)]
    // public float xValue = 0f;

    // [Range(-1, 1)]
    // public float yValue = 0f;

    // [Range(-1, 1)]
    // public float zValue = 0f;

    void Start()
    {
        Debug.Log("Script Started");
    }

    // Update is called once per frame
    void Update()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(xValue, 0, zValue);
    }
}
