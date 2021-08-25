using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerConstants", menuName = "ScriptableObjects/PlayerConstants", order = 1)]
public class PlayerConstants : ScriptableObject
{
    public PlayerLevel[] Levels;

    public ItemQuantity[] StartingItems;
}
