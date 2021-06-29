using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public float Timer = 15;
    public float TimerMenu = 15;
    public float TimeReset = 15;
    bool end = false;
    public bool restarting = false;
    public bool restarted = false;
    bool menu = false;
    public int blueTeamScore;
    public int redTeamScore;
    public Spawner[] spawnPoints;
    public int WinCondition;
    public int countDown;
    public int CurrentRound;
    public int blueAlive = 5;
    public int redAlive = 5;
    public Text redScore;
    public Text blueScore;
    public GameObject redWin;
    public GameObject blueWin;
    public GameObject player;
    public GameObject playerSpawn;
    PlayerController playerController;
    public AudioSource beginningSource;

    
    // Start is called before the first frame update
    void Start()
    {
        //spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
        for(int i = 0; i< spawnPoints.Length; i++){
            spawnPoints[i].Spawn();
        }
        CurrentRound = 1;
        moveplayer();
        beginningSource.Play();
    }

    public void removeRed(){
        redAlive -= 1;
        Debug.Log(redAlive);
        if(redAlive == 0){
            blueTeamScore++;
            blueScore.text = blueTeamScore.ToString();
            restarting = true;
        }
    }

    public void removeBlue(){
        blueAlive -= 1;
        Debug.Log(blueAlive);
        if(blueAlive == 0){
            redTeamScore++;
            redScore.text = redTeamScore.ToString();
            restarting = true;
        }
    }

    void restartRound(){
        
        if(blueAlive == 0){
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyBot");
            foreach(GameObject enemy in enemies){
                GameObject.Destroy(enemy);
            }
        }else if(redAlive == 0){
            player = GameObject.Find("Player");
            playerController = player.gameObject.GetComponentInParent<PlayerController>();
            playerController.SpectatePlayer();
            GameObject[] allies = GameObject.FindGameObjectsWithTag("AllyBot");
            player.tag = "AllyBot";
            foreach(GameObject ally in allies){
                GameObject.Destroy(ally);
            }
            playerController.UnSpectatePlayer();
        }
        for(int i = 0; i< spawnPoints.Length; i++){
            spawnPoints[i].Spawn();
        }
        blueAlive = 5;
        redAlive = 5;
        CurrentRound++;
        moveplayer();
        restarted = false;
    }
    void moveplayer(){
        player = GameObject.Find("Player");
        playerController = player.gameObject.GetComponentInParent<PlayerController>();
        player.transform.position = playerSpawn.transform.position;
        playerController.RespawnPlayer();
    }

    void Update(){
        if(restarting == true){
            Timer -= Time.deltaTime;
        }
        if(restarted != true){
            if(Timer <= 0){
                restarted = true;
                restarting = false;
                Timer = 15;
                restartRound();
            }
        }
        if(end == false){
            if(redTeamScore == 2 && blueTeamScore <= 1){
                redWin.SetActive(true);
                end = true;
                Timer = 15;
                menu = true;
            } else if(blueTeamScore == 2 && redTeamScore <= 1){
                blueWin.SetActive(true);
                end = true;
                Timer = 15;
                menu = true;
            } else if(redTeamScore == 5 && blueTeamScore == 5){
                end = true;
                TimerMenu = 15;
                menu = true;
            }
        }
        if(menu == true){
            TimerMenu -= Time.deltaTime;
        }
        if(TimerMenu <= 0){
            SceneManager.LoadScene("menu");
        }
    }
}
