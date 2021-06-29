using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//this code controlles the bots of my game and tells them how to act.
public enum botStates{
    //defines bot states
    Search,
    Chase,
    Attack,
    Food,
    Water

}
public class BotController : MonoBehaviour
{

    //defines bot variables
    private Animator anim;
    private NavMeshAgent navAgent;
    private botStates bot_State;

    public LayerMask whatIsGround, whatIsOp;

    // searching variables
    public Vector3 searchPoint;
    bool isSearching;
    public GameObject attackPoint;
    public float searchPointRange;

    // attacking variables
    public float attackTime;
    bool alreadyAttacked = false;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool opInSightRange, opInAttackRange;
    
    public float healDist = 10f;
    public float attackDist = 15f;
    
    GameObject closestFood = null;
    GameObject closestWater = null;
    GameObject closestAlly = null;
    public GameObject closestEnemy = null;
    GameObject rememberedEnemy = null;
    public int bFammo = 0;
    public int bWammo = 0;
    public bool Ally = false;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public bool run = false;
    public bool running = false;
    public bool destroyed = false;
    public bool blocked = false; 
    public bool attackable = false;
    public GameObject game;
    public GameMode gameMode;



    void Awake(){
        //when bot is spawned set the nav agent and know where the player is
        game = GameObject.Find("GameController");
        gameMode = game.GetComponent<GameMode>();
        anim = gameObject.GetComponentInChildren<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        if(Ally == true){
            GameObject[] target1 = GameObject.FindGameObjectsWithTag("EnemyBot");
            Transform[] targetOp = new Transform[target1.Length];
            //Copy the GameObject transform to the new3 transform array
            for (int i = 0; i < target1.Length; i++)
            {
                targetOp[i] = target1[i].transform;
            }
            GameObject[] target2 = GameObject.FindGameObjectsWithTag("AllyBot");
            Transform[] targetAlly = new Transform[target2.Length];
            //Copy the GameObject transform to the new3 transform array
            for (int j = 0; j < target2.Length; j++)
            {
                targetAlly[j] = target2[j].transform;
            }
        }else{
            GameObject[] target2 = GameObject.FindGameObjectsWithTag("EnemyBot");
            Transform[] targetAlly = new Transform[target2.Length];
            //Copy the GameObject transform to the new3 transform array
            for (int i = 0; i < target2.Length; i++)
            {
                targetAlly[i] = target2[i].transform;
            }
            GameObject[] target1 = GameObject.FindGameObjectsWithTag("AllyBot");
            Transform[] targetOp = new Transform[target1.Length];
            //Copy the GameObject transform to the new3 transform array
            for (int j = 0; j < target1.Length; j++)
            {
                targetOp[j] = target1[j].transform;
            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        //initial state is water which searches for water
        bot_State = botStates.Water;         
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        //depending on the state it will call a function
        if (bot_State == botStates.Food){
            FMode();
            anim.SetBool("IsAttacking", false);
        } else if (bot_State == botStates.Water){
            WMode();
            anim.SetBool("IsAttacking", false);
        } else if (bot_State == botStates.Attack){
            AMode();
            run = false;
            isRunning();
            anim.SetBool("IsAttacking", true);
        } else if (bot_State == botStates.Search){
            SMode();
            anim.SetBool("IsAttacking", false);
        } else if (bot_State == botStates.Chase){
            CMode();
            anim.SetBool("IsAttacking", false);
        }

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int layerMask = 1 << 10;
        int layerMask2 = 1 << 11;
        int layerMask3 = 1 << 13;
        RaycastHit hit;
        /*if (Physics.Raycast(attackPoint.transform.position, fwd,out hit, 10, layerMask)){
            Debug.DrawRay(attackPoint.transform.position, fwd, out hit, 10, layerMask, Color.white);
            blocked = true;
        }*/
        if (Physics.Raycast(attackPoint.transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
        {
            Debug.DrawRay(attackPoint.transform.position, attackPoint.transform.TransformDirection(Vector3.forward) * 10, Color.yellow);
            Debug.Log("Did Hit");
            blocked = true;
        }else
        {
            Debug.DrawRay(attackPoint.transform.position, attackPoint.transform.TransformDirection(Vector3.forward) * 10, Color.white);
            Debug.Log("Did not Hit");
            StartCoroutine(blockedFalse(.7f));
        }
        
        if(Ally == true && Physics.Raycast(attackPoint.transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask2) && blocked == false){
            attackable = true;
        } else if(Ally == false && Physics.Raycast(attackPoint.transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask3) && blocked == false){
            attackable = true;
        }
        
        

        if(Ally == true){
            opInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsOp);
            opInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsOp);
            if(bWammo <= 10){
                bot_State = botStates.Water;
            } else if(bFammo <= 10){
                bot_State = botStates.Food;
            }
            if(!opInSightRange && !opInAttackRange && bFammo >= 10){
                bot_State = botStates.Search;
            }
            if(opInSightRange && !opInAttackRange && bFammo >= 10){
                bot_State = botStates.Chase;
            } else if(opInSightRange && opInAttackRange && bFammo >= 10 && blocked == true){
                bot_State = botStates.Chase;
            }
            if(opInSightRange && opInAttackRange && bFammo >= 10 && attackable == true){
                bot_State = botStates.Attack;
            }
        }else{
            opInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsOp);
            opInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsOp);
            if(bWammo <= 10){
                bot_State = botStates.Water;
            } else if(bFammo <= 10){
                bot_State = botStates.Food;
            }
            if(!opInSightRange && !opInAttackRange && bFammo >= 10){
                bot_State = botStates.Search;
            }
            if(opInSightRange && !opInAttackRange && bFammo >= 10){
                bot_State = botStates.Chase;
            } else if(opInSightRange && opInAttackRange && bFammo >= 10 && blocked == true){
                bot_State = botStates.Chase;
            }
            if(opInSightRange && opInAttackRange && bFammo >= 10  && attackable == true){
                bot_State = botStates.Attack;
            }
        }
    }



