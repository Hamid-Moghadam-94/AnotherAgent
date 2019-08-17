using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject escapeMenu;
    private bool menuOn = false;
    public GameObject startScreen;
    private GameObject player;
    private PlayerStats chosenName;
    public Text agentName;

    void Start(){
        player = GameObject.Find("Player");
        chosenName = player.GetComponent<PlayerStats>();
        
        StartCoroutine("StartScene");
        
    }

    void Update(){
        agentName.text = ("Agent " + chosenName.namePicked);
        if(Input.GetButtonDown("EscapeMenu")){
            Resume();
        }
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("Quitting...... ( ͡° ͜ʖ ͡°)");
    }

    public void Resume(){
        escapeMenu.SetActive(!menuOn);
        menuOn = !menuOn;

    }

    public void Menu(){
        SceneManager.LoadScene(0);
    }

    IEnumerator StartScene(){
        startScreen.SetActive(true);
        player.SetActive(false);
        yield return new WaitForSeconds(5f);
        player.SetActive(true);
        startScreen.SetActive(false);

    }
}
