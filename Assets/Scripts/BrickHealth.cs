using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHealth : MonoBehaviour
{
    //how much damage the brick can take
    private int health = 3;

    //takes the damage from the ball
    public void DamageBrick(int damage)
    {

        health -= damage;
        if (health <= 0)
        {
            DestroyBrick();
        }
    }

    //if health runs out, destroy the brick
    private void DestroyBrick()
    {
       // Debug.Log("Kill Brick");
        gameObject.SetActive(false);
    }
}