    public IEnumerator blockedFalse(float f){
        yield return new WaitForSeconds(f);
        blocked = false;
    }





    void FMode(){
        // if the bot is in the food state it will search for the nearest food pellet
        navAgent.isStopped = false;
       MoveToClosestFood(); 
    }

    void WMode(){
        // if the bot is in the water state they will search for the nearest water fountain
        navAgent.isStopped = false;
        MoveToClosestHandle();
    }
    void CMode(){
        // if the bot is in the water state they will search for the nearest water fountain
        navAgent.isStopped = false;
        ChasePlayer();
    }

    void AMode(){
        //scanning to attack not fully implemented yet
        navAgent.isStopped = true;
        AttackPlayer();
    }





    void isRunning(){
        if(run == true && running == false){
            anim.SetInteger("AnimPar", 1);
            running = true;
        }
        if(run == false && running == true){
            anim.SetInteger("AnimPar", 0);
            
            running = false;
        }

    }




    void SMode(){
        //the bot will patrol the map looking for stuff, currently it is random and they will stop and search after movement
        if (!isSearching) SearchWalkPoint();

        if (isSearching)
            navAgent.SetDestination(searchPoint);
            run = true;
            isRunning();

        Vector3 distanceToWalkPoint = transform.position - searchPoint;

        if(distanceToWalkPoint.magnitude < 1f)
            isSearching = false;
    }
    void SearchWalkPoint(){
        //calculate random search point
        float randomZ = Random.Range(-searchPointRange, searchPointRange);
        float randomX = Random.Range(-searchPointRange, searchPointRange);

        searchPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //searchPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(searchPoint, -transform.up, 2f, whatIsGround))
            isSearching = true;
    }

    void MoveToClosestFood(){
        //sets destination to the nearest food after finding it
        FindClosestFood();
        navAgent.SetDestination(closestFood.transform.position);
        run = true;
        isRunning();
    }
    void MoveToClosestHandle(){
        //sets destination to the nearest water after finding it
        FindClosestWater();
        navAgent.SetDestination(closestWater.transform.position);
        run = true;
        isRunning();
    }



    public GameObject FindClosestFood()
    {
        if(Ally == true){
            //code for finding the closest food item
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("AllyFood");
            
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestFood = go;
                    distance = curDistance;
                }
            }
        }
        else if (Ally == false){
            //code for finding the closest food item
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("EnemyFood");
            
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestFood = go;
                    distance = curDistance;
                }
            }
        }
        
        return closestFood;
    }



