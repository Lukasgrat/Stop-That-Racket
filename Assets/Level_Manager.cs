using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
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
    public Button ToggleMovesButton;
    public GameObject MoveHandler;
    public GameObject movementTeleport;


    public GameObject frontLineMoves;
    public GameObject wizardMoves;
    public GameObject captainMoves;

    public TMP_Text distanceText;
    public int currentDistance;
    public GameObject selectPieceUI;
    public Button[] selectPieceUIRoles;
    public string selectPieceMove;

    public GameObject shipMovementControls;

    public Button toggleSpellsButton;
    public GameObject wizardSpells;
    public TMP_Text turn;

    private int nextTurnEnemyHealth;
    public bool isForceFieldActive;

    [System.NonSerialized]
    public int currentAP;
    [System.NonSerialized]
    public int currentTurn;
    [System.NonSerialized]
    public int currentPlayer;
    [System.NonSerialized]
    public bool isInspire;

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
        isInspire = false;
        isForceFieldActive = false;
        currentAP = MAX_AP;
        currentTurn = 1;
        currentPlayer = 0;
        nextTurnEnemyHealth = 0;

        endTurnButton.onClick.AddListener(nextTurn);
        resetMovesButton.onClick.AddListener(resetMoves);
        ToggleMovesButton.onClick.AddListener(toggleMoves);
        toggleSpellsButton.onClick.AddListener(toggleSpells);
        selectPieceUIRoles[0].onClick.AddListener(delegate { selectPieceHandler(0);});
        selectPieceUIRoles[1].onClick.AddListener(delegate { selectPieceHandler(1);});
        selectPieceUIRoles[2].onClick.AddListener(delegate { selectPieceHandler(2);});
        selectPieceUIRoles[3].onClick.AddListener(delegate { selectPieceHandler(3);});
        selectPieceUIRoles[4].onClick.AddListener(delegate { selectPieceHandler(4);});
        clearMoveUI();
        setDistanceText();
    }
    // increases AP by num and updates AP accordingly. Returns true if it can be done and false if the value is already 0
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
        if(isInspire)
        {
            currentPlayer = 1;
            isInspire = false;
        }
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
        setPlayerTurnText();
        frontLineMoves.SetActive(false);
        wizardMoves.SetActive(false);
    }
    void setPlayerTurnText()
    {
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
    }
    // increases the distance by num and updates AP accordingly. Returns whether the value becomes 0
    public bool increase_distance(int num)
    {
        currentDistance = currentDistance + num;
        if (currentDistance <= 0)
        {
            currentDistance = 0;
            setDistanceText();
            return true;
        }
        else
        {
            setDistanceText();
            return false;
        }
    }
    void setDistanceText()
    {
        if (currentDistance == 1)
        {
            distanceText.text = "You are " + currentDistance + " Lightyear away from Dr. Racket.";
        }
        else
        {
            distanceText.text = "You are " + currentDistance + " Lightyears away from Dr. Racket.";
        }
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
        if (isForceFieldActive)
        {
            isForceFieldActive = false;
            num /= 2;
        }
        if (currentPlayerHealth + num <= 0)
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
            else if (currentPlayer == 1)
            {
                captainMoves.SetActive(true);
            }
        }
        else
        {
            clearMoveUI();
        }
        isMovesActive = !isMovesActive;
    }
    public void toggleMovement()
    {
        shipMovementControls.SetActive(!shipMovementControls.activeSelf);
    }
    public void toggleSpells()
    {
        wizardSpells.SetActive(!wizardSpells.activeSelf);
    }
    public void clearMoveUI()
    {
        frontLineMoves.SetActive(false);
        wizardMoves.SetActive(false);
        selectPieceUI.SetActive(false);
        movementTeleport.SetActive(false);
        captainMoves.SetActive(false);
        shipMovementControls.SetActive(false);
        wizardSpells.SetActive(false);
        selectPieceMove = "none";
    }

    //Given a list of pieceIDs, sets the selection UI containing those IDs
    public void setSelectPieceUI(List<int> displayingIDS)
    {
        selectPieceUI.SetActive(true);
        for(int i = 0; i < selectPieceUIRoles.Length; i++)
        {
            if (!(displayingIDS.Contains(i)))
            {
                selectPieceUIRoles[i].gameObject.SetActive(false);
            }
            else
            {
                selectPieceUIRoles[i].gameObject.SetActive(true);
            }
        }
    }

    public void selectPieceHandler(int index)
    {
        string displaycase = selectPieceMove;
        clearMoveUI();
        switch (displaycase)
        {
            case "teleport":
                MoveHandler.GetComponent<moveHandler>().teleportLocation(index);
                movementTeleport.SetActive(true);
                break;
            case "inspire":
                isInspire = true;
                resetAP();
                currentPlayer = index;
                setPlayerTurnText();
                break;
        }
    }
}