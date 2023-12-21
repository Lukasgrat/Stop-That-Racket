using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class moveHandler : MonoBehaviour
{
    public GameObject LevelManager;
    public Button bombardShortButton;
    public Button bombardLongButton;
    public GameObject[] pieces;
    public GameObject shortCannon1;
    public GameObject shortCannon2;
    public GameObject diningHall;
    void Start()
    {
        bombardShortButton.onClick.AddListener(bombardShort);
        bombardLongButton.onClick.AddListener(bombardLong);
    }
    void bombardShort()
    {
        GameObject currentPlayer = pieces[LevelManager.GetComponent<Level_Manager>().currentPlayer];
        GameObject currentRoom = currentPlayer.GetComponent<pieceTest>().currentRoom;
        if(currentRoom.name == shortCannon1.name || currentRoom.name == shortCannon2.name) { 
            if (LevelManager.GetComponent<Level_Manager>().increase_AP(-2))
            {
                LevelManager.GetComponent<Level_Manager>().increaseEnemyHealth(-10);
            }
        }

    }
    void bombardLong()
    {
        if (LevelManager.GetComponent<Level_Manager>().increase_AP(-2))
        {
            LevelManager.GetComponent<Level_Manager>().increaseEnemyHealth(-15);
        }

    }
}
