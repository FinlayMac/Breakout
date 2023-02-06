using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FinlayUtilities2D
{

    /*Floats for angle and magnitude are passed in
     * returns a Vector 2
     */
    public static Vector2 AngleToVector(float angle, float magnitude)
    {
        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * magnitude;
    }


    /** Finds the angle in radians between two game objects
     * Set degreesYes to get the angle back in degrees
     * Returns a float angleBetween
     */
    public static float AngleDifference(GameObject obj1, GameObject obj2, bool degreesYes)
    {
        //finds the difference in locations
        float diffX = obj1.transform.position.x - obj2.transform.position.x;
        float diffY = obj1.transform.position.y - obj2.transform.position.y;

        //calculates the angle in radians between two points (0 points up, -1.5 points left, 1.5 points right)
        float angleBetween = Mathf.Atan(diffX / diffY);

        //stops the ball screwing up if it hits below the y height of the paddle
        if (diffY < 0)
        {
            angleBetween = -angleBetween;
        }

        if (degreesYes == true)
        {
            //converts the angle to degrees instead
            angleBetween = (Mathf.Rad2Deg * angleBetween);
        }
        //angle in degrees
        return angleBetween;

    }
}
