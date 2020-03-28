using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.PlayerLoop;

public class TiltNavigation : MonoBehaviour
{
    public Transform lookAt;
    public float sensitivity;
    private Transform selectedPlanet;

    private Vector3 currentPosition;
    private Vector3 deltaPosition;
    private Vector3 lastPosition;

    private float currentDistance = -10;
    private float normalizedValueOld;
    public float minDistance = -0.75f;
    public float maxDistance = -12000f;

    private void Start()
    {
        normalizedValueOld = GlobalManager.instance._global_NormalizeDistance;
    }

    private void LateUpdate()
    {
        float globalNorDistVal = GlobalManager.instance._global_NormalizeDistance;
        //Set position
        if (selectedPlanet != null)
        {
            var transform1 = selectedPlanet.transform;
            lookAt.transform.position = transform1.position;
            lookAt.transform.localScale = selectedPlanet.localScale;
        }

        //Set rotation
        currentPosition = Input.mousePosition;
        deltaPosition = currentPosition-lastPosition;
        lastPosition = currentPosition;
        if (Input.GetMouseButton(0))
        {
            lookAt.transform.Rotate(0, deltaPosition.x, 0, Space.World);
            lookAt.transform.Rotate(-deltaPosition.y, 0, 0, Space.Self);
        }
        
        
        //Set distance
        var localPosition = transform.localPosition;
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        float norDistPow = Mathf.Pow(globalNorDistVal, 0.25f);


        var localScale = lookAt.transform.localScale;
        localScale = localScale - localScale * norDistPow
                     +
                     Vector3.one * (norDistPow * 0.01f);
        
        lookAt.transform.localScale = localScale;
        
        //scroll
        currentDistance +=  -scroll * currentDistance/10;
        
        //Global Normalized dist dependency
        if (normalizedValueOld < globalNorDistVal)
            currentDistance -= currentDistance * globalNorDistVal * 0.25f;
        else if (normalizedValueOld > globalNorDistVal)
            currentDistance += currentDistance * globalNorDistVal * 0.25f;
        normalizedValueOld = globalNorDistVal;
        
        if (currentDistance > minDistance)
        {
            currentDistance = minDistance;
        } 
        else if (currentDistance < maxDistance)
        {
            currentDistance = maxDistance;
        }

        SetDistance(currentDistance);
//        CheckTranslation();
    }

    public void SelectPlanet(Transform target)
    {
        if(selectedPlanet != null)
            selectedPlanet.GetComponent<LineRenderer>().enabled = true;
        
        selectedPlanet = target;
        currentDistance = minDistance;

        selectedPlanet.GetComponent<LineRenderer>().enabled = false;
    }

    public float GetDistance()
    {
        return (transform.position - lookAt.position).magnitude;
    }

    public void SetDistance(float dist)
    {
        var localPosition = transform.localPosition;
        localPosition = new Vector3(localPosition.x, localPosition.y, currentDistance);
        transform.localPosition = localPosition / lookAt.transform.localScale.x;
    }

    private void CheckTranslation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("hey");
            lookAt.transform.position += Quaternion.AngleAxis(lookAt.transform.rotation.y, Vector3.up) * new Vector3(deltaPosition.x, 0, deltaPosition.y);
        }
    }
    

}
