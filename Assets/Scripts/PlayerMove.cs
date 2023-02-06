using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerWidth = 0.75f;
    private float playerYHeight = -3.5f;

    void OnMouseDrag()
    {
        //gets position of mouse
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //sets the position of the mouse to the in game position
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Clamps the position of the x coordinate - restricts it
        Vector2 playerPos = new Vector2(Mathf.Clamp(objPosition.x, -7f, 7f), playerYHeight);
        //finally sets the player position by drag
        transform.position = playerPos;

    }
    
}
