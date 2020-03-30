using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : AstronomicalObject
{
    public void Start()
    {
        Init(0);
    }

    public override void Init(int index)
    {
        this.systemIndex = index;
        Camera.main.GetComponent<TiltNavigation>().SelectPlanet(_astronomicalObjects[index].transform);
        InitChilds();
    }
}
