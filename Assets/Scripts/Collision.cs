using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successparticles;
    [SerializeField] ParticleSystem crashparticles;

    AudioSource audioSource;
    bool istransitioning = false;
    bool isCollisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RespondToDebugkeys();
    }
    void RespondToDebugkeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisable = !isCollisionDisable;
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(istransitioning || isCollisionDisable)
        {
            return;
        }
        switch(collision.gameObject.tag)
        {
            case"Friendly":
                Debug.Log("This thing is friendly");
                break;
            case"Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        istransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successparticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        istransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashparticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay);
    }
    void LoadNextLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = CurrentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }
    void ReloadLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }
}
