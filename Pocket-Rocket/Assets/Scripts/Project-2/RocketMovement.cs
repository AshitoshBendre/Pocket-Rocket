using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    //Parameters
    [SerializeField] ParticleSystem thrustParticles;
    
    //Thrustrad is used to set radius of secondary thrust particle system
    [SerializeField] float thrustrad = 0.8f;
    
    [SerializeField] ParticleSystem stlParticles;
    [SerializeField] ParticleSystem strParticles;
    [SerializeField] AudioClip engineThrust;
    [SerializeField, Range(100f, 1000f)]
    float thrust = 1f;
    [SerializeField, Range(100f, 1000f)]
    float secthrust = .5f;
    [SerializeField, Range(1f, 100f)]
    float rotateSpeed = 1f;

    //Cache
    AudioSource ads;
    Rigidbody rb;
    CollisionHandler ch;

    //State
    
    // radMod is used to get the ShapeModule from the ParticleSystem
    private ParticleSystem.ShapeModule radMod;
    
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        ads = GetComponent<AudioSource>();
        ch = GetComponent<CollisionHandler>();
        radMod = thrustParticles.shape;
        ads.Stop();
    }

    void Update()
    {
        processInput();
        processThrust();
    }

    public void processInput()
    {
        float rotateInput = Input.GetAxis("Horizontal");
        applyRotation(rotateInput);
    }

    public void processThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            
            // Checks Whether there is already the same audio file running
            if (!ads.isPlaying) 
            {
                Debug.Log("Main Thrust");
                ads.PlayOneShot(engineThrust);
                //ads.Play();
            }
            
            // Checks Wheter there is already the same particle system running
            if(!thrustParticles.isPlaying)
            { thrustParticles.Play(); }
        }
        else if (Input.GetKey(KeyCode.X))
        {
            rb.AddRelativeForce(Vector3.up * secthrust * Time.deltaTime);
            
            // Checks Whether there is already the same audio file running
            if (!ads.isPlaying)
            {
                Debug.Log("Secondary Thrust");
                ads.pitch = 0.5f;
                ads.PlayOneShot(engineThrust);
                //ads.Play();
            }
            
            // Checks Wheter there is already the same particle system running
            if (!thrustParticles.isPlaying)
            { 
                thrustParticles.Play(); 
                radMod.radius = thrustrad;
            }
        }
        else
        {
            // Stops all audios and particle systems
            thrustParticles.Stop();
            ads.Stop();

            // Reverts the changes applied for the secondaryThrust
            ads.pitch = 1;
            radMod.radius = 0.45f;
        }
    }

    public void applyRotation(float ipt)
    {
        rb.freezeRotation = true;
        transform.Rotate(0, 0, -1 * ipt * rotateSpeed * Time.deltaTime);

        //This if-else statements checks the ipt which is the rotationInput from ProcessInput Methods
        // and checks rotation direction and plays or stops side thrust Particle Systems
        if(ipt> 0 && !stlParticles.isPlaying)
        {
            stlParticles.Play();
        }
        else if (ipt<0 && !strParticles.isPlaying)
        {
            strParticles.Play();
        }
        else
        {
            stlParticles.Stop();
            strParticles.Stop();
        }
        rb.freezeRotation = false;
    }
}
