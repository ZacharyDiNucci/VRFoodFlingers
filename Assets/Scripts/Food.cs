using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this code controlles the food and if they are being touched by the player or the bots
public class Food : MonoBehaviour
{
    // defining variables for the food pellates
    public GameObject food;
    public float Timer = 5;
    public float TimeReset = 5;
    BotController bot_script;
    PlayerController player;
    
    void OnTriggerEnter(Collider other){
        // looks to see what is currently colliding whith the food pellate if hand trigger player food reload funciton if bot trigger that bot's food reload function
        if(other.gameObject.tag == "Hand")
        {
            player = other.gameObject.GetComponentInParent<PlayerController>();
            player.reloadFood();
            Timer = TimeReset;
            food.SetActive(false);
        } else if(other.gameObject.tag == "EnemyBot")
        {
            bot_script = other.gameObject.GetComponentInParent<BotController>();
            bot_script.reloadFood();
            Timer = TimeReset;
            food.SetActive(false);
        } else if(other.gameObject.tag == "AllyBot") 
        {
            bot_script = other.gameObject.GetComponentInParent<BotController>();
            bot_script.reloadFood();
            Timer = TimeReset;
            food.SetActive(false);
        } 
    }
}