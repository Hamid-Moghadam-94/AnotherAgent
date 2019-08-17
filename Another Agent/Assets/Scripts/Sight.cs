using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    private LineRenderer lr;
    public LayerMask layerMask;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }


    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit,Mathf.Infinity, layerMask)){


            if(hit.collider){
                lr.SetPosition(1, new Vector3 (0, 0, (transform.InverseTransformPoint(hit.transform.position).z)));
            }
        } else {
            lr.SetPosition(1,new Vector3(0,0,5000));
        }
    }
}
