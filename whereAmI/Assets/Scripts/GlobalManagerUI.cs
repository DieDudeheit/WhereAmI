using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalManagerUI : MonoBehaviour
{

    //Animation speed
    public Slider slider_AnimSpeed;
    public TextMeshProUGUI text_AnimSpeed;
    
    //Update time
    public Slider slider_UpdateTime;
    public TextMeshProUGUI text_UpdateTime;
    
    //Planet scale
    public Slider slider_PlanetScale;
    public TextMeshProUGUI text_PlanetScale;
    
    //Normalize distance
    public Slider slider_NormDist;
    public TextMeshProUGUI text_NormDist;
    
    //Normalize scale
    public Slider slider_NormScale;
    public TextMeshProUGUI text_NormScale;

    //Start Stop Animation
    public Button button_Pause;
    public Button button_Play;
    private bool isPlaying = true;
    private float currentAnimSpeed;
    
    public void Start()
    {
        slider_AnimSpeed.wholeNumbers = true;
        slider_AnimSpeed.minValue = 1;
        slider_AnimSpeed.maxValue = Mathf.Pow((float) (60 * 60 * 24 * 365.25 * 65), 1.0f/3.0f);
        slider_AnimSpeed.value = 1;

        slider_UpdateTime.wholeNumbers = true;
        slider_UpdateTime.minValue = 0;
        slider_UpdateTime.maxValue = 8;
        slider_UpdateTime.value = 1;
        
        slider_PlanetScale.wholeNumbers = false;
        slider_PlanetScale.minValue = 1;
        slider_PlanetScale.maxValue = 5;
        slider_PlanetScale.value = 1;
        
        slider_NormDist.wholeNumbers = false;
        slider_NormDist.minValue = 0;
        slider_NormDist.maxValue = 1;
        slider_NormDist.value = 0;
        
        slider_NormScale.wholeNumbers = false;
        slider_NormScale.minValue = 0;
        slider_NormScale.maxValue = 1;
        slider_NormScale.value = 0;
        
        Update_AnimSpeed();
        SetUpdateTime();
        SetPlanetScales();
        SetNormDist();
        SetNormScale();
    }

    //On value change Methods
    public void Update_AnimSpeed()
    {
        int value = (int) slider_AnimSpeed.value;
        currentAnimSpeed = value;

        value = (int) Mathf.Pow(value, 3);
        GlobalManager.instance._global_TimeSpeed = value;

        if (value > 0)
        {
            PlayPause(true);
        }
        
//        Debug.Log("Global time speed: " + value);
        UpdateAnimSpeedText(value);
    }

    void UpdateAnimSpeedText(float value)
    {
        if (value <= 0)
            text_AnimSpeed.text = "FREEEEZE";
        else if (value == 1)
            text_AnimSpeed.text = "realtime";
        else if(value < 60)
            text_AnimSpeed.text = (int) (value) + " seconds per second";
        
        else if(value < 60 * 60)
            text_AnimSpeed.text = (int) (value / 60) + " minutes per second";
        
        else if(value < 60 * 60 * 24)
            text_AnimSpeed.text = (int) (value / (60 * 60)) + " hours per second";
        
        else if(value < 60 * 60 * 24 * 365.25/12)
            text_AnimSpeed.text = (int) (value / (60 * 60 * 24)) + " days per second";
        
        else if (value < 60 * 60 * 24 * 365.25)
            text_AnimSpeed.text = (int) (value / (60 * 60 * 24 * (365.25/12))) + " Months per second";
        
        else if (value < 60 * 60 * 24 * 365.25 * 5)
            text_AnimSpeed.text = (int) (value / (60 * 60 * 24 * 365.25)) + " Years per second";

        else if (value < 60 * 60 * 24 * 365.25 * 10)
            text_AnimSpeed.text = "You want more?";
        
        else if (value < 60 * 60 * 24 * 365.25 * 50)
            text_AnimSpeed.text = (int) (value / (60 * 60 * 24 * 365.25)) + " Years per second!!!";
        
        else if (value >= 60 * 60 * 24 * 365.25 * 50)
            text_AnimSpeed.text = "Dude, stop it already ...";
    }
    
    public void SetUpdateTime()
    {
        float value = slider_UpdateTime.value;
        GlobalManager.instance._global_UpdateTime = value/8;
        
        if(value <= 0)
            text_UpdateTime.text = "instant";
        else if(value > 0)
            text_UpdateTime.text = value/8.0f + " seconds";
    }
    
    public void SetPlanetScales()
    {
        float value = slider_PlanetScale.value;
        GlobalManager.instance._global_PlanetScale = value;
        
        if(value <= 1)
            text_PlanetScale.text = "Real life scale";
        else if(value > 0)
            text_PlanetScale.text = value + "x";
    }
    
    public void SetNormDist()
    {
        float value = slider_NormDist.value;
        GlobalManager.instance._global_NormalizeDistance = value;
        
        if(value <= 0)
            text_NormDist.text = "Real life distances";
        else if(value > 0)
            text_NormDist.text = (int) (value*100) + " % equalized";
    }
    
    public void SetNormScale()
    {
        float value = slider_NormScale.value;
        GlobalManager.instance._global_NormalizeScale = value;
        
        if(value <= 0)
            text_NormScale.text = "Real life scale";
        else if(value > 0)
            text_NormScale.text = (int) (value*100) + " % equalized";
    }

    public void ResetView()
    {
        slider_AnimSpeed.value = slider_AnimSpeed.minValue;
        slider_UpdateTime.value = slider_UpdateTime.minValue;
        slider_PlanetScale.value = slider_PlanetScale.minValue;
        slider_NormDist.value = slider_NormDist.minValue;
        slider_NormScale.value = slider_NormScale.minValue;

        GameObject[] astroObjects = GameObject.FindGameObjectsWithTag("AstronomicalObject");
        foreach (var go in astroObjects)
        {
            if (go.name == "Sun")
            {
                TiltNavigation tiltNav = Camera.main.GetComponent<TiltNavigation>();
                tiltNav.SelectPlanet(go.transform);
            }
        }
    }

    public void PlayPause(bool start)
    {
        isPlaying = start;
        button_Pause.gameObject.SetActive(isPlaying);
        button_Play.gameObject.SetActive(!isPlaying);

        float tempAnimSpeed = slider_AnimSpeed.value;

        if (!isPlaying)
        {
            slider_AnimSpeed.minValue = 0;
            slider_AnimSpeed.value = 0;    
        }
        else
        {
            slider_AnimSpeed.value = currentAnimSpeed;
            slider_AnimSpeed.minValue = 1;
        }

        currentAnimSpeed = tempAnimSpeed;
    }
    
}
