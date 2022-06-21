
/****************************************************************************
* Copyright 2019 Nreal Techonology Limited. All rights reserved.
*                                                                                                                                                          
* This file is part of NRSDK.                                                                                                          
*                                                                                                                                                           
* https://www.nreal.ai/        
* 
*****************************************************************************/

namespace NRKernal.NRExamples
{
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    /// <summary> Controller for TrackingImage example. </summary>
    [HelpURL("https://developer.nreal.ai/develop/unity/image-tracking")]
    public class MyTrkImgExampleController : MonoBehaviour
    {
        /// <summary> A prefab for visualizing an TrackingImage. </summary>
        public MyTrackingImgVis TrackingImageVisualizerPrefab;

        /// <summary> for not. </summary>
        public GameObject notificationGO;

        /// <summary> Text for not. </summary>
        public TMP_Text notText;

        /// <summary> The overlay containing the fit to scan user guide. </summary>
        public GameObject FitToScanOverlay;

        /// <summary> The visualizers. </summary>
        private Dictionary<int, MyTrackingImgVis> m_Visualizers
            = new Dictionary<int, MyTrackingImgVis>();

        /// <summary> The temporary tracking images. </summary>
        private List<NRTrackableImage> m_TempTrackingImages = new List<NRTrackableImage>();

        private void Start()
        {
            fetchData();
        }

        private async void fetchData()
        {
            notificationHandler(true, "Fetching data from server ...");
            await APIHelper.getInfoWithUnityWebReq();
            notificationHandler(false, "Fetching data from server");
        }

        private void notificationHandler(bool toogle, string text)
        {
            notText.text = text;
            notificationGO.SetActive(toogle);
        }

        /// <summary> Updates this object. </summary>
        public void Update()
        {
            trackImage();
        }

        private void trackImage()
        {
#if !UNITY_EDITOR
            // Check that motion tracking is tracking.
            if (NRFrame.SessionStatus != SessionState.Running)
            {
                return;
            }
#endif
            // Get updated augmented images for this frame.
            NRFrame.GetTrackables<NRTrackableImage>(m_TempTrackingImages, NRTrackableQueryFilter.New);

            // Create visualizers and anchors for updated augmented images that are tracking and do not previously
            // have a visualizer. Remove visualizers for stopped images.
            foreach (var image in m_TempTrackingImages)
            {
                MyTrackingImgVis visualizer = null;
                m_Visualizers.TryGetValue(image.GetDataBaseIndex(), out visualizer);
                if (image.GetTrackingState() != TrackingState.Stopped && visualizer == null)
                {
                    NRDebugger.Info("Create new TrackingImageVisualizer!");
                    // Create an anchor to ensure that NRSDK keeps tracking this augmented image.
                    visualizer = (MyTrackingImgVis)Instantiate(TrackingImageVisualizerPrefab, image.GetCenterPose().position, image.GetCenterPose().rotation);
                    visualizer.Image = image;
                    visualizer.transform.parent = transform;

                    //visualizer.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

                    m_Visualizers.Add(image.GetDataBaseIndex(), visualizer);
                }
                else if (image.GetTrackingState() == TrackingState.Stopped && visualizer != null)
                {
                    //m_Visualizers.Remove(image.GetDataBaseIndex());
                    //Destroy(visualizer.gameObject);
                }

                FitToScanOverlay.SetActive(false);
            }
        }

        /// <summary> Enables the image tracking. </summary>
        public void EnableImageTracking()
        {
            //var config = NRSessionManager.Instance.NRSessionBehaviour.SessionConfig;
            //config.ImageTrackingMode = TrackableImageFindingMode.ENABLE;
            //NRSessionManager.Instance.SetConfiguration(config);
            //FitToScanOverlay.SetActive(true);
            trackImage();

        }

        /// <summary> Disables the image tracking. </summary>
        public void DisableImageTracking()
        {
            var config = NRSessionManager.Instance.NRSessionBehaviour.SessionConfig;
            config.ImageTrackingMode = TrackableImageFindingMode.DISABLE;
            NRSessionManager.Instance.SetConfiguration(config);
        }

    }
}
