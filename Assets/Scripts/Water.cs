using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this code is controlling the water fountain and controls water collection 
public class Water : MonoBehaviour
{
    //defining the variables
    public float Timer = 0;
    public float TimeReset = 3;
    //public Attack attack;
    public Animation anim;
    public bool col = false;
    public bool bcol = false;
    BotController bot_script;
    PlayerController player;
    
    // Start is called before the first frame update
    void Start(){
        //gets the component animation
        anim = GetComponent<Animation>();
    }
    public void Update(){
        //colliders if the bot or player runs into it run the animation
        Timer -= Time.deltaTime;
        if (Timer <= 0){
            if(col == true){
                GetComponent<Collider>().enabled = false;
                Timer = TimeReset;
                col = false;
                //attack.reloadWater();
            } else if(bcol == true){
                GetComponent<Collider>().enabled = false;
                Timer = TimeReset;
                bcol = false;
                
            }
        }  else if(Timer >= 0){
            if (col != true || bcol != true){}
            GetComponent<Collider>().enabled = true;
        }
    }
    void OnTriggerEnter(Collider other){
        // gives water to the bot or player depending on who runs into the object
        if(other.gameObject.tag == "Hand") 
        {
            anim.Play("handlePush");
            col = true;
            player = other.gameObject.GetComponentInParent<PlayerController>();
            player.reloadWater();
        } else if(other.gameObject.tag == "EnemyBot") 
        {
            anim.Play("handlePush");
            bcol = true;
            bot_script = other.gameObject.GetComponentInParent<BotController>();
            bot_script.reloadWater();
        } else if(other.gameObject.tag == "AllyBot") 
        {
            anim.Play("handlePush");
            bcol = true;
            bot_script = other.gameObject.GetComponentInParent<BotController>();
            bot_script.reloadWater();
        }
    }
}