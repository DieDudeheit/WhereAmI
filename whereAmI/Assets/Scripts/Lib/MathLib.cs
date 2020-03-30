using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathLib
{

    public static Vector3 GetCurrentPos_Circle(float xPos, float yPos, float radius, float t, float axisTilt)
    {
        float angle = Mathf.Deg2Rad * 360 * t;
        
        float x = (radius * Mathf.Cos(t)) + xPos;
        float z = (radius * Mathf.Sin(t)) + yPos;
        
        //Tilt
//        float y = radius * Mathf.Sin(axisTilt);
        float y = (Mathf.Sin(Mathf.PI * axisTilt/180)) * Mathf.Cos(t) * radius;
        
        
        return new Vector3(x, y, z);
    }
    
}
