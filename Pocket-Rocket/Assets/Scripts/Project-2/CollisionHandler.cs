using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Parameters
    int currentSceneIndex;
    int nextSceneIndex;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;
    [Range(0f,10f)]
    public float delay = 3f;

    //States

    // is Transitioning is used to disable controls when we can changing levels
    bool isTransitioning = false;
    bool collisionDisable = false;

    //Cache
    RocketMovement rm;
    AudioSource ads;

    void Start()
    {
        rm = GetComponent<RocketMovement>();
        ads = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            nextScene();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            //Toggles Collision
            collisionDisable = !collisionDisable;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable) { return; }
        switch (other.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("Start Point");
                break;
            case "Finish":
                Debug.Log("Congrats, you finished!!");
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("You picked up fuel");
                break;
            default:
                StartCrashSequence(); 
                break;
        }
    }

    public void reloadScene()
    {
        Debug.Log("You Crashed!!");

        //We can directly use Scene build number to LoadScence
        //SceneManager.LoadScene(0);

        //But we can reuse the same method by making it dynamic
        //We can get BuildIndex using GetActiveScene
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void nextScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Game Ended Reloading First Level");
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void StartCrashSequence()
    {
        isTransitioning = true;
        ads.Stop();
        deathParticle.Play(); 
        ads.PlayOneShot(deathSound);
        rm.enabled = false;
        Invoke("reloadScene",delay);
    }

    public void StartSuccessSequence()
    {
        isTransitioning = true;
        ads.Stop();
        successParticle.Play();
        ads.PlayOneShot(successSound);
        rm.enabled = false;
        Invoke("nextScene", delay);
    }
}
