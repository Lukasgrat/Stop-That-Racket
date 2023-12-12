using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pieceTest : MonoBehaviour
{
    public Sprite regularSprite;
    public Sprite highlightedSprite;
    private bool isSelected;
    Camera m_Camera;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            var spriteRender = GetComponent<SpriteRenderer>();
            if (hit.collider != null && hit.collider.name == name)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                spriteRender.sprite = highlightedSprite;
                isSelected = true;
            }
            else
            {
                if (isSelected)
                {
                    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                spriteRender.sprite = regularSprite;
                isSelected = false;
            }
        }
    }
}