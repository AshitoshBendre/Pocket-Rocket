using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    public GameObject cube;
    public GameObject ball;

    public GameObject[] spawnPoints;
    public GameObject[] spawnPointsBall;
    MeshRenderer mr;
    Rigidbody rb;
    public int waitTime = 3;
    bool trig = false;
    // Start is called before the first frame update
    void Start()
    {
        mr = cube.GetComponent<MeshRenderer>();
        rb = cube.GetComponent<Rigidbody>();
        mr.enabled = false;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > waitTime && trig == false)
        {
            Debug.Log("Dropping Cubes");
            mr.enabled = true;
            rb.useGravity = true;
            Spawn();
            SpawnBall();
            trig = true;
        }
    }

    public void Spawn()
    {
        int i = Random.Range(0, spawnPoints.Length);
        Instantiate(cube, spawnPoints[i].transform.position,Quaternion.identity);
    }

    public void SpawnBall()
    {
        int i = Random.Range(0, spawnPointsBall.Length);
        Instantiate(ball, spawnPointsBall[i].transform.position, Quaternion.identity);
    }
}
