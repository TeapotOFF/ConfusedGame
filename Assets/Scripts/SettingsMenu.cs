using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void CloseSetting()
    {
        gameObject.SetActive(false);
    }
    public void SetQuality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex+1);
        if (qualityindex == 3)
        {
            QualitySettings.SetQualityLevel(7);
        }
    }
    public void ChangeResolution(int resolutionindex)
    {
        switch (resolutionindex)
        {
            case 0:
                Screen.SetResolution(1024, 768, true);
                break;
            case 1:
                Screen.SetResolution(1366, 768, true);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }
}
