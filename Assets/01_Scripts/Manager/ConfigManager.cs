using System;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance {get; private set;}
    
    [SerializeField] private GameConfig config;
    public static GameConfig Config => Instance.config;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
