using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thurstStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSFX; 
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        processThrust();
        processRotation();
    }

    private void processThrust()
    {
        if(thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrustingComponents();
        }
    }

    private void processRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            applyRotation(rotationStrength);
            ActivateRightThruster();
        }
        else if(rotationInput > 0)
        {
            applyRotation(-rotationStrength);
            ActivateLeftThruster();
        }else
        {
            StopSideThrusters();
        }
    }

    private void applyRotation(float rotationThisFrame)
    {   
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thurstStrength * Time.fixedDeltaTime);
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if(!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrustingComponents()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ActivateRightThruster()
    {
        if(!rightThrustParticles.isPlaying)
        {   
            leftThrustParticles.Stop();
            rightThrustParticles.Play();
        }
    }

    private void ActivateLeftThruster()
    {
        if(!leftThrustParticles.isPlaying)
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Play();
        }
    }

    private void StopSideThrusters()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }
}
