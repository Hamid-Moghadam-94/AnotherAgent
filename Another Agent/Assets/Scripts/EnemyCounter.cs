using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour
{
    public int enemyCount;
    private Text counter;
    public GameObject winSplash;
    private float waitTime = 5f;
    

    void Start()
    {
        counter = GetComponent<Text>();
    }

    
    void Update()
    {
        waitTime -= Time.deltaTime;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        counter.text = ("Enemies left: " + enemyCount);

        if(enemyCount <= 0 && waitTime <= 0){
            StartCoroutine("winState");
        }

    }



    IEnumerator winState(){
        yield return new WaitForSeconds(5f);
        winSplash.SetActive(enabled);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
        }
}
