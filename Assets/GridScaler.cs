using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScaler : MonoBehaviour
{
    public float temp; 
    Vector3 tempScale;
    private void Update()
    {
        ResizeSpriteToScreen();
        temp = Screen.width / 3;
    }

    public void ResizeSpriteToScreen() {
        var sr = GetComponent<SpriteRenderer>();

        if (sr == null) return;
     
        
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width/ Screen.height; // basically height * screen aspect ratio

       
        tempScale.x = (width / 1.85f);
        tempScale.y = height;
        gameObject.transform.localScale = tempScale;

        // transform.localScale = new Vector3(1,1,1);
        /*Camera.main.orthographicSize = (100f / Screen.width * Screen.height / 2.0f);*/

        /*/*var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;
     
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight * Camera.main.aspect;#1#
        float screenAspect = (float) Screen.width / (float) Screen.height;
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

      

        

        Vector3 vec = transform.localScale;
        vec.x = (float)camWidth /3;
        //vec.y = (float)worldScreenHeight ;
        transform.localScale = vec;*/

    }
}
