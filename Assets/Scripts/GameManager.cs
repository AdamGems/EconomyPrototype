using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ItemManager itemManager;
    public PlayerState Player;
    public PlayerConstants PlayerConstants;
    

    public void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            
            itemManager.Initialize();
            Player.Initialize();

            DontDestroyOnLoad(gameObject);
        }
    }
}
