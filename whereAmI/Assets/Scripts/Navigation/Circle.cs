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
    public AstronomicalObject target;
    
    private LineRenderer _lr;
    private TiltNavigation tn;
    
    public void Init(float posX, float posY, float radius, float lineWidth, AstronomicalObject target)
    {
        this.posX = posX;
        this.posY = posY;
        this.radius = radius;
        this.segments = GlobalManager.instance._global_CircleSegments;
        this.lineWidth = lineWidth;
        this.target = target;
        
        _lr = gameObject.GetComponent<LineRenderer>();
        if(_lr == null)
            _lr = gameObject.AddComponent<LineRenderer>();

        tn = Camera.main.GetComponent<TiltNavigation>();

        DrawCircle();
    }

    private void LateUpdate()
    {
        float camDist = tn.GetDistance();
        if (segments != GlobalManager.instance._global_CircleSegments || 
            radius > target._easyView_OrbitRadius_Current || radius < target._easyView_OrbitRadius_Current)
        {
            segments = GlobalManager.instance._global_CircleSegments;
            radius = target._easyView_OrbitRadius_Current;
            posX = target._easyView_OrbitCenter.position.x;
            posY = target._easyView_OrbitCenter.position.y;
            DrawCircle();
        }

        lineWidth = Mathf.Pow(Mathf.Abs(camDist/100), 0.8f);
        float tempWith = lineWidth - (lineWidth * GlobalManager.instance._global_NormalizeDistance - 0.01f);  
        _lr.startWidth = tempWith;
        _lr.endWidth = tempWith;
    }

    void DrawCircle()
    {
//        Debug.Log("Draw Circle");
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
    
}
