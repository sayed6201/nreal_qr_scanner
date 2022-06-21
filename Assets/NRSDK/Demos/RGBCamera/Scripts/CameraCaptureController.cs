/****************************************************************************
* Copyright 2019 Nreal Techonology Limited. All rights reserved.
*                                                                                                                                                          
* This file is part of NRSDK.                                                                                                          
*                                                                                                                                                           
* https://www.nreal.ai/        
* 
*****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using ZXing;

namespace NRKernal.NRExamples
{
    /// <summary> A controller for handling camera captures. </summary>
    [HelpURL("https://developer.nreal.ai/develop/unity/rgb-camera")]
    public class CameraCaptureController : MonoBehaviour
    {
        /// <summary> The capture image. </summary>
        public RawImage CaptureImage;
        /// <summary> Number of frames. </summary>
        public Text FrameCount;
        public Text textOut;                 
        public Text textOutPinch;

        /// <summary> for not. </summary>
        public GameObject infoPrefab;

        Text infoText;

        /// <summary> for not. </summary>
        public GameObject QrCodeScannerObj;

        /// <summary> Gets or sets the RGB camera texture. </summary>
        /// <value> The RGB camera texture. </value>
        private NRRGBCamTexture RGBCamTexture { get; set; }

        private HandState handState = null;
        // private shouldScan = true;

        Dictionary<string, string> myData = null;

        //qrcode
        string myQrCode = null;

        void Start()
        {
            infoPrefab.SetActive(false);
            RGBCamTexture = new NRRGBCamTexture();
            //CaptureImage.texture = RGBCamTexture.GetTexture();
            RGBCamTexture.Play();
            Debug.Log("Start called");
            handState = NRInput.Hands.GetHandState(HandEnum.RightHand);
            myData = new Dictionary<string, string>(){
                    {"https://product101", "product101"},
                    {"https://car-seller", "car"},
                    {"https://shoes.com", "shoes"}
                };
            // Scan();
            
        }

        /// <summary> Updates this object. </summary>
        void Update()
        {
            if (RGBCamTexture == null)
            {
                return;
            }
            //Debug.Log("Update called");
            FrameCount.text = RGBCamTexture.FrameCount.ToString();

            if (NRInput.GetButton(ControllerButton.TRIGGER))
            {
                Scan();
            }

            //if (handState.isPinching){
            //    textOutPinch.text = "1111";
            //    textOut.text = "Scanning ...";
            //}else{
            //    textOutPinch.text = "0000";
            //} 

            }

        /// <summary> Plays this object. </summary>
        public void Play()
        {
            if (RGBCamTexture == null)
            {
                RGBCamTexture = new NRRGBCamTexture();
                //CaptureImage.texture = RGBCamTexture.GetTexture();
            }

            RGBCamTexture.Play();
            // The origin texture will be destroyed after call "Stop",
            // Rebind the texture.
            //CaptureImage.texture = RGBCamTexture.GetTexture();

            // Scan();

            
        }

        /// <summary> Pauses this object. </summary>
        public void Pause()
        {
            RGBCamTexture?.Pause();
        }

        /// <summary> Stops this object. </summary>
        public void Stop()
        {
            RGBCamTexture?.Stop();
            RGBCamTexture = null;
        }

        /// <summary> Executes the 'destroy' action. </summary>
        void OnDestroy()
        {
            RGBCamTexture?.Stop();
            RGBCamTexture = null;
        }

        public void Scan(){
        try{
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(RGBCamTexture.GetTexture().GetPixels32(), RGBCamTexture.GetTexture().width, RGBCamTexture.GetTexture().height);
            if(result != null) {
                textOut.text = result.Text;
                textOut.fontSize = 30;
            }else{
                // textOut.text = "Failed TO READ QR CODE";
                // textOut.fontSize = 30;
            }
        }catch{
            //textOut.text = "Failed to scan :( ";
        }
        }

        public void Refresh(){
            textOut.text = "Scanning ..... ";
             Scan();
        }

        public void OpenLink(){
            Application.OpenURL(textOut.text.ToString());
            Debug.Log("Openinig link");
        }

        public void viewInformation()
        {
            myQrCode = textOut.text;
            
            QrCodeScannerObj.SetActive(false);
            infoPrefab.SetActive(true);
            Debug.Log("Hello");
        }

        public void exitInformation()
        {
            QrCodeScannerObj.SetActive(true);
            infoPrefab.SetActive(false);
        }
    }
}
