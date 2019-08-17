using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour

{
    private GameObject player;
    public GameObject deathSplash;

    void Start(){
        player = GameObject.Find("Player");
    }


    public IEnumerator playerDead(){
       // player.SetActive(false);
        deathSplash.SetActive(true);
        yield return new WaitForSeconds(5f);
        Debug.Log("Restarting....");
        SceneManager.LoadScene(1);

    }
}
