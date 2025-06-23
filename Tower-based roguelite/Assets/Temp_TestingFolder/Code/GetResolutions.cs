using UnityEngine;
using System.Collections;

public class GetResolutions : MonoBehaviour
{
    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            print(res.width + "x" + res.height);
        }
        Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
    }
}