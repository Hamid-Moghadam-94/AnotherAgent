using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    //Important script used on the entry room so no other room will spawn ontop of it
    void OnTriggerEnter(Collider other){
        Debug.Log("Room Collider " + other.gameObject.name);
        Destroy(other.gameObject);
    }
}
