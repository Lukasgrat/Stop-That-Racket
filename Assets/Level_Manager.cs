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
    public int playerCount;
    public TMP_Text AP_TEXT;
    public Button endTurnButton;
    public Slider healthBarPlayer;
    public TMP_Text playerHealthText;

    [System.NonSerialized]
    public int currentAP;
    [System.NonSerialized]
    public int currentTurn;
    [System.NonSerialized]
    public int current_player;

    [System.NonSerialized]
    public int currentPlayerHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHealth = MAX_PLAYER_HEALTH;
        currentAP = MAX_AP;
        currentTurn = 1;
        current_player = 0;
        increase_AP(-1* currentAP);
        endTurnButton.onClick.AddListener(resetAP);
    }
    // decreases AP by 1 and updates AP accordingly. Returns true if it can be done and false if the value is already 0
    public bool increase_AP(int num) 
    {
        increaseHealth(-10);
        if (currentAP + num < 0)
        {
            return false;
        }
        else 
        { 
            update_AP(currentAP + num); 
            return true;
        }
    }
    void update_AP(int num)
    {
        currentAP = num;
        AP_TEXT.text = "AP:" + currentAP.ToString();
    }
    void nextTurn() 
    {
        resetAP();
        if(current_player == playerCount - 1)
        {
            current_player = 0;
            currentTurn += 1;
        }
        else
        {
            current_player += 1;
        }
    }
    public void resetAP()
    {
        update_AP(MAX_AP);
    }
    //increases the player's health by num, capping at MAX_PLAYER_HEALTH and 0. Returns whether the player is dead or not
    bool increaseHealth(int num)
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

    public void alterPlayerHealth(int newHealth)
    {
        currentPlayerHealth = newHealth;
        healthBarPlayer.value = (float)currentPlayerHealth/ (float)MAX_PLAYER_HEALTH;
        playerHealthText.text = currentPlayerHealth.ToString();
    }
}
