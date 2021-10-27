using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    public new Image renderer;

    public Crosshair[] Crosshairs;
    
    [HideInInspector]
    public int activeNum;

    private RectTransform rect;
    // Update is called once per frame

    private void Start()
    { 
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        Cursor.visible = false;

        Vector3 mousePos = Input.mousePosition;
        rect.position = Vector2.Lerp(rect.position, mousePos, Crosshairs[activeNum].moveSpeed);

        renderer.sprite = Crosshairs[activeNum].Image;
    }
    
    public void ChangeCrosshair(string name)
    {
        for (int i = 0; i < Crosshairs.Length; i++)
        {
            if (Crosshairs[i].Name == name)
            {
                activeNum = i;
                //Debug.Log("Changed");
            }
        }
    }
}
