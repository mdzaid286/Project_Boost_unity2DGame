using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] AudioClip audioClip;
    AudioSource audioSource;
    [SerializeField] float MainThrust;
    [SerializeField] float RotationThrust;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessSpace();
        ProcessRotation();

    }

    void ProcessSpace(){

        if(Input.GetKey(KeyCode.Space)){
           rb.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);
              if(!audioSource.isPlaying){
                audioSource.PlayOneShot(audioClip);
              }
        }else{
            audioSource.Stop();
        }
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A)){
            Rotation(RotationThrust);
        }else if(Input.GetKey(KeyCode.D)){
           Rotation(-RotationThrust);
        }
    }
    void Rotation(float rotation ){

        rb.freezeRotation = true;
       transform.Rotate(Vector3.forward * rotation* Time.deltaTime);
        rb.freezeRotation = false;
    }  
}
