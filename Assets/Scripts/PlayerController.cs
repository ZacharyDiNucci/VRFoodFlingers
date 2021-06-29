using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean fire;
    public float speed = 1;
    public SteamVR_Action_Skeleton SkeletonAction;
    private bool indexUp;
    private bool middleUp;
    private bool ringUp;
    private bool pinkyUp;
    public int health = 100;
    public int Fammo = 10;
    public int Wammo = 10; 
    public GameObject hand;
    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public GameObject shot4;
    public float Timer = 3.2f;
    public float TimeReset = 3.2f;
    public bool runTimer = false;
    public GameObject canvas;
    public bool destroyed = false;
    public GameObject game;
    public bool handsCollider = false;
    public GameObject gameController;
    public GameMode gameMode;
    Collider m_Collider;
    string sceneName;
    [SerializeField]
    private Text fammoText;
    [SerializeField]
    private Text wammoText;
    public Character character;
    float range = 20f;
    float up = -10f;
    float scale =5.0f;
    
    
    void Start(){
        m_Collider = GetComponent<Collider>();
        
        Debug.Log(sceneName);
        
    }           

    //Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "menu") {hand.GetComponent<SphereCollider>().enabled = false;  }
        else if (SceneManager.GetActiveScene().name == "Level 1") {
            hand.GetComponent<SphereCollider>().enabled = true;

        }
        Skeleton();
    }

    private void Skeleton(){
        if (SkeletonAction.indexCurl <= .2){
            indexUp = true;
        }
        else {
            indexUp = false;
        }
        if (SkeletonAction.middleCurl <= .2){
            middleUp = true;
        }
        else {
            middleUp = false;
        }
        if (SkeletonAction.ringCurl <= .2){
            ringUp = true;
        }
        else{
            ringUp = false;
        }
        if (SkeletonAction.pinkyCurl <= .2){
            pinkyUp = true;
        }
        else{
            pinkyUp = false;
        }

        if(indexUp== true & middleUp == true && ringUp == false && pinkyUp == false && runTimer == false && destroyed == false){
            if(fire.stateDown){
                if(Fammo >= 10){
                    print("BANG!");
                
                if(character.Biology == true){
                    Rigidbody proj = Instantiate(shot1, hand.transform.position, Quaternion.Euler(hand.transform.rotation.eulerAngles) * Quaternion.Euler(0,0,0)).GetComponent<Rigidbody>();
                    proj.transform.localScale = new Vector3(scale, scale, scale);
                    proj.AddForce(hand.transform.forward * range, ForceMode.Impulse);
                    proj.AddForce(transform.up * up, ForceMode.Impulse);
                } else{
                    Rigidbody proj = Instantiate(shot3, hand.transform.position, Quaternion.Euler(hand.transform.rotation.eulerAngles) * Quaternion.Euler(0,0,0)).GetComponent<Rigidbody>();
                    proj.transform.localScale = new Vector3(scale, scale, scale);
                    proj.AddForce(hand.transform.forward * range, ForceMode.Impulse);
                    proj.AddForce(transform.up * up, ForceMode.Impulse);
                }
                Fammo -= 10;
                fammoText.text = Fammo.ToString();
                }
            }
        }
        if(indexUp== true & middleUp == false && ringUp == false && pinkyUp == true && runTimer == false && destroyed == false){
            if(fire.stateDown){
                if(Wammo >= 10){
                    print("BANG!2");
                
                if (character.Chem == true){
                    Rigidbody proj = Instantiate(shot2, hand.transform.position, Quaternion.Euler(hand.transform.rotation.eulerAngles) * Quaternion.Euler(0,0,0)).GetComponent<Rigidbody>();
                    proj.transform.localScale = new Vector3(scale/50f, scale/50f, scale/50f);
                    proj.AddForce(hand.transform.forward * range, ForceMode.Impulse);
                    proj.AddForce(transform.up * up, ForceMode.Impulse);
                }else{
                    Rigidbody proj = Instantiate(shot4, hand.transform.position, Quaternion.Euler(hand.transform.rotation.eulerAngles) * Quaternion.Euler(0,0,0)).GetComponent<Rigidbody>();
                    proj.transform.localScale = new Vector3(scale/50f, scale/50f, scale/50f);
                    proj.AddForce(hand.transform.forward * range, ForceMode.Impulse);
                    proj.AddForce(transform.up * up, ForceMode.Impulse);
                }
                Wammo -= 10;
                wammoText.text = Wammo.ToString();
                }
            }
        }
    }

    /*private void fireShot(){
        if (fire1 == true){
            print("BANG!");
            fire1 = false;
            Rigidbody proj = Instantiate(shot1, hand.transform.position, Quaternion.Euler(hand.transform.rotation.eulerAngles) * Quaternion.Euler(45,0,0)).GetComponent<Rigidbody>();
            proj.AddForce(transform.forward * 20f, ForceMode.Impulse);
            proj.AddForce(transform.up * 3f, ForceMode.Impulse); 
        }else if (fire2 == true){
            print("BANG!2");
            fire2 = false;
            Rigidbody proj = Instantiate(shot2, hand.transform.position, Quaternion.Euler(hand.transform.rotation.eulerAngles) * Quaternion.Euler(0,-90,0)).GetComponent<Rigidbody>();
            proj.AddForce(transform.forward * 20f, ForceMode.Impulse);
            proj.AddForce(transform.up * 3f, ForceMode.Impulse); 
        }
    }*/
    public void reloadFood(){
        //Player code for reloading food
        if (Fammo < 50)
        {
            Fammo += 50;
            if(Fammo > 50){
                Fammo = 50;
            }
        }
        fammoText.text = Fammo.ToString();
    }
    public void reloadWater(){
        //Player code for reloading water
        if (Wammo < 50)
        {
            Wammo += 50;
            if(Wammo > 50){
                Wammo = 50;
            }
        }
        wammoText.text = Wammo.ToString();
    }
    public void TakeDamage(){
        health -= 20;
        if (health <= 0){
            SpectatePlayer();
        }
        Debug.Log(health);
    }
    public void SpectatePlayer(){
        if(destroyed != true){
            gameController = GameObject.Find("GameController");
            gameMode = gameController.gameObject.GetComponentInParent<GameMode>();
            gameMode.removeBlue();
            gameObject.layer = 2;
            this.gameObject.tag="Spectator";
            foreach (Transform child in transform)
            {
                child.tag = "Spectator";
            }
            destroyed = true;
        }
    }
    public void UnSpectatePlayer(){
        if(destroyed == true){
            gameObject.layer = 13;
            this.gameObject.tag="AllyBot";
            foreach (Transform child in transform)
            {
                child.tag = "AllyBot";
            }
            destroyed = false;
        }
    }
    public void RespawnPlayer(){
        Fammo = 0;
        Wammo = 0;
        health = 100;
        wammoText.text = Wammo.ToString();
        fammoText.text = Fammo.ToString();
        if(character.Gym == true){
            range = 25f;
            up = -15f;
        }
        if(character.DT == true){
            scale = 5.5f;
        }
    }
}