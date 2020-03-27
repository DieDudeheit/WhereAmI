using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class SolarSystem : AstronomicalObject
{
    public void Start()
    {
        Init(0);
    }

    public override void Init(int index)
    {
        this.systemIndex = index;
        InitChilds();
    }
}
