using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MenuHandler : MonoBehaviour
{
    //Parameters
    public TextMeshProUGUI fuelText;

    //Cache
    RocketMovement rm;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindWithTag("Player").GetComponent<RocketMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFuelText();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Exiting Game");
            Application.Quit();
        }
    }

    private void UpdateFuelText()
    {
        fuelText.text = $"Fuel Capacity:" + rm.currentFuelCapacity.ToString();
    }
}
