using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public float posX;
    public float posY;
    
    public float radius;
    public int segments;
    public float lineWidth;

    private LineRenderer _lr;
    public void Init(float posX, float posY, float radius, float lineWidth)
    {
        this.posX = posX;
        this.posY = posY;
        this.radius = radius;
        this.segments = GlobalManager.instance._global_CircleSegments;
        this.lineWidth = lineWidth;
        
        _lr = gameObject.GetComponent<LineRenderer>();
        if(_lr == null)
            _lr = gameObject.AddComponent<LineRenderer>();


        DrawCircle();
    }

    private void Update()
    {
        if (segments != GlobalManager.instance._global_CircleSegments)
        {
            segments = GlobalManager.instance._global_CircleSegments;
            DrawCircle();
        }
    }

    void DrawCircle()
    {
        Debug.Log("Draw Circle");
        _lr.positionCount = segments + 1;

        Vector3[] points = new Vector3[segments + 1];
        
        for (int i = 0; i <= segments; i++)
        {
            float t =  (i * 1.0f) / (segments * 1.0f) *  Mathf.PI * 2;
            Vector2 tempPos2D = MathLib.GetCurrentPos_Circle(posX, posY, radius, t);
            points [i] = new Vector3(tempPos2D.x, 0, tempPos2D.y);
        } 
        _lr.SetPositions(points);
    }

//    private void Update()
//    {
//        lineWidth = Camera.main.transform.position
//    }
}
