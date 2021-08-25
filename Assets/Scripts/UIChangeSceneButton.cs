using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIChangeSceneButton : MonoBehaviour
{
    public string DestinationScene;
    public Feature.ID FeatureRequired = Feature.ID.None;
    public void OnTap()
    {
        SceneManager.LoadScene(DestinationScene);
    }
}
