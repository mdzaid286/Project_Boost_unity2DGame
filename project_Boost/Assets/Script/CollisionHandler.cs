using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHnadler : MonoBehaviour
{

    [SerializeField] public float loadlevel = 2f;
     [SerializeField] AudioClip success;
     [SerializeField] AudioClip crash;

    AudioSource audioSource;

    bool isPlaying = false;

    void Start(){

        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) {
    
        if(isPlaying){
            return;
        }
        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartNextScene();
                break;
            default:
                StartCrashSequences();
                break;
        }
   }

    private void StartNextScene()
    {  
        isPlaying = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", loadlevel);
    }

    public void StartCrashSequences(){
        
        isPlaying = true;
        audioSource.Stop();
          audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", loadlevel);
    }
    
   public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextscene = currentSceneIndex + 1;
        if (nextscene == SceneManager.sceneCountInBuildSettings)
        {
            nextscene = 0;
        }
        SceneManager.LoadScene(nextscene);
    }
}

