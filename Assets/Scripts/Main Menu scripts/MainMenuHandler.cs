using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuHandler : MonoBehaviour
{
    public static MainMenuHandler Instance;
    public Sprite[] shapeSprites, lineSprites;
    public Transform itemsCount;
    public GameObject ItemPreafab_TextBased, ItemPreafab_SpriteBased;
    [HideInInspector] public string PanelName;

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        
    }

    public void ShowTracingItems(string category)
    {
        foreach (Transform child in itemsCount)
        {
            Destroy(child.gameObject);
        }
        switch (category)
        {
            case "alphabet":
                for(int i = 0; i < 26; i++)
                {
                    int no = i + 65;
                    SetItemText(i, no);
                }
                break;

            case "number":
                for (int i = 0; i <= 9; i++)
                {
                    int no = i + 48;
                    SetItemText(i, no);
                }
                break;

            case "shape":
                for (int i = 0; i < shapeSprites.Length; i++)
                {
                    SetItemImage(i, shapeSprites);
                }
                break;

            case "line":
                for (int i = 0; i < lineSprites.Length; i++)
                {
                    SetItemImage(i, lineSprites);
                }
                break;
        }
    }

    private void SetItemText(int i , int num)
    {
        GameObject _item = Instantiate(ItemPreafab_TextBased, itemsCount);
        _item.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToChar(num).ToString();
    }

    private void SetItemImage(int i , Sprite[] spritesItem)
    {
        GameObject _item = Instantiate(ItemPreafab_SpriteBased, itemsCount);
        _item.transform.GetChild(0).GetComponent<Image>().sprite = spritesItem[i];
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
