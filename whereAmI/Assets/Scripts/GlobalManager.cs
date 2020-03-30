using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager instance;

    /// <summary>
    /// 1 = realtime
    /// </summary>
    public float _global_TimeSpeed;
    [FormerlySerializedAs("_global_TimeStepSize")] public float _global_UpdateTime;
    public float _global_Scale;
    [FormerlySerializedAs("_global_ObjScale")] public float _global_PlanetScale;
    [FormerlySerializedAs("_global_Normalize")] [Range(0,1)]
    public float _global_NormalizeDistance;
    [Range(0,1)]
    public float _global_NormalizeScale;
    [Range(0,1)]
    public float _global_NormalizeOrbitTilts;

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
    
    public TimeSpan GetTimeSpanSinceProgressStart(DateTime dateTime)
    {
        return dateTime.Date - progressStartDate;
    }

}
