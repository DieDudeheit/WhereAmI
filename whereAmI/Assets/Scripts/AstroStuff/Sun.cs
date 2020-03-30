using UnityEngine;

public class Sun : AstronomicalObject
{
    private void Update()
    {
        //Rotation progress
        float tempAxisRotationProgress = Time.deltaTime * GlobalManager.instance._global_TimeSpeed / (axisRotationTime * 24 * 60 * 60);
        currentAxisRotationProgress = tempAxisRotationProgress;
        
        base.SetTranform();
        base.UpdateRotation();
    }

}
