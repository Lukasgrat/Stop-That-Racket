using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level_Manager : MonoBehaviour
{
    public const int MAX_AP = 2;
    public const int MAX_TURN = 15;
    public int current_AP;
    public int current_turn;
    public int player_count;
    public int current_player;
    public TMP_Text AP_TEXT;
    // Start is called before the first frame update
    void Start()
    {
        current_AP = MAX_AP;
        current_turn = 0;
        current_player = 0;
        decrease_AP(current_AP);
    }
    // decreases AP by 1 and updates AP accordingly. Returns true if it can be done and false if the value is already 0
    bool decrease_AP() 
    {
        if (current_AP == 0)
        {
            return false;
        }
        else 
        { 
            update_AP(current_AP - 1); 
            return true;
        }
    }
    void update_AP(int num)
    {
        if (current_AP == 0) { return false; }
        else 
        {
            current_AP -= 1;
            update_AP_TEXT(current_AP);
            AP_TEXT.text = "AP:" + current_AP.ToString();
            return true;
        }
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }*/
}
