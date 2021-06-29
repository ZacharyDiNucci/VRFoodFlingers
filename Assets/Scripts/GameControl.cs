using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour{
    public int Elective1;
    public int Elective2;
    public Character character;

    public void Start(){
        LoadData();
    }

    public void SaveData(){
        PlayerPrefs.SetInt("Elec1", Elective1);
        PlayerPrefs.SetInt("Elec2", Elective2);
        PlayerPrefs.Save();
    }

    public void LoadData(){
        Elective1 = PlayerPrefs.GetInt("Elec1");
        if(Elective1 == 1){
            character.Biology = true;
            character.Chem = false;
        } else if (Elective1 == 2){
            character.Chem = true;
            character.Biology = false;
        }
        Elective2 = PlayerPrefs.GetInt("Elec2");
        if(Elective2 == 1){
            character.Gym = true;
            character.DT = false;
        } else if (Elective2 == 2){
            character.DT = true;
            character.Gym = false;
        }
    }
}