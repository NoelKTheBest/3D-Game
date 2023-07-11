using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState {NOBATTLE, START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    /*
     * Things to note:
     *  The battle system allows for the player to continue attack even when it's not there turn
     */
    Unit playerUnit;
    Unit enemyUnit;

    #region Image References
    public GameObject playerIdle;
    public GameObject playerAttack;
    public GameObject playerHurt;
    public GameObject playerDie;

    public GameObject enemy1Idle;
    public GameObject enemy1Attack;
    public GameObject enemy1Hurt;
    public GameObject enemy1Die;

    public GameObject enemy2Idle;
    public GameObject enemy2Attack;
    public GameObject enemy2Hurt;
    public GameObject enemy2Die;
    #endregion

    public TMP_Text dialogText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public GameObject buttons;

    public int turnNumber = 1;

    bool hasBeenCalled = false;

    void Awake()
    {
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        enemyUnit = GameObject.FindGameObjectWithTag("Unit").GetComponent<Unit>();
        //Debug.Log(enemyUnit.gameObject.tag);
    }

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.NOBATTLE;

        playerIdle.SetActive(false);
        playerAttack.SetActive(false);
        playerHurt.SetActive(false);
        playerDie.SetActive(false);

        enemy1Idle.SetActive(false);
        enemy1Attack.SetActive(false);
        enemy1Hurt.SetActive(false);
        enemy1Die.SetActive(false);

        enemy2Idle.SetActive(false);
        enemy2Attack.SetActive(false);
        enemy2Hurt.SetActive(false);
        enemy2Die.SetActive(false);
    }

    void Update()
    {
        if (state == BattleState.PLAYERTURN && !hasBeenCalled)
        {
            buttons.SetActive(true);
        }
        else if (state != BattleState.PLAYERTURN)
        {
            buttons.SetActive(false);
        }
        else
        {
            buttons.SetActive(false);
        }

        if (state == BattleState.WON || state == BattleState.LOST)
        {
            turnNumber = 0;
        }
        
        //Debug.Log(turnNumber);
    }

    #region Setup & Initialization

    public void Init()
    {
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        state = BattleState.START; //might try and set this another way later on

        dialogText.text = "A wild " + enemyUnit.unitName + " wants to party."; //" approaches.";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        
        playerIdle.SetActive(true);

        if (enemyUnit.unitName == "Kruumpa")
        {
            enemy1Idle.SetActive(true);
        }
        else if (enemyUnit.unitName == "Grumer" || enemyUnit.unitName == "Grumlord")
        {
            enemy2Idle.SetActive(true);
        }

        yield return new WaitForSeconds(2f);
        
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    #endregion

    #region Ending the Battle
    
    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogText.text = "You cheated...";
            if (playerUnit.currentHP + 10 < playerUnit.maxHP) playerUnit.currentHP += 10;
            if (playerUnit.currentHP + 10 > playerUnit.maxHP) playerUnit.currentHP = playerUnit.maxHP;
        }
        else if (state == BattleState.LOST)
        {
            dialogText.text = "BANNED";
        }

        if (enemyUnit.unitName == "Kruumpa")
        {
            enemy1Die.SetActive(false);
        }
        else if (enemyUnit.unitName == "Grumer" || enemyUnit.unitName == "Grumlord")
        {
            enemy2Die.SetActive(false);
        }

        hasBeenCalled = false;

        yield return new WaitForSeconds(1f);

        OverworldStatus.battleInProgress = false;
    }

    #endregion

    #region Player Actions

    // What is the point of this function?
    void PlayerTurn()
    {
        dialogText.text = "Choose an action:";
    }

    public void OnAttackButton()
    {
        //if (state != BattleState.PLAYERTURN && !hasBeenCalled) return;
        hasBeenCalled = true;

        playerIdle.SetActive(false);
        playerAttack.SetActive(true);
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        if (enemyUnit.unitName == "Kruumpa")
        {
            enemy1Idle.SetActive(false);
            enemy1Hurt.SetActive(true);
        }
        else if (enemyUnit.unitName == "Grumer" || enemyUnit.unitName == "Grumlord")
        {
            enemy2Idle.SetActive(false);
            enemy2Hurt.SetActive(true);
        }

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogText.text = "You attack!";

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            if (enemyUnit.unitName == "Kruumpa")
            {
                enemy1Hurt.SetActive(false);
                enemy1Die.SetActive(true);
            }
            else if (enemyUnit.unitName == "Grumer" || enemyUnit.unitName == "Grumlord")
            {
                enemy2Hurt.SetActive(false);
                enemy2Die.SetActive(true);
            }

            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            if (enemyUnit.unitName == "Kruumpa")
            {
                enemy1Hurt.SetActive(false);
                enemy1Idle.SetActive(true);
            }
            else if (enemyUnit.unitName == "Grumer" || enemyUnit.unitName == "Grumlord")
            {
                enemy2Hurt.SetActive(false);
                enemy2Idle.SetActive(true);
            }
            
            yield return new WaitForSeconds(1f);

            state = BattleState.ENEMYTURN;
            hasBeenCalled = false;

            if (enemyUnit.unitName == "Kruumpa")
            {
                enemy1Idle.SetActive(false);
                enemy1Attack.SetActive(true);
            }
            else if (enemyUnit.unitName == "Grumer" || enemyUnit.unitName == "Grumlord")
            {
                enemy2Idle.SetActive(false);
                enemy2Attack.SetActive(true);
            }
            StartCoroutine(EnemyTurn());
        }

        playerAttack.SetActive(false);
        //playerIdle.SetActive(true);
    }

    public void OnRunButton()
    {
        hasBeenCalled = true;

        StartCoroutine(RunAway());
    }

    IEnumerator RunAway()
    {
        // Print statement to dialog box
        
        // Do a randomized check

        // Deny request to escape if check fails or allow escape if check passes
        
        yield return new WaitForSeconds(1f);

        // Print result
    }
    #endregion

    #region Enemy Turn

    IEnumerator EnemyTurn()
    {
        dialogText.text = enemyUnit.unitName + " attacks!";

        playerIdle.SetActive(false);
        playerHurt.SetActive(true);

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        
        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            playerHurt.SetActive(false);
            playerDie.SetActive(true);

            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            playerHurt.SetActive(false);
            playerIdle.SetActive(true);

            state = BattleState.PLAYERTURN;
            turnNumber++;
            PlayerTurn();
        }

        if (enemyUnit.unitName == "Kruumpa")
        {
            enemy1Attack.SetActive(false);
            enemy1Idle.SetActive(true);
        }
        else if (enemyUnit.unitName == "Grumer" || enemyUnit.unitName == "Grumlord")
        {
            enemy2Attack.SetActive(false);
            enemy2Idle.SetActive(true);
        }
    }

    #endregion
}
