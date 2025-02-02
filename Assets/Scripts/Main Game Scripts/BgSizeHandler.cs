using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSizeHandler : MonoBehaviour
{

    private void Start()
    {
        ResizeSpriteToScreen();
    }
    private void ResizeSpriteToScreen()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;
        transform.localScale = new Vector3(1, 1, 1);
        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        float my_x = (float)worldScreenWidth / width;
        float my_y = (float)worldScreenHeight / height;

        transform.localScale = new Vector3(my_x, my_y, 0);  

    }
}
