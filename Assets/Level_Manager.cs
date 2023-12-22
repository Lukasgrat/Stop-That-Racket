using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level_Manager : MonoBehaviour
{
    public const int MAX_AP = 2;
    public const int MAX_TURN = 15;
    public const int MAX_PLAYER_HEALTH = 100;
    public const int MAX_ENEMY_HEALTH = 100;
    public int playerCount;
    public TMP_Text AP_TEXT;
    public Button endTurnButton;
    public Button resetMovesButton;
    public Slider healthBarPlayer;
    public TMP_Text playerHealthText;
    public TMP_Text currentPlayerText;
    public Slider healthBarEnemy;
    public TMP_Text enemyHealthText;
    public TMP_Text currentEnemyText;
    public string[] pieceNames;
    public Button ToggleMovesButton;




    public GameObject frontLineMoves;
    public GameObject wizardMoves;



    public TMP_Text turn;

    private int nextTurnEnemyHealth;


    [System.NonSerialized]
    public int currentAP;
    [System.NonSerialized]
    public int currentTurn;
    [System.NonSerialized]
    public int currentPlayer;

    [System.NonSerialized]
    public int currentPlayerHealth;
    [System.NonSerialized]
    public int currentEnemyHealth;
    private bool isMovesActive;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHealth = MAX_PLAYER_HEALTH;
        currentEnemyHealth = MAX_ENEMY_HEALTH;
        isMovesActive = false;
        currentAP = MAX_AP;
        currentTurn = 1;
        currentPlayer = 0;
        nextTurnEnemyHealth = 0;
        increase_AP(-1* currentAP);


        endTurnButton.onClick.AddListener(nextTurn);
        resetMovesButton.onClick.AddListener(resetMoves);
        ToggleMovesButton.onClick.AddListener(toggleMoves);



        frontLineMoves.SetActive(false);
        wizardMoves.SetActive(false);
    }
    // decreases AP by 1 and updates AP accordingly. Returns true if it can be done and false if the value is already 0
    public bool increase_AP(int num) 
    {
        if (currentAP + num < 0)
        {
            return false;
        }
        else 
        { 
            set_AP(currentAP + num); 
            return true;
        }
    }
    void set_AP(int num)
    {
        currentAP = num;
        AP_TEXT.text = "AP:" + currentAP.ToString();
    }
    public void set_nextTurnEnemyIncreaseHealth(int num)
    {
        nextTurnEnemyHealth = num;
    }
    void nextTurn() 
    {
        resetAP();
        if(currentPlayer == playerCount - 1)
        {
            currentPlayer = 0;
            currentTurn += 1;
            turn.text = "Turn " + currentTurn.ToString();
            increaseEnemyHealth(nextTurnEnemyHealth);
            nextTurnEnemyHealth = 0;
        }
        else
        {
            currentPlayer += 1;
        }
        string playertext = "Current Player: ";
        switch (currentPlayer)
        {
            case 0:
                playertext += "Wizard";
                break;
            case 1:
                playertext += "Captain";
                break;
            case 2:
                playertext += "Engineer";
                break;
            case 3:
                playertext += "Frontline Right";
                break;
            case 4:
                playertext += "Frontline Left";
                break;
        }
        currentPlayerText.text = playertext;
        frontLineMoves.SetActive(false);
        wizardMoves.SetActive(false);
    }

    public void resetMoves()
    {
        resetAP();
    }
    public void resetAP()
    {
        set_AP(MAX_AP);
    }
    //increases the player's health by num, capping at MAX_PLAYER_HEALTH and 0. Returns whether the player is dead or not
    public bool increasePlayerHealth(int num)
    {
        if(currentPlayerHealth + num <= 0)
        {
            alterPlayerHealth(0);
            return true;
        }
        else if(currentPlayerHealth + num > MAX_PLAYER_HEALTH) 
        {
            alterPlayerHealth(100);
        }
        else
        {
            alterPlayerHealth(currentPlayerHealth + num);
        }
        return false;
    }
    //increases the enemy's health by num, capping at MAX_PLAYER_HEALTH and 0. Returns whether the player is dead or not
    public bool increaseEnemyHealth(int num)
    {
        if (currentEnemyHealth + num <= 0)
        {
            alterEnemyHealth(0);
            return true;
        }
        else if (currentEnemyHealth + num > MAX_ENEMY_HEALTH)
        {
            alterEnemyHealth(100);
        }
        else
        {
            alterEnemyHealth(currentEnemyHealth + num);
        }
        return false;
    }

    public void alterPlayerHealth(int newHealth)
    {
        currentPlayerHealth = newHealth;
        healthBarPlayer.value = (float)currentPlayerHealth/ (float)MAX_PLAYER_HEALTH;
        playerHealthText.text = currentPlayerHealth.ToString();
    }
    public void alterEnemyHealth(int newHealth)
    {
        currentEnemyHealth = newHealth;
        healthBarEnemy.value = (float)currentEnemyHealth / (float)MAX_ENEMY_HEALTH;
        enemyHealthText.text = currentEnemyHealth.ToString();
    }
    public bool isPiecesTurn(int id)
    {
        return (id == currentPlayer);
    }
    public void toggleMoves() 
    { 
        if(!isMovesActive)
        {
            if(currentPlayer == 3 || currentPlayer == 4)
            {
                frontLineMoves.SetActive(true);
            }
            else if(currentPlayer == 0)
            {
                wizardMoves.SetActive(true);
            }
        }
        else
        {
            frontLineMoves.SetActive(false);
            wizardMoves.SetActive(false);
        }
        isMovesActive = !isMovesActive;
    }
}