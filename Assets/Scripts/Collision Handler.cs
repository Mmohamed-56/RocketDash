using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    bool isControlleable = true;
    bool isCollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isControlleable = true;
    }

    private void Update()
    {
        respondToDebugKeys();
    }

    private void OnCollisionEnter(Collision other)
    {   
        if(!isControlleable || !isCollidable)
        {
            return;
        }


        switch(other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void LoadNextLevel()
    {

        stopAudio();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
          nextSceneIndex = 0; 
        }

        SceneManager.LoadScene(nextSceneIndex); 
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void StartCrashSequence()
    {
        //TODO: Add particle effect here and audio here
        stopAudio();
        playAudio(crashSFX);
        if(crashParticles != null)
        {
            crashParticles.Play();
        }
        DisableControls();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        //TODO: Add particle effect here and audio here
        stopAudio();
        playAudio(successSFX);
        if(successParticles != null)
        {
            successParticles.Play();
        }
        DisableControls();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void DisableControls()
    {
        GetComponent<Movement>().enabled = false;
        isControlleable = false;
    }

    private void stopAudio()
    {
        if(audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private void playAudio(AudioClip audioClip)
    {
        if(audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void respondToDebugKeys()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }
}
