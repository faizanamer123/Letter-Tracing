using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    public string myPanelName;
    public bool pauseGame;

    public void LateUpdate()
    {
        HandlePanelVisibility();
        HandleGamePause();
    }

    private void HandlePanelVisibility()
    {
        if(myPanelName == MainMenuHandler.Instance.PanelName)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            if(MainMenuHandler.Instance.PanelName != "loading")
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void HandleGamePause()
    {
        if (pauseGame)
        {
            if (this.transform.GetChild(0).gameObject.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
