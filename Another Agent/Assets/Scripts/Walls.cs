using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public Transform player;
    private Camera mainCamera;
    
    
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(transform.position);
        RaycastHit hit;
        float distance = 50;

        if(Physics.Raycast(ray, out hit, distance)){
            distance = hit.distance;
        }

        Debug.DrawLine(ray.origin, transform.position, Color.blue);
        
        if(hit.collider.tag == "Wall"){
            Debug.Log("Obstruction " + hit.collider.gameObject.name);


        }
       
    }
}
