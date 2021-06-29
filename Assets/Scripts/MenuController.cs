using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

//this code controlles the main menu scene and will let the player set up their player classes. Since this game is set in a school the player chooses their school classes to create a class currently the classes do not change anything.
public class MenuController : MonoBehaviour
{
    //defing the variables needed for the code
    public GameObject chemToggle;
    public GameObject bioToggle;
    public GameObject gymToggle;
    public GameObject dtToggle;
    public Character character;
    public GameControl gc;
    public GameObject[] menuPanels;
    PlayerController player;
    GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("Player");
        player = obj.gameObject.GetComponentInParent<PlayerController>();
        
        player.canvas.SetActive(false);
    }
    //the change functions are for toggling the values for defining the players class you can choose 2 currently if you choose a third one will turn off
    public void ChangeBioValue()
    {
        if(bioToggle.GetComponent<Toggle>().isOn != false){
            chemToggle.GetComponent<Toggle>().isOn = false;
            character.Biology = true;
            gc.Elective1 = 1;
            if(character.Chem == true){
                character.Chem = false;
            }
        }else {
            //bioToggle.GetComponent<Toggle>().isOn = false;
            character.Biology = false;
        }
    }
    public void ChangeGymValue()
    {
        if(gymToggle.GetComponent<Toggle>().isOn != false){
            dtToggle.GetComponent<Toggle>().isOn = false;
            character.Gym = true;
            gc.Elective2 = 1;
            if(character.DT == true){
                character.DT = false;
            }
        }else {
            //gymToggle.GetComponent<Toggle>().isOn = false;
            character.Gym = false;
        }
    }
    public void ChangeDTValue()
    {
        if(dtToggle.GetComponent<Toggle>().isOn != false){
            gymToggle.GetComponent<Toggle>().isOn = false;
            character.DT = true;
            gc.Elective2 = 2;
            if(character.Gym == true){
                character.Gym = false;
            }
        }else {
            //dtToggle.GetComponent<Toggle>().isOn = false;
            character.DT = false;
        }
    }
    public void ChangeChemValue()
    {
        if(chemToggle.GetComponent<Toggle>().isOn != false){
            bioToggle.GetComponent<Toggle>().isOn = false;
            character.Chem = true;
            gc.Elective1 = 2;
            if(character.Biology == true){
                character.Biology = false;
            }
        }else {
            //chemToggle.GetComponent<Toggle>().isOn = false;
            character.Chem = false;
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if(artToggle.GetComponent<Toggle>().isOn == true){
            character.Art = true;
            Art = 1;
        } else{
            character.Art = false;
            Art = 0;
        }
        if(bioToggle.GetComponent<Toggle>().isOn == true){
            character.Biology = true;
            Bio = 1;
        } else{
            character.Biology = false;
            Bio = 0;
        }
        if(bandToggle.GetComponent<Toggle>().isOn == true){
            character.Band = true;
            Band = 1;
        } else{
            character.Band = false;
            Band = 0;
        }
        if(gymToggle.GetComponent<Toggle>().isOn == true){
            character.Gym = true;
            Gym = 1;
        } else{
            character.Gym = false;
            Gym = 0;
        }
    }*/

    public void createChar(){
        openPanel("Panel2");
    }
    public void saveChar(){
        openPanel("Panel1");
        gc.SaveData();
    }
    public void openPanel(string panelName){
        foreach(GameObject go in menuPanels){
            if(go.name == panelName){
                go.SetActive(true);
            }else{
                go.SetActive(false);
            }
        }
    }    
    public void play(){
        //this code playes the actuall game
        player.canvas.SetActive(true);
        SceneManager.LoadScene("Level 1");
    }
}