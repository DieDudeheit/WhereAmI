using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Planet : AstronomicalObject
{
    
    public override void Init(int index)
    {
        base.Init(index);
        //Start Rotation
        if(gameObject.activeSelf)
            StartCoroutine(Orbit_easyView());
    }

    private void Update()
    {
        float tempProgress = Time.deltaTime * GlobalManager.instance._global_TimeSpeed / (_easyView_OrbitTime * 24 * 60 * 60);
        currentProgress += tempProgress;
        
        base.SetTranform();
        
    }

    IEnumerator Orbit_easyView()
    {
        while (true)
        {
            Vector2 pos2D = MathLib.GetCurrentPos_Circle(_easyView_OrbitCenter.position.x, _easyView_OrbitCenter.position.y,
                _easyView_OrbitRadius_Current, currentProgress * Mathf.PI * 2);
            transform.position = new Vector3(pos2D.x, 0, pos2D.y);
            yield return new WaitForSeconds(GlobalManager.instance._global_TimeStepSize);
        }
        
    }
}
