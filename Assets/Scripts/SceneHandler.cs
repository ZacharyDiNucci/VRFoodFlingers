/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public GameObject chemToggle;
    public GameObject bioToggle;
    public GameObject gymToggle;
    public GameObject dtToggle;
    public Character character;
    public GameControl gc;
    public GameObject[] menuPanels;
    PlayerController player;
    GameObject obj;

    void Awake()
    {
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Join Classes")
        {
            createChar();
        } else if (e.target.name == "New Game")
        {
            play();
        } else if (e.target.name == "Bio")
        {
            ChangeBioValue();
            
        } else if (e.target.name == "Chem")
        {
            ChangeChemValue();
            Debug.Log("Chem was clicked");
        } else if (e.target.name == "DT")
        {
            ChangeDTValue();
            Debug.Log("DT was clicked");
        } else if (e.target.name == "PE")
        {
            ChangeGymValue();
            Debug.Log("PE was clicked");
        } else if (e.target.name == "Save Classes")
        {
            saveChar();
            Debug.Log("Save was clicked");
        } 
    }
void Start()
    {
        obj = GameObject.Find("Player");
        player = obj.gameObject.GetComponentInParent<PlayerController>();
        laserPointer.enabled = true;
        player.canvas.SetActive(false);
    }
    //the change functions are for toggling the values for defining the players class you can choose 2 currently if you choose a third one will turn off
    public void ChangeBioValue()
    {
        if(bioToggle.GetComponent<Toggle>().isOn == false){
            Debug.Log("Bio was clicked");
            chemToggle.GetComponent<Toggle>().isOn = false;
            bioToggle.GetComponent<Toggle>().isOn = true;
            character.Biology = true;
            gc.Elective1 = 1;
            if(character.Chem == true){
                character.Chem = false;
            }
        }else {
            bioToggle.GetComponent<Toggle>().isOn = false;
            character.Biology = false;
            Debug.Log("Bio was unclicked");
        }
    }
    public void ChangeGymValue()
    {
        if(gymToggle.GetComponent<Toggle>().isOn == false){
            dtToggle.GetComponent<Toggle>().isOn = false;
            gymToggle.GetComponent<Toggle>().isOn = true;
            character.Gym = true;
            gc.Elective2 = 1;
            if(character.DT == true){
                character.DT = false;
            }
        }else {
            gymToggle.GetComponent<Toggle>().isOn = false;
            character.Gym = false;
        }
    }
    public void ChangeDTValue()
    {
        if(dtToggle.GetComponent<Toggle>().isOn == false){
            gymToggle.GetComponent<Toggle>().isOn = false;
            dtToggle.GetComponent<Toggle>().isOn = true;
            character.DT = true;
            gc.Elective2 = 2;
            if(character.Gym == true){
                character.Gym = false;
            }
        }else {
            dtToggle.GetComponent<Toggle>().isOn = false;
            character.DT = false;
        }
    }
    public void ChangeChemValue()
    {
        if(chemToggle.GetComponent<Toggle>().isOn == false){
            bioToggle.GetComponent<Toggle>().isOn = false;
            chemToggle.GetComponent<Toggle>().isOn = true;
            character.Chem = true;
            gc.Elective1 = 2;
            if(character.Biology == true){
                character.Biology = false;
            }
        }else {
            chemToggle.GetComponent<Toggle>().isOn = false;
            character.Chem = false;
        }
    }
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
        laserPointer.enabled =false;
        player.canvas.SetActive(true);
        SceneManager.LoadScene("Level 1");
    }
}
