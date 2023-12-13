using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level_Manager : MonoBehaviour
{
    public const int MAX_AP = 2;
    public const int MAX_TURN = 15;
    public int currentAP;
    public int currentTurn;
    public int playerCount;
    public int current_player;
    public TMP_Text AP_TEXT;
    public Button endTurnButton;
    // Start is called before the first frame update
    void Start()
    {
        increase_AP(-1* currentAP);
        endTurnButton.onClick.AddListener(resetAP);
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
            update_AP(currentAP + num); 
            return true;
        }
    }
    void update_AP(int num)
    {
        currentAP = num;
        AP_TEXT.text = "AP:" + currentAP.ToString();
    }
    void onClick() 
    {
        update_AP(MAX_AP);
    }
    void nextTurn() 
    {
        resetAP();
        if(current_player == playerCount -1)
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
    /*
    // Update is called once per frame
    void Update()
    {
        
    }*/
}
