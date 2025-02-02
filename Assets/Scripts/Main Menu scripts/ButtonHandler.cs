using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void ActivatePlane(string _panelName)
    {
        MainMenuHandler.Instance.PanelName = _panelName;
    }
}
