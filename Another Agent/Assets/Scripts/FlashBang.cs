using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBang : MonoBehaviour
{
    public float waitTime;
    public ParticleSystem flash;
    private bool used;
    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0 && used == false){
            flash.Play();
            used = true;
            _audio.Play();
        }
    }
}
