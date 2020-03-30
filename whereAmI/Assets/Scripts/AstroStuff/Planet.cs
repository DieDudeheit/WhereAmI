using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : AstronomicalObject
{
    
    public override void Init(int index)
    {
        base.Init(index);
        //Start Rotation
        if(gameObject.activeSelf)
            StartCoroutine(Orbit());
    }

    private void Update()
    {
        //Orbit progress
        float tempOrbitRotationProgress = Time.deltaTime / (_easyView_OrbitTime * 24 * 60 * 60) * GlobalManager.instance._global_TimeSpeed;
        currentProgress += tempOrbitRotationProgress;
        
        //Rotation progress
        float tempAxisRotationProgress = Time.deltaTime/ (axisRotationTime * 24 * 60 * 60) * GlobalManager.instance._global_TimeSpeed;
        currentAxisRotationProgress = tempAxisRotationProgress;
        
        base.SetTranform();
        base.UpdateRotation();
    }

    IEnumerator Orbit()
    {
        while (true)
        {
            Vector3 pos3D = MathLib.GetCurrentPos_Circle(_easyView_OrbitCenter.position.x, _easyView_OrbitCenter.position.y,
                _easyView_OrbitRadius_Current, currentProgress * Mathf.PI * 2, _correctView_OrbitTilt);
            transform.position = pos3D;
            yield return new WaitForSeconds(GlobalManager.instance._global_UpdateTime);
        }
        
    }
}
