using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigManager
{
    private static ConfigManager _instance;
    public static ConfigManager Instance => _instance ??= new ConfigManager();

    private ConfigManager()
    {
        //Debug.Log($"{} Configs Loaded Successfully!");
    }

    public void GetConfig(string configName)
    {
    }
}
