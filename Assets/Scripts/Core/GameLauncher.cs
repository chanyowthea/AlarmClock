using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

public class GameLauncher : MonoSingleton<GameLauncher>
{
    private void Start()
    {
        UIManager.Instance.Open<StartUI>(); 
    }

    private void Update()
    {
        TimeManager.instance.Update(); 
    }
}
