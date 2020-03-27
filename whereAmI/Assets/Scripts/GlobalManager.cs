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
    public float _global_ObjScale;
    [FormerlySerializedAs("_global_Normalize")] [Range(0,1)]
    public float _global_NormalizeDistance;
    [Range(0,1)]
    public float _global_NormalizeScale;
    
    [Range(16,180)]
    public int _global_CircleSegments;
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
    }

}
