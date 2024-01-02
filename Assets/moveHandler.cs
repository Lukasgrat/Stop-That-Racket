using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Button forcefieldButton;
    public Button teleportButton;
    public Button inspireButton;
    public Button moveForward;
    public Button moveBackward;
    public Button setSailsButton;

    [System.NonSerialized]
    public int fireBallsLeft;
    public TMP_Text fireBallsLeftText;
    [System.NonSerialized]
    public int forceFieldsLeft;
    public TMP_Text forceFieldsLeftText;
    [System.NonSerialized]
    public int stuntsLeft;
    public TMP_Text stuntsLeftText;
    void Start()
    {
        levelScript = LevelManager.GetComponent<Level_Manager>();

        bombardShortButton.onClick.AddListener(bombardShort);
        bombardLongButton.onClick.AddListener(bombardLong);
        fireballButton.onClick.AddListener(fireBall);
        forcefieldButton.onClick.AddListener(forceField);
        teleportButton.onClick.AddListener(teleportInitializeUI);
        inspireButton.onClick.AddListener(inspireInitializeUI);
        setSailsButton.onClick.AddListener(setSail);
        moveForward.onClick.AddListener(delegate { move(-1); });
        moveBackward.onClick.AddListener(delegate { move(1); });
        fireBallsLeft = 2;
        forceFieldsLeft = 2;
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

        Debug.Log(fireBallsLeft > 0);
        Debug.Log((isPieceInRoom(levelScript.currentPlayer, longCannon1) || isPieceInRoom(levelScript.currentPlayer, longCannon2)));
        if (fireBallsLeft > 0 
            &&  (isPieceInRoom(levelScript.currentPlayer, longCannon1) || isPieceInRoom(levelScript.currentPlayer, longCannon2))
            && levelScript.increase_AP(-2))
        {
            Debug.Log("Here");
            levelScript.increaseEnemyHealth(-5);
            levelScript.set_nextTurnEnemyIncreaseHealth(-5);
            fireBallsLeft -= 1;
            fireBallsLeftText.text = "Fireballs left: " + fireBallsLeft;
        }
    }
    void forceField()
    {
        if (forceFieldsLeft > 0
            && (isPieceInRoom(levelScript.currentPlayer, wizardTower))
            && levelScript.increase_AP(-2))
        {
            levelScript.isForceFieldActive = true;
            forceFieldsLeft -= 1;
            forceFieldsLeftText.text = "Forcefields left: " + forceFieldsLeft;
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
        pieces[x].GetComponent<pieceTest>().isNextMoveTeleport = true;
    }
    void move(int x)
    {
        if (levelScript.increase_AP(-1) && levelScript.currentDistance != 0)
        {
            if (areInRoomTogether(1, 2)){
                levelScript.increase_distance(x * 2);
            }
            else
            {
                levelScript.increase_distance(x);
            }
        }
    }
    void setSail()
    {

        if (isPieceInRoom(1, commandCenter))
        {
            levelScript.toggleMovement();
        }
    }
}
