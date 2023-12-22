using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class moveHandler : MonoBehaviour
{
    public GameObject LevelManager;
    private Level_Manager levelScript;
    public GameObject[] pieces;

    public GameObject shortCannon1;
    public GameObject shortCannon2;
    public GameObject diningHall;
    public GameObject longCannon1;
    public GameObject longCannon2;
    public GameObject wizardTower;


    public Button bombardShortButton;
    public Button bombardLongButton;
    public Button fireballButton;


    [System.NonSerialized]
    public int fireBallsLeft;
    [System.NonSerialized]
    public int forceFields;
    [System.NonSerialized]
    public int stuntsLeft;
    void Start()
    {
        levelScript = LevelManager.GetComponent<Level_Manager>();

        bombardShortButton.onClick.AddListener(bombardShort);
        bombardLongButton.onClick.AddListener(bombardLong);
        fireballButton.onClick.AddListener(fireBall);
        fireBallsLeft = 2;
        forceFields = 2;
        stuntsLeft = 2;
    }
    //Given the indexes of two pieces, returns whether or not they are in the same room
    bool areInRoomTogether(int piece1, int piece2)
    {
        GameObject currentRoom1 = pieces[piece1].GetComponent<pieceTest>().currentRoom;
        GameObject currentRoom2 = pieces[piece2].GetComponent<pieceTest>().currentRoom;
        return currentRoom1 == currentRoom2;
    }
    //Given a room and an index of a piece, returns if the given piece is in that room
    bool isPieceInRoom(int index, GameObject room)
    {
        GameObject currentPiece = pieces[index];
        GameObject currentRoom = currentPiece.GetComponent<pieceTest>().currentRoom;
        return currentRoom.name == room.name;
    }
    void bombardShort()
    {
        int currentPlayerIndex = LevelManager.GetComponent<Level_Manager>().currentPlayer;
        bool isCannon1 = isPieceInRoom(currentPlayerIndex, shortCannon1);
        bool isCannon2 = isPieceInRoom(currentPlayerIndex, shortCannon2);
        if (isCannon1 || isCannon2) { 
            if (LevelManager.GetComponent<Level_Manager>().increase_AP(-2))
            {
                LevelManager.GetComponent<Level_Manager>().increaseEnemyHealth(-10);
                int otherFrontline = 3;
                if(currentPlayerIndex == 3)
                {
                    otherFrontline = 4;
                }
                if((isCannon1 && isPieceInRoom(otherFrontline, shortCannon2)) || 
                    (isCannon2 && isPieceInRoom(otherFrontline, shortCannon1)))
                {
                    LevelManager.GetComponent<Level_Manager>().increaseEnemyHealth(-5);
                }
            }
        }
    }
    void bombardLong()
    {
        int currentPlayerIndex = LevelManager.GetComponent<Level_Manager>().currentPlayer;
        bool isCannon1 = isPieceInRoom(currentPlayerIndex, longCannon1);
        bool isCannon2 = isPieceInRoom(currentPlayerIndex, longCannon2);
        if (isCannon1 || isCannon2)
        {
            if (LevelManager.GetComponent<Level_Manager>().increase_AP(-2))
            {
                LevelManager.GetComponent<Level_Manager>().increaseEnemyHealth(-5);
                int otherFrontline = 3;
                if (currentPlayerIndex == 3)
                {
                    otherFrontline = 4;
                }
                if ((isCannon1 && isPieceInRoom(otherFrontline, longCannon2)) ||
                    (isCannon2 && isPieceInRoom(otherFrontline, longCannon1)))
                {
                    LevelManager.GetComponent<Level_Manager>().increaseEnemyHealth(-5);
                }
            }
        }
    }
    void fireBall()
    {
        int currentPlayerIndex = LevelManager.GetComponent<Level_Manager>().currentPlayer;
        if (isPieceInRoom(currentPlayerIndex, wizardTower) && LevelManager.GetComponent<Level_Manager>().increase_AP(-2))
        {
            LevelManager.GetComponent<Level_Manager>().increaseEnemyHealth(-5);
            LevelManager.GetComponent<Level_Manager>().set_nextTurnEnemyIncreaseHealth(-5);
        }
    }
}
