using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyFood(2));
    }
    public IEnumerator DestroyFood(float f){
        yield return new WaitForSeconds(f);
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other){
        if(dead != true){
            if(gameObject.name == "burger 1(Clone)" && other.gameObject.tag == "EnemyBot"){
                other.gameObject.GetComponentInParent<BotController>().TakeDamage();
                Destroy(gameObject);
                dead = true;
            }
            if(gameObject.name == "burger 2(Clone)" && other.gameObject.tag == "EnemyBot"){
                other.gameObject.GetComponentInParent<BotController>().TakeDamage2();
                Destroy(gameObject);
                dead = true;
            }
            else if (gameObject.name == "hotdog 1(Clone)" && other.gameObject.tag == "AllyBot"){
                if(other.gameObject.GetComponentInParent<BotController>() != null){
                    other.gameObject.GetComponentInParent<BotController>().TakeDamage();
                    Destroy(gameObject);
                    dead = true;
                } else {
                    other.gameObject.GetComponentInParent<PlayerController>().TakeDamage();
                    Destroy(gameObject);
                    dead = true;
                }

                
            }
            else if (gameObject.name == "shot1(Clone)" && other.gameObject.tag == "AllyBot"){
                if(other.gameObject.GetComponentInParent<BotController>() != null){
                    other.gameObject.GetComponentInParent<BotController>().HealDamage();
                    Destroy(gameObject);
                    dead = true;
                }
            }
            else if (gameObject.name == "shot2(Clone)" && other.gameObject.tag == "AllyBot"){
                if(other.gameObject.GetComponentInParent<BotController>() != null){
                    other.gameObject.GetComponentInParent<BotController>().HealDamage2();
                    Destroy(gameObject);
                    dead = true;
                }
            }
            else if (gameObject.name == "hotdog 1(Clone)" && other.gameObject.tag == "EnemyBot"){
            }
            else if(gameObject.name == "burger 1(Clone)" && other.gameObject.tag == "AllyBot"){
            }
            else if(gameObject.name == "burger 2(Clone)" && other.gameObject.tag == "AllyBot"){
            }
            else if(gameObject.name == "burger 1(Clone)" && other.gameObject.tag == "Player"){
            }
            else if(gameObject.name == "burger 2(Clone)" && other.gameObject.tag == "Player"){
            }
            else if(gameObject.name == "burger 1(Clone)" && other.gameObject.tag == "Hand"){
            }
            else if(gameObject.name == "burger 2(Clone)" && other.gameObject.tag == "Hand"){
            }
            else if(gameObject.name == "shot2(Clone)" && other.gameObject.tag == "Hand"){
            }
            else if(gameObject.name == "shot1(Clone)" && other.gameObject.tag == "Hand"){
            }
            else if(other.gameObject.name == "burger 1(Clone)" || other.gameObject.name == "hotdog 1(Clone)"){
            }
            else{
                StartCoroutine(DestroyFood(0));
            }
        }

    }
}
