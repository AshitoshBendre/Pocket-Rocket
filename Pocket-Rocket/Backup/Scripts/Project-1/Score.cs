using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    public ObjSpawner spawner;
    public int scrMul =2;
    public int scrCounter = 0;

    private void Start()
    {

        spawner = GameObject.FindGameObjectWithTag("Spn").GetComponent<ObjSpawner>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Obs"))
        {
            Debug.Log($"Collided with + {other.gameObject.name}, so here's extra bonusss!!!");
            scrCounter += scrMul;
            Destroy(other.gameObject);
            spawner.Spawn();
        }
        else if (other.gameObject.tag.Equals("Ball"))
        {
            Debug.Log("Ball Smashed");
            Destroy(other.gameObject);
            spawner.SpawnBall();
        }
        else
        {
            scrCounter++;
            Debug.Log($"You Bumped into {other.gameObject.name} this many times {scrCounter}");
            other.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
