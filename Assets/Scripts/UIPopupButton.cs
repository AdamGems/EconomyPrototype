using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupButton : MonoBehaviour
{
    //Summons a popup

    public string RequiredFeature;
    public UIPopup PopupToOpen;

    public void OnTap()
    {
        PopupToOpen.Open();
    }
}
