/****************************************************************************
* Copyright 2019 Nreal Techonology Limited. All rights reserved.
*                                                                                                                                                          
* This file is part of NRSDK.                                                                                                          
*                                                                                                                                                           
* https://www.nreal.ai/        
* 
*****************************************************************************/

using System.Collections.Generic;
using NRKernal;
using UnityEngine;
using TMPro;

    /// <summary> Uses 4 frame corner objects to visualize an TrackingImage. </summary>
    public class MyTrackingImgVis : MonoBehaviour
    {
        /// <summary> The TrackingImage to visualize. </summary>
        public NRTrackableImage Image;

        // /// <summary>
        // /// A model for the lower left corner of the frame to place when an image is detected. </summary>
        public GameObject myObject;

    public TMP_Text text;

    // /// <summary>
    // /// A model for the lower right corner of the frame to place when an image is detected. </summary>
    // public GameObject FrameLowerRight;

    // /// <summary>
    // /// A model for the upper left corner of the frame to place when an image is detected. </summary>
    // public GameObject FrameUpperLeft;

    // /// <summary>
    // /// A model for the upper right corner of the frame to place when an image is detected. </summary>
    // public GameObject FrameUpperRight;

    // /// <summary> The axis. </summary>
    // public GameObject Axis;
    InfoObj myinfo = null;

    /// <summary> Updates this object. </summary>
    ///
    public void Start()
    {
        fetchData();

        //= new List<string>()
        //        {
        //            "Ahmedabad, in western India, is the largest city in the state of Gujarat. The Sabarmati River runs through its center.",
        //            "Leicester City Football Club is a professional football club based in Leicester in the East Midlands, England.",                        
        //            "Dhaka is the capital city of Bangladesh, in southern Asia. Set beside the Buriganga River, it’s at the center of national government, trade and culture.",
        //            "TulipTech, From setting up an IT infrastructure to leveraging cloud-based solutions to providing custom-built business applications, we deliver the best in class tech solutions to our clients.",
        //            "Wtv is one of the longest established virtual and hybrid events technology agencies in Europe and Asia, headquartered in Geneva, Switzerland with offices in London, Frankfurt, Madrid, Hong Kong and Zurich."
        //        };
        //myObject.renderer.material.color =
        //new Color(1.0f, 1.0f.1.0f, 1.0f);
        Debug.Log(myinfo);
        text.text = "started";
    }

    private  void fetchData()
    {
        myinfo = DataSaver.loadData<InfoObj>(MyConst.CACHE_NAME);
    }

    public void Update()
        {
            if (Image == null || Image.GetTrackingState() != TrackingState.Tracking)
            {
                myObject.SetActive(false);
                // FrameLowerRight.SetActive(false);
                // FrameUpperLeft.SetActive(false);
                // FrameUpperRight.SetActive(false);
                // Axis.SetActive(false);
                return;
            }

            // float halfWidth = Image.ExtentX / 2;
            // float halfHeight = Image.ExtentZ / 2;
            // FrameLowerLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.back);
            // FrameLowerRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.back);
            // FrameUpperLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.forward);
            // FrameUpperRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.forward);

            var center = Image.GetCenterPose();
            transform.position = center.position;
            transform.rotation = center.rotation;
        
        text.text = $"Img identified DB index: {Image.GetDataBaseIndex()}";
        text.text = myinfo.data[Image.GetDataBaseIndex()];



        myObject.SetActive(true);
            // FrameLowerRight.SetActive(true);
            // FrameUpperLeft.SetActive(true);
            // FrameUpperRight.SetActive(true);
            // Axis.SetActive(true);
        }

    }

