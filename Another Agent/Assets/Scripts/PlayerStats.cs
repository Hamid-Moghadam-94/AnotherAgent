using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int playerLevel = 1;
    public float currentXp;
    public float xpToLevel;
    private float prevXPneeded = 100;
    public Slider xpSlider;
    public Text xpText;
    [Space]
    private float maxHealth = 100;
    public float currentHealth;
    public float maxArmour;
    public float currentArmour;
    public Slider armourSlider;
    public Slider healthSlider;
    public Text armourText;
    public Text healthText;
    [Space]
    public int grenadeAmount;
    public Text grenadeText;
    
    [Space]
    public TextAsset names;
    public string[] nameLine;
    public Text agentName;
    public string namePicked;
    [Space]
    public RawImage healthEffect;
    public RawImage armourEffect;
    [Space]
    public Death death;
    [Space]
    private AudioSource _audio;
    public AudioClip hitSound;
    
    
    
    void Start()
    {
        currentArmour = maxArmour;
        armourSlider.maxValue = maxArmour;
        armourSlider.value = currentArmour;
        armourText.text = (currentArmour + " Armour");
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text = (currentHealth + " Health");

        nameLine = ( names.text.Split( '\n' ) );
        namePicked = nameLine[Random.Range(0,1043)];
        agentName.text = ("Agent " + namePicked);
        _audio = GetComponent<AudioSource>();

    }

    
    void Update()
    {
        xpToLevel = prevXPneeded + prevXPneeded * 10/100;
        xpSlider.maxValue =  xpToLevel;
        xpSlider.value = currentXp;
        //
        grenadeText.text = ("FlashBangs: " + grenadeAmount);
        //
        xpText.text = ("Level: " + playerLevel);
        //
        healthSlider.value = currentHealth;
        armourSlider.value = currentArmour;
        //
        armourText.text = (currentArmour + " Armour");
        healthText.text = (currentHealth + " Health");
        //
        if( currentXp >= xpToLevel){
            prevXPneeded = xpToLevel;
            currentXp = 0;
            playerLevel++;
        }
        if(currentArmour <= 0){
            armourText.text = ("Armour Broken");
        } else{
            armourText.text = (currentArmour + " Armour");
        }

        if(currentHealth <= 0){
            die();
        }
    }

    public void TakeDamage(float damage){
        if (currentArmour > 0){
            StartCoroutine("armourDamage");
            currentArmour -= damage;

        } else if (currentHealth >= 0){
            StartCoroutine("healthDamage");
            currentHealth -= damage;
        } 
    }

    IEnumerator armourDamage(){
        armourEffect.enabled = true;
        _audio.clip = hitSound;
        _audio.Play();
        yield return new WaitForSeconds(0.1f);
        armourEffect.enabled = false;
    }

    IEnumerator healthDamage(){
        healthEffect.enabled = true;
        _audio.clip = hitSound;
        _audio.Play();
        yield return new WaitForSeconds(0.1f);
        healthEffect.enabled = false;
    }

    void die(){
        StartCoroutine(death.playerDead());
    }

}
