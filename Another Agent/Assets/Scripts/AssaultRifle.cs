using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssaultRifle : MonoBehaviour
{

    public Transform shootPoint;
    public ParticleSystem muzzleFlash;
    public ParticleSystem smoke;
    public GameObject impactEffect;
    public GameObject bloodEffect;
    public float ammoStart;
    public float ammo;
    public Text ammoText;
    public float damage;
    public float critChance = 10;
    public float fireRateStart = 0.5f;
    public float fireRate;
    private AudioSource _audio;
    public AudioClip gunShotSound;
    public AudioClip noAmmoSound;

    private Enemy enemy;

    void Start(){
        
        _audio = GetComponent<AudioSource>();
        ammo = ammoStart;
        ammoText.text = ("Ammo: " + ammo + " / " + ammoStart);
    }

    void Update()
    {
        if(ammo > 0){
        ammoText.text = ("Ammo: " + ammo + " / " + ammoStart);
        }else{
            ammoText.text = ("RELOAD!");
        }
        if(fireRate > 0)
        fireRate -= Time.deltaTime;

        if(Input.GetButtonDown("Fire1") && ammo <= 0){
            _audio.clip = noAmmoSound;
            _audio.Play();
        }
    }

    public void Shoot(){



        muzzleFlash.Play();
        smoke.Play();
        _audio.clip = gunShotSound;
        _audio.Play();
        Ray ray = new Ray(shootPoint.position, shootPoint.forward);
        RaycastHit hit;

        float shotDistance = 50;

        if( Physics.Raycast(ray,out hit, shotDistance)){
            shotDistance = hit.distance;
        }

        if(hit.collider.tag == "Enemy"){
            Debug.Log("Hit: " + hit.collider.gameObject.name);
           GameObject bloodGO = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
           enemy = hit.collider.GetComponent<Enemy>();
           if(enemy.targedSpotted == false){
           enemy.targedSpotted = true;
           }
           DamageCalc();
           Destroy(bloodGO,4f);
        }

        if(hit.collider.tag == "Wall"){
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO,4f);
        }

        if(hit.collider.tag == null){
            //DO NOTHING
        }
        
        

    }

    void DamageCalc(){
        float randValue = Random.Range(0,100);
        if(randValue <= critChance){
            enemy.currentHP -= damage * 2;
            Debug.Log("CRIT");
        } else{
            enemy.currentHP -= damage;
        }
    }

    
}
