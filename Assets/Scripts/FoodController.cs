using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this code is to controller the food stations and the spawning of food(AMMO) pellets
public class FoodController : MonoBehaviour
{
     // defining the variables for the food controller including the pellates
    public GameObject food;
    public GameObject food1;
    public GameObject food2;
    public GameObject food3;
    public float Timer = 5;
    public float Timer1 = 5;
    public float Timer2 = 5;
    public float Timer3 = 5;
    public float TimeReset = 5;
    // Start is called before the first frame update
    public void Update()
    {
        //each pellate has a timer and they turn the food on if it is less than zero and it is off after being collided with
        if (Timer >= 0){
            Timer -= Time.deltaTime;
        }

        if (Timer <= 0 && food.activeSelf == false)
        {
            food.SetActive(true);
            Timer = TimeReset;
        }

        if (Timer1 >= 0){
            Timer1 -= Time.deltaTime;
        }

        if (Timer1 <= 0 && food1.activeSelf == false)
        {
            food1.SetActive(true);
            Timer1 = TimeReset;
        }

        if (Timer2 >= 0){
            Timer2 -= Time.deltaTime;
        }

        if (Timer2 <= 0 && food2.activeSelf == false)
        {
            food2.SetActive(true);
            Timer2 = TimeReset;
        }

        if (Timer3 >= 0){
            Timer3 -= Time.deltaTime;
        }
        
        if (Timer3 <= 0 && food3.activeSelf == false)
        {
            food3.SetActive(true);
            Timer3 = TimeReset;
        }
    }
}
