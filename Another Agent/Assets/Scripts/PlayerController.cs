using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody rb;
    

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;
    public AssaultRifle rifle;
    private PlayerStats player;
    public GameObject _flash;
    public float throwForce;
    public Transform throwPoint;
    public AudioClip reloadSound;
    public AudioClip flashThrowSound;
    private AudioSource _audio;
    public bool useController;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();  
        player = GetComponent<PlayerStats>();
    }

    
    void Update()
    {
       moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * movementSpeed;

        //Rotate with mouse
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

        if(groundPlane.Raycast(cameraRay,out rayLength)){
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }  

        //Rotate with controller


            Vector3 playerDirection = Vector3.right * Input.GetAxis("RHorizontal") + Vector3.forward * -Input.GetAxis("RVertical");
            if(playerDirection.sqrMagnitude > 0.0f){
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }


        if(((Input.GetButton("Fire1") || (Input.GetAxis("GamePad_RT") != 0))  && rifle.ammo > 0 && rifle.fireRate <= 0)){
            rifle.ammo--;
            rifle.fireRate = rifle.fireRateStart;
            rifle.Shoot();
            
        }
        if(Input.GetButtonDown("Reload")){
            StartCoroutine("Reload");
            
        }
      

        if(Input.GetButtonDown("FlashBang") && player.grenadeAmount > 0){
            ThrowFlashBang();
            player.grenadeAmount--;
        }

    }

    void FixedUpdate(){
        rb.velocity = moveVelocity;
    }

    void ThrowFlashBang(){
        GameObject flashbang = Instantiate(_flash, throwPoint.position, Quaternion.identity);
        _audio.clip = flashThrowSound;
        _audio.Play();
        Rigidbody rb = flashbang.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

    }

    IEnumerator Reload(){
        _audio.clip = reloadSound;
        _audio.Play();
        yield return new WaitForSeconds(2f);
        rifle.ammo = rifle.ammoStart;
    }

}
