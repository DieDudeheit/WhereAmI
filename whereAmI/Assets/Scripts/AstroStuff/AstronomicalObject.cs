using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Serialization;

public class AstronomicalObject : MonoBehaviour
{
    
    [Header("Basics")]
    public string name;
    
    /// <summary>
    ///Diameter in kilometer
    /// </summary>
    public float diameter;
    /// <summary>
    /// CurrentProgress in Days
    /// </summary>
    public float currentProgress;
    
    #region easyView

        [Header("Easy view")]
        public Transform _easyView_OrbitCenter;
        /// <summary>
        /// Orbit radius in mio km
        /// </summary>
        public float _easyView_OrbitRadius;
        public float _easyView_OrbitRadius_Current;
        /// <summary>
        /// Orbit time in days
        /// </summary>
        public float _easyView_OrbitTime;

        public int systemIndex;
    
    #endregion

    public List<AstronomicalObject> _astronomicalObjects = new List<AstronomicalObject>();
    private List<KeyValuePair<string, string>> information = new List<KeyValuePair<string, string>>();

    public virtual void Init(int index)
    {
        this.systemIndex = index;
        SetTranform();
        InitChilds();
        SetCircle();
    }

    public void SetTranform()
    {
        //Parenting
        //transform.SetParent(_easyView_OrbitCenter);
        
        //Scaling
        float normalizedScaleValue = diameter - diameter * GlobalManager.instance._global_NormalizeScale; 
        transform.localScale = Vector3.one * GlobalManager.instance._global_NormalizeScale + Vector3.one * (normalizedScaleValue/1000000.0f);
        transform.localScale *= GlobalManager.instance._global_ObjScale;
        
        //diameter
        float normalizedOrbitRadius = _easyView_OrbitRadius - _easyView_OrbitRadius * Mathf.Pow(GlobalManager.instance._global_NormalizeDistance, 0.25f);
        _easyView_OrbitRadius_Current = systemIndex * GlobalManager.instance._global_NormalizeDistance + normalizedOrbitRadius;
        _easyView_OrbitRadius_Current *= GlobalManager.instance._global_ObjScale;

//        Debug.Log("scale: " + transform.localScale);
//        Debug.Log("Orbit: " + _easyView_OrbitRadius_Current);
    }

    public void InitChilds()
    {
        int i = 0;
        foreach (var astroObj in _astronomicalObjects)
        {
            if(astroObj._easyView_OrbitCenter == null)
                astroObj._easyView_OrbitCenter = transform;
            
            GameObject obj = Instantiate(astroObj.gameObject);
            astroObj.name = obj.name;
            obj.SetActive(true);
            obj.GetComponent<AstronomicalObject>().Init(i);
            i++;
        }
    }

    public void SetCircle()
    {
        
        Circle circle = gameObject.AddComponent<Circle>();
        
        circle.Init(_easyView_OrbitCenter.position.x, _easyView_OrbitCenter.position.z,
            _easyView_OrbitRadius_Current, 1 );
    }

}