public GameObject FindClosestWater()
    {
        //code for finding the closest water item
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Handle");
        
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestWater = go;
                distance = curDistance;
            }
        }
        return closestWater;
    }
    public void reloadFood(){
        //bots code for reloading food
        if (bFammo < 50)
        {
            bFammo += 50;
            if(bFammo > 50){
                bFammo = 50;
            }
        }
    }
    public void reloadWater(){
        //bots code for reloading water
        if (bWammo < 50)
        {
            bWammo += 50;
            if(bWammo > 50){
                bWammo = 50;
            }
        }
    }

    /*void OnCollisionEnter(Collision col){
        //if the bot is hit depending on tag it will either take damage and die or they will be healed
        if(Ally == true){
            if(col.gameObject.name == "burger 1(Clone)"){
            } else if(col.gameObject.name == "hotdog 1(Clone)"){
                currentHealth += 25;
            }
        }else if(Ally == false){
            if(col.gameObject.name == "burger 1(Clone)"){
                currentHealth -= 25;
            } else if(col.gameObject.name == "hotdog 1(Clone)"){
            }
        }
    }*/



    private void ChasePlayer(){
        FindClosestEnemy();
        navAgent.SetDestination(closestEnemy.transform.position);
        run = true;
        isRunning();
    }



    private void AttackPlayer(){
        FindClosestEnemy();
        if(rememberedEnemy == null){
            rememberedEnemy = closestEnemy;
            closestEnemy = null;
            Vector3 position = transform.position;
            Vector3 diff = rememberedEnemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance <= attackDist){
                navAgent.SetDestination(transform.position);
                transform.LookAt(rememberedEnemy.transform.position);
                if (alreadyAttacked == false){
                    alreadyAttacked = true;
                    anim.SetBool("IsAttacking", true);
                    //bFammo -= 10;
                    //Debug.Log(bFammo);
                    StartCoroutine(ResetAttack(2));
                }
            }
            if(curDistance >= sightRange){
                StartCoroutine(ResetMemory(5));
            }
        } else{
            closestEnemy = null;
            Vector3 position = transform.position;
            Vector3 diff = rememberedEnemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance <= attackDist){
                navAgent.SetDestination(transform.position);
                transform.LookAt(rememberedEnemy.transform.position);
                if (alreadyAttacked == false){
                    alreadyAttacked = true;
                    anim.SetBool("IsAttacking", true);
                    //bFammo -= 10;
                    //Debug.Log(bFammo);
                    StartCoroutine(ResetAttack(2));
                }
            }
            if(curDistance >= sightRange){
                StartCoroutine(ResetMemory(5));
            }
        }
    }
    public IEnumerator ResetMemory(float f){
        yield return new WaitForSeconds(f);
        rememberedEnemy = null;
        attackable = false;
        Debug.Log("Memory Reset");
    }

    public IEnumerator ResetAttack(float f){
        yield return new WaitForSeconds(f);
        alreadyAttacked = false;
    }
    public void Attack(){
        Rigidbody rb = Instantiate(projectile, attackPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
        rb.AddForce(transform.up * 3f, ForceMode.Impulse);
        bFammo -= 10;
        Debug.Log(bFammo);
    }
    public GameObject FindClosestEnemy(){
        GameObject[] gos;
        if(Ally == true){
            gos = GameObject.FindGameObjectsWithTag("EnemyBot");

            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestEnemy = go;
                    distance = curDistance;
                }
            }
            return closestEnemy;
        } else if(Ally == false){
            gos = GameObject.FindGameObjectsWithTag("AllyBot");

            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestEnemy = go;
                    distance = curDistance;
                }
            }
            return closestEnemy;
        }
        return null;
    }





    public GameObject FindClosestAlly(){
        GameObject[] gos;
        if(Ally == true){
            gos = GameObject.FindGameObjectsWithTag("AllyBot");

            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestAlly = go;
                    distance = curDistance;
                }
            }
            return closestAlly;
        } else if(Ally == false){
            gos = GameObject.FindGameObjectsWithTag("EmemyBot");

            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestAlly = go;
                    distance = curDistance;
                }
            }
            return closestAlly;
        }
        return null;
    }




    public void TakeDamage(){
        currentHealth -= 20;
        if (currentHealth <= 0){
            DestroyEnemy();
        }
    }
    public void TakeDamage2(){
        currentHealth -= 25;
        if (currentHealth <= 0){
            DestroyEnemy();
        }
    }
    public void HealDamage(){
        currentHealth += 10;
        if (currentHealth >= maxHealth){
            currentHealth = maxHealth;
        }
    }
    public void HealDamage2(){
        currentHealth += 15;
        if (currentHealth >= maxHealth){
            currentHealth = maxHealth;
        }
    }
    private void DestroyEnemy(){
        if(destroyed != true){
            if(Ally == true){
                gameMode.removeBlue();
                Debug.Log("Removed Blue");
                Destroy(gameObject);
                destroyed = true;
            }else if(Ally == false){
                gameMode.removeRed();
                Debug.Log("Removed Red");
                Destroy(gameObject);
                destroyed = true;
            }
        }
    }
    
}