// <copyright file="PlayMovieTextureOnUI.cs" company="dyadica.co.uk">
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
// <summary>
// A simple class that activates and de-activates the playback of a web camera via the
// use of the Unity 3D WebCamTexture class.
// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayMovieTextureOnUI : MonoBehaviour
{
    #region Properties

    // The webcam texture

    private WebCamTexture webcamTexture;

    // The raw image which we are going to apply 
    // the camera stream to for display.

    public RawImage rawimage;

    // Dropdown widget for both devices and display
    // orientation.

    public Dropdown deviceDropDown;
    public Dropdown orientationDropDown;

    // The status of connection

    public bool IsConnected = false;

    // The Text display of the connection button.
    // This is made public so that we can update 
    // it to reflect status etc.    

    public Text cButton;

    // Enum of the potential display orienbtations

    public enum Orientations
    {
        Normal, FlipX, FlipY, FlipXY
    }

    // Instance of the Orientation Enum

    public Orientations Orientation;

    // These are example extra options which can also be
    // added to the menu if wanted. For now we can just
    // use the editor.

    // The desired FPS

    public int RequestedFPS = 60;

    // The desired Width and Height

    public int RequestedWidth = 640;
    public int RequestedHeight = 480;

    #endregion Properties

    #region Unity Loop

    void Start()
    {
        rawimage.enabled = false;

        PopulateDevices();

        PopulateOrientations();
    }

    #endregion Unity Loop

    #region Methods

    /// <summary>
    /// Method used to perform the connection and disconnection of
    /// the webcam. This method also updates the active status of
    /// the RawImage and the referenced connection status IsConnected.
    /// </summary>
    public void ConnectCamera()
    {
        if (IsConnected == false)
        {
            // Enable the raw image and connect camera

            rawimage.enabled = true;

            // Update the status to connected

            IsConnected = true;

            // Initalise the texture

            webcamTexture =
                new WebCamTexture(deviceDropDown.options[deviceDropDown.value].text);

            // Populate the example options

            webcamTexture.requestedFPS = 
                RequestedFPS;

            webcamTexture.requestedWidth = 
                RequestedWidth;

            webcamTexture.requestedHeight = 
                RequestedHeight;

            // webcamTexture.mipMapBias = 0f;

            // We also have some for the rawImage

            rawimage.texture = webcamTexture;

            rawimage.material.mainTexture = 
                webcamTexture;

            rawimage.material.mainTexture.anisoLevel = 1;

            rawimage.material.mainTexture.filterMode = 
                FilterMode.Bilinear;

            // Initialise the camera

            webcamTexture.Play();

            // Update the connection button to 
            // reflect the current state

            cButton.text = "Disconnect";
        }
        else
        {
            // Disable the raw image and connect camera

            rawimage.enabled = false;

            // Update the status to disconnected

            IsConnected = false;

            // Stop the camera

            webcamTexture.Stop();

            // Set the texture to null

            webcamTexture = null;
            rawimage.texture = null;

            // Update the connection button to 
            // reflect the current state

            cButton.text = "Connect";
        }
    }

    /// <summary>
    /// Method used to update the orientation of the display image
    /// </summary>
    public void SwapOrientation()
    {
        // Check to see if the widget has been 
        // set via the editor.

        if (orientationDropDown == null)
            return;

        // Get the current value from the dropDown list

        Orientations current = (Orientations)System.Enum.Parse(typeof(Orientations),
            orientationDropDown.options[orientationDropDown.value].text);

        // Update the rawimages uvRect to reflect the
        // orientation.

        switch (current)
        {
            case Orientations.Normal:
                rawimage.uvRect = new Rect(0, 0, 1, 1);
                break;
            case Orientations.FlipX:
                rawimage.uvRect = new Rect(1, 0, -1, 1);
                break;
            case Orientations.FlipY:
                rawimage.uvRect = new Rect(0, 1, 1, -1);
                break;
            case Orientations.FlipXY:
                rawimage.uvRect = new Rect(1, 1, -1, -1);
                break;
        }
    }

    /// <summary>
    /// Method used to populate the devices dropDown widget
    /// </summary>
    public void PopulateDevices()
    {
        // Check to see if the widget has been 
        // set via the editor

        if (deviceDropDown == false)
            return;

        // Initialise a list for the devices.

        WebCamDevice[] devices = WebCamTexture.devices;

        // Clear any existing options.

        deviceDropDown.options.Clear();

        // Loop through the device list and add the name
        // of the current device to the list.

        for (int i = 0; i < devices.Length; i++)
        {
            deviceDropDown.options.Add(new Dropdown.OptionData() { text = devices[i].name });
        }

        // Perform an update to refresh the current selected
        // value to that of first [0] in the list.

        deviceDropDown.value = 1;
        deviceDropDown.value = 0;
    }

    /// <summary>
    /// Method used to populate the avaliable orientations dropDown widget
    /// </summary>
    public void PopulateOrientations()
    {
        // Check to see if the widget has been 
        // set via the editor

        if (orientationDropDown == null)
            return;

        // Clear any existing options.

        orientationDropDown.options.Clear();

        // Loop through the orientations enum and add the name
        // of the current device to the list.

        foreach (Orientations orientation in System.Enum.GetValues(typeof(Orientations)))
        {
            orientationDropDown.options.Add(new Dropdown.OptionData() { text = orientation.ToString() });
        }

        // Perform an update to refresh the current selected
        // value to that of first [0] in the list.

        orientationDropDown.value = 1;
        orientationDropDown.value = 0;
    }    

    #endregion Methods
}
