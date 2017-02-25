// <copyright file="MenuActivate.cs" company="dyadica.co.uk">
// Copyright (c) 2010, 2017 All Right Reserved, http://www.dyadica.co.uk

// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// </copyright>

// <author>Steven Battersby</author>
// <contact>https://www.facebook.com/ADropInTheDigitalOcean</contact>
// <date>25.02.2017</date>
// <summary>A simple class that activates and de-activates the WebCamPreview app menu </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuActivate : MonoBehaviour
{
    // The Menu gameObject to activate and
    // De-Activate on RollOver

    public GameObject Menu;

	void Start ()
    {
        // Set the menu object to in-active

        if (Menu != null)
            Menu.SetActive(false);
    }

    void Update()
    {
        // Get the current mouse positions

        float mx = Input.mousePosition.x;
        float my = Input.mousePosition.y;

        // Set the initial state to false

        bool ShowMenu = false;

        // Check to see if the mouse is within 
        // the screen window

        if(mx > 0 && mx < Screen.width)
        {
            if(my > 0 && my < Screen.height)
            {
                // The mouse is within the window
                // so update the visibility to true

                ShowMenu = true;
            }
        }

        // Update the active state of the menu to
        // reflect that set by mouse location

        if (Menu != null)
            Menu.SetActive(ShowMenu);
    }
}
