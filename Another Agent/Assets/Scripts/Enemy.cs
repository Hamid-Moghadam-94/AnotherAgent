using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float xpGiven;
    public float startHP;
    public float currentHP;
    public Transform target;
    public float rotationDamping;
    public bool targedSpotted = false;
    private PlayerStats player;
    [Space]
    public float sphereRadius;
    public float maxDistance;
    private Vector3 origin;
    private Vector3 direction;
    public LayerMask layerMask;

    private float hitDistance;
    public GameObject currentHitObject;
    [Space]
    public float fireRateStart;
    private float fireRate;
    public Transform shootPoint;
    public GameObject impactEffect;
    public GameObject bloodEffect;
    public ParticleSystem muzzleFlash;
    public ParticleSystem smoke;
    public float startAmmo;
    private float ammoLeft;
    [Space]
    public float damage;
    [Space]
    public float stoppingDistance;
    //public float retreatDistance;
    private float distance;
    public float speed;
    private Rigidbody rb;
    private AudioSource _audio;
    

    
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ammoLeft = startAmmo;
        currentHP = startHP;
        StartCoroutine("FindPlayer");
        xpGiven = startHP / 10;
        _audio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
        origin = transform.position;
        direction = transform.forward;
        RaycastHit hit;
        if (Physics.SphereCast(origin,sphereRadius,direction, out hit, maxDistance, layerMask)){
            currentHitObject = hit.transform.gameObject;
            hitDistance = hit.distance;
        } else{
            hitDistance = maxDistance;
            currentHitObject = null;
        }
        if(currentHitObject != null){
        if(currentHitObject.tag == "Player"){
            targedSpotted = true;
        }
        }
        

        if(targedSpotted == true){
            lookAtPlayer();
        }


        if(currentHP <= 0){
            player.currentXp += xpGiven;
            Dead();
        }

        if(fireRate <= 0 && targedSpotted == true && ammoLeft > 0){
            Shoot();
            ammoLeft--;
            fireRate = fireRateStart;
        }

        if(fireRate > 0){ 
            fireRate -= Time.deltaTime;
        }
        if(ammoLeft <= 0){
            StartCoroutine("Reload");
        }
        
        if(targedSpotted == true){
            if(Vector3.Distance(transform.position, target.position) > stoppingDistance){
                rb.velocity = (transform.forward * speed);
            } else if (Vector3.Distance(transform.position, target.position) < stoppingDistance){
                transform.position = this.transform.position;
        }
    }
    }
    

    void lookAtPlayer(){
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    void Dead(){
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * hitDistance);
        Gizmos.DrawWireSphere(origin + direction * hitDistance,sphereRadius);
    }

    void Shoot(){
        muzzleFlash.Play();
        smoke.Play();
        _audio.Play();
        Ray ray = new Ray(shootPoint.position, shootPoint.forward);
        RaycastHit hit;
        

        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            
        }

        if(hit.collider.tag == "Player"){
                GameObject bloodGO = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                player.TakeDamage(damage);
                Destroy(bloodGO, 4f);
            }

            if(hit.collider.tag == "Wall"){
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO,4f);
        }

        if(hit.collider.tag == null){
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO,4f);
        }

    }

    IEnumerator Reload(){
        yield return new WaitForSeconds(Random.Range(2, 6));
            ammoLeft = startAmmo;
    }

    IEnumerator FindPlayer(){
        yield return new WaitForSeconds (5f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

}
