using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathLib
{

    public static Vector2 GetCurrentPos_Circle(float xPos, float yPos, float radius, float t)
    {
        float angle = Mathf.Deg2Rad * 360 * t;
        
        float x = radius * Mathf.Cos(t) + xPos;
        float y = radius * Mathf.Sin(t) + yPos;
        
        return new Vector2(x, y);
    }
    
}
