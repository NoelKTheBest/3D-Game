using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        dialogText.text = "A wild " + enemyUnit.unitName + " wants to start some shit."; //" approaches.";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

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
        }
        else if (state == BattleState.LOST)
        {
            dialogText.text = "You suck, fuck outta here bruh";
        }

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

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogText.text = "It wasn't very effective, dipshit...";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            hasBeenCalled = false;
            StartCoroutine(EnemyTurn());
        }
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

        yield return new WaitForSeconds(0.5f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        
        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            turnNumber++;
            PlayerTurn();
        }
    }

    #endregion
}
