using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PlanetUI : MonoBehaviour
{
    public float panelHeight;
    public TextMeshProUGUI textPanel; 
    public RectTransform canvas;

    public AstronomicalObject target;
    private TiltNavigation tn;
    
    private void Start()
    {
        string name = target.planetName;
        SetText(name);
        tn = Camera.main.GetComponent<TiltNavigation>();
    }

    private void LateUpdate()
    {
        transform.position = target.transform.position;
        BillboardCanvasToMainCamera();
        float camDist = tn.GetDistance();
        //Set scale
        float scale = Mathf.Abs(camDist * GlobalManager.instance._global_ObjScale/ 10);
        SetScale(scale);
        float height = Mathf.Pow(Mathf.Abs(camDist), 0.1f) * GlobalManager.instance.billboardScale * GlobalManager.instance._global_ObjScale;
        height = height -  0.5f * height * GlobalManager.instance._global_NormalizeDistance;
        SetHeight(height);
    }

    public void SetText(string text)
    {
        textPanel.text = text;
    }

    public void SetHeight(float height)
    {
        canvas.anchoredPosition = new Vector2(0, height);
    }

    public void SetScale(float scale)
    {
        canvas.transform.localScale = Vector3.one * (scale/100 * GlobalManager.instance.billboardScale);
        
    }

    void BillboardCanvasToMainCamera()
    {
        transform.rotation = Camera.main.transform.parent.transform.rotation;
    }
    
    public void SetCameraLookAt()
    {
        Camera.main.GetComponent<TiltNavigation>().SelectPlanet(target.transform);
    }

}
