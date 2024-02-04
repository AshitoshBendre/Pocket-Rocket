using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField,Range(0,1)] float movementFactor;
    [SerializeField,Range(1,100)] float period = 2f;
    void Start()
    {
        startingPosition = transform.position;        
    }

    void Update()
    {
        //Returns if period 0 to eliminate Nan error
        // if(period == 0) { return; } <- This code cannot be used with updating float values
        if (period <= Mathf.Epsilon) { return; }

        //Grows continually over time
        float cycles = Time.time / period;
       
        //Constant value of 6.283
        const float tau = Mathf.PI * 2;
        //Oscillates from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);

        //Debug.Log(rawSinWave);

        //Recalculated to go from 0 to 1
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
    }
}
