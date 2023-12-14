using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
public class pieceTest : MonoBehaviour
{
    public Sprite regularSprite;
    public int charcter_ID;
    public Sprite highlightedSprite;
    public GameObject LevelManager;
    private bool isSelected;
    public GameObject initialRoom;
    Camera m_Camera;
    [System.NonSerialized]
    public GameObject currentRoom;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        isSelected = false;
        currentRoom = initialRoom;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var spriteRender = GetComponent<SpriteRenderer>();
            bool wasHit = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);
            foreach (var hit in hits)
            {
                if (hit.collider.name == name)
                {
                    Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                    spriteRender.sprite = highlightedSprite;
                    isSelected = true;
                    wasHit = true;
                }
            }
            if(! wasHit)
            {   
                if(hits.Length > 0) { 
                    int index = roomIndex(currentRoom.GetComponent<gameRoom>().neighborRooms, hits[0]);
                    if (isSelected && index != -1)
                    {
                        currentRoom = currentRoom.GetComponent<gameRoom>().neighborRooms[index];
                        if (charcter_ID < currentRoom.GetComponent<gameRoom>().characterPositions.Length)
                        {
                            movePlayer(currentRoom.transform.position + (currentRoom.GetComponent<gameRoom>().characterPositions)[charcter_ID]);
                        }
                        else
                        {
                            movePlayer(currentRoom.transform.position);
                        }

                    }
                }
                spriteRender.sprite = regularSprite;
                isSelected = false;
            }
        }
    }
    void movePlayer(Vector3 pos)
    {
        if (LevelManager.GetComponent<Level_Manager>().increase_AP(-1))
        {
            transform.position = pos;
        }
    }
   // returns the index of the neigboring room the ray is hitting. If its not a neighboring room, returns -1
    int roomIndex(GameObject[] rooms, RaycastHit2D hit)
    {
        for (int x = 0; x < rooms.Length; x++)
        {
            if (rooms[x].name == hit.collider.name) 
            {
                return x;
            }
        }
        return -1;
    }
}