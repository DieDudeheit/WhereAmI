using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TiltNavigation : MonoBehaviour
{
    public Transform lookAt;
    public float sensitivity;
    public Transform selectedPlanet;

    private void Update()
    {
        //Set position
        if (selectedPlanet != null)
        {
            lookAt.transform.position = selectedPlanet.transform.position;
        }
        
        
    }
}
