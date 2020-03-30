using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AstronomicalObject : MonoBehaviour
{
    
    [FormerlySerializedAs("name")] [Header("Basics")]
    public string planetName;
    
    /// <summary>
    ///Diameter in kilometer
    /// </summary>
    public float diameter;
    /// <summary>
    ///Full rotation time in days
    /// </summary>
    public float axisRotationTime;
    /// <summary>
    ///Planet axis rotation 1 = 100%
    /// </summary>
    public float currentAxisRotationProgress;
    /// <summary>
    ///Planet axis tilt in degrees
    /// </summary>
    public float nativeAxisTilt;
    /// <summary>
    /// CurrentProgress in Days
    /// </summary>
    public float currentProgress;

    /// <summary>
    /// CurrentProgress in Days
    /// </summary>
    public float currentProgressStart;

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

    #region correctView

        [Header("Correct view")]
        public float _correctView_OrbitTilt;
        public float current_OrbitTilt;

    #endregion

    public List<AstronomicalObject> _astronomicalObjects = new List<AstronomicalObject>();

    public virtual void Init(int index)
    {
        this.systemIndex = index;
        SetCurrentProgress();
        SetAxisTilt();
        SetTranform();
        InitChilds();
        SetCircle();
        SetUI();
    }

    public void SetTranform()
    {
        //Parenting
        //transform.SetParent(_easyView_OrbitCenter);
        
        //Scaling
        float normalizedScaleValue = diameter - diameter * GlobalManager.instance._global_NormalizeScale; 
        transform.localScale = Vector3.one * GlobalManager.instance._global_NormalizeScale + Vector3.one * (normalizedScaleValue/1000000.0f);
        transform.localScale *= GlobalManager.instance._global_Scale * GlobalManager.instance._global_PlanetScale;
        
        //diameter
        float normalizedOrbitRadius = _easyView_OrbitRadius - _easyView_OrbitRadius * Mathf.Pow(GlobalManager.instance._global_NormalizeDistance, 0.25f);
        _easyView_OrbitRadius_Current = systemIndex * GlobalManager.instance._global_NormalizeDistance + normalizedOrbitRadius;
        _easyView_OrbitRadius_Current *= GlobalManager.instance._global_Scale;
        
        //Orbit Tilt
        current_OrbitTilt = _correctView_OrbitTilt - _correctView_OrbitTilt * Mathf.Pow(GlobalManager.instance._global_NormalizeOrbitTilts, 0.25f);

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
            obj.name = obj.name.Replace("(Clone)", "");
            astroObj.planetName = obj.name;
            obj.SetActive(true);
            obj.GetComponent<AstronomicalObject>().Init(i);
            
            i++;
        }
    }

    public void SetCircle()
    {
        Circle circle = gameObject.AddComponent<Circle>();
        
        circle.Init(_easyView_OrbitCenter.position.x, _easyView_OrbitCenter.position.z,
            _easyView_OrbitRadius_Current, 1 , this);
    }

    public void SetUI()
    {
        GameObject nameBillboard = Instantiate(GlobalManager.instance.planetNameBillboard);
        nameBillboard.GetComponent<PlanetUI>().target = this;
    }

    public void SetCurrentProgress()
    {
        TimeSpan ts = GlobalManager.instance.GetTimeSpanSinceProgressStart();
        float days = (float) ts.TotalSeconds / 60.0f/ 60.0f /24.0f;
        currentProgress = currentProgressStart + days / _easyView_OrbitTime;
//        Debug.Log("Name: " + planetName + "; Days from Origin: " + days + "; Progress: " + currentProgress);
    }

    public void SetAxisTilt()
    {
        transform.Rotate(-nativeAxisTilt, 0, 0, Space.World);
    }

    public void UpdateRotation()
    {
        transform.Rotate(0, currentAxisRotationProgress * 360, 0, Space.Self);
    }

    void OnMouseDown()
    {
        Camera.main.GetComponent<TiltNavigation>().SelectPlanet(transform);
    }
}
