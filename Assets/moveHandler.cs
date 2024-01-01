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
    public GameObject commandCenter;

    public Button bombardShortButton;
    public Button bombardLongButton;
    public Button fireballButton;
    public Button teleportButton;
    public Button inspireButton;



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
        teleportButton.onClick.AddListener(teleportInitializeUI);
        inspireButton.onClick.AddListener(inspireInitializeUI);
        fireBallsLeft = 2;
        forceFields = 2;
        stuntsLeft = 2;
    }
    //Given the indexes of two pieces, returns whether or not they are in the same room
    bool areInRoomTogether(int piece1, int piece2)
    {
        GameObject currentRoom1 = pieces[piece1].GetComponent<pieceTest>().currentRoom;
        GameObject currentRoom2 = pieces[piece2].GetComponent<pieceTest>().currentRoom;
        return currentRoom1.name == currentRoom2.name;
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
        int currentPlayerIndex = levelScript.currentPlayer;
        bool isCannon1 = isPieceInRoom(currentPlayerIndex, shortCannon1);
        bool isCannon2 = isPieceInRoom(currentPlayerIndex, shortCannon2);
        if (isCannon1 || isCannon2) { 
            if (levelScript.increase_AP(-2))
            {
                levelScript.increaseEnemyHealth(-10);
                int otherFrontline = 3;
                if(currentPlayerIndex == 3)
                {
                    otherFrontline = 4;
                }
                if((isCannon1 && isPieceInRoom(otherFrontline, shortCannon2)) || 
                    (isCannon2 && isPieceInRoom(otherFrontline, shortCannon1)))
                {
                    levelScript.increaseEnemyHealth(-5);
                }
            }
        }
    }
    void bombardLong()
    {
        int currentPlayerIndex = levelScript.currentPlayer;
        bool isCannon1 = isPieceInRoom(currentPlayerIndex, longCannon1);
        bool isCannon2 = isPieceInRoom(currentPlayerIndex, longCannon2);
        if (isCannon1 || isCannon2)
        {
            if (levelScript.increase_AP(-2))
            {
                levelScript.increaseEnemyHealth(-5);
                int otherFrontline = 3;
                if (currentPlayerIndex == 3)
                {
                    otherFrontline = 4;
                }
                if ((isCannon1 && isPieceInRoom(otherFrontline, longCannon2)) ||
                    (isCannon2 && isPieceInRoom(otherFrontline, longCannon1)))
                {
                    levelScript.increaseEnemyHealth(-5);
                }
            }
        }
    }
    void fireBall()
    {
        int currentPlayerIndex = levelScript.currentPlayer;
        if (isPieceInRoom(currentPlayerIndex, wizardTower) && levelScript.increase_AP(-2))
        {
            levelScript.increaseEnemyHealth(-5);
            levelScript.set_nextTurnEnemyIncreaseHealth(-5);
        }
    }
    void teleportInitializeUI()
    {
        levelScript.clearMoveUI();
        List<int> IDList = new List<int>();
        for (int x = 1; x < levelScript.playerCount; x++)
        {
            if (areInRoomTogether(0, x))
            {
                IDList.Add(x);
            }
        }
        levelScript.setSelectPieceUI(IDList);
        levelScript.selectPieceMove = "teleport";
    }
    void inspireInitializeUI()
    {
        levelScript.clearMoveUI();
        List<int> IDList = new List<int>();
        for (int x = 0; x < levelScript.playerCount; x++)
        {
            if (x != 1 && areInRoomTogether(1, x))
            {
                IDList.Add(x);
            }
        }
        levelScript.setSelectPieceUI(IDList);
        levelScript.selectPieceMove = "inspire";
    }
    //Given the index of a piece, sets up the UI for movement and changes the piece's respective next movement flag
    public void teleportLocation(int x)
    {
        Debug.Log(x);
        pieces[x].GetComponent<pieceTest>().isNextMoveTeleport = true;
    }
}
