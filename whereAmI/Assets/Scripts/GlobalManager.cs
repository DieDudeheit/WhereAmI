using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Serialization;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager instance;

    /// <summary>
    /// 1 = realtime
    /// </summary>
    public float _global_TimeSpeed;
    public float _global_TimeStepSize;
    public float _global_Scale;
    public float _global_ObjScale;
    [FormerlySerializedAs("_global_Normalize")] [Range(0,1)]
    public float _global_NormalizeDistance;
    [Range(0,1)]
    public float _global_NormalizeScale;

    public string _progressStart;
    private DateTime progressStartDate;
    
    [Range(16,180)]
    public int _global_CircleSegments;

    [Header("UI Stuff")] 
    public GameObject planetNameBillboard;
    public float billboardScale;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        StringToDate();
    }

    void StringToDate()
    {
        progressStartDate = DateTime.Parse(_progressStart);
        Debug.Log(progressStartDate);
    }

    public TimeSpan GetTimeSpanSinceProgressStart()
    {
        return DateTime.Now - progressStartDate;
    }
    
}
