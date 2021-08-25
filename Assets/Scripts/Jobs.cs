using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jobs", menuName = "ScriptableObjects/Jobs", order = 2)]
public class Jobs : ScriptableObject
{
    public Job[] Definitions;

    [System.Serializable]
    public class Job
    {
        public ItemQuantity[] ItemsRequired;
        public ItemQuantity[] Rewards;
        public string StoryEventTriggered;
    }
}

