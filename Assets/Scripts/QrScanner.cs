// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using ZXing;

// namespace NRKernal.NRExamples
// {
// public class QrScanner : MonoBehaviour
// {
//     /// <summary> The capture image. </summary>
//     public RawImage CaptureImage;
//     /// <summary> Number of frames. </summary>
//     // public Text FrameCount;
//     /// <summary> Gets or sets the RGB camera texture. </summary>
//     /// <value> The RGB camera texture. </value>
//     private NRRGBCamTexture RGBCamTexture { get; set; }
//     // public Texture2D inputTexture; // Note: [x] Read/Write must be enabled from texture import settings

//     // Start is called before the first frame update
//     void Start()
//     {

//         RGBCamTexture = new NRRGBCamTexture();
//         CaptureImage.texture = RGBCamTexture.GetTexture();
//         RGBCamTexture.Play();

//         // create a barcode reader instance
//         IBarcodeReader reader = new BarcodeReader();
//         // get texture Color32 array
//         var barcodeBitmap = inputTexture.GetPixels32();
//         // detect and decode the barcode inside the Color32 array
//         var result = reader.Decode(barcodeBitmap, inputTexture.width, inputTexture.height);
//         // do something with the result
//         if (result != null)
//         {
//             Debug.Log(result.BarcodeFormat.ToString());
//             Debug.Log(result.Text);
//         }
//     }

   

    

//         /// <summary> Updates this object. </summary>
//         void Update()
//         {
//             if (RGBCamTexture == null)
//             {
//                 return;
//             }
//             // FrameCount.text = RGBCamTexture.FrameCount.ToString();
//         }

//         /// <summary> Plays this object. </summary>
//         public void Play()
//         {
//             if (RGBCamTexture == null)
//             {
//                 RGBCamTexture = new NRRGBCamTexture();
//                 CaptureImage.texture = RGBCamTexture.GetTexture();
//             }

//             RGBCamTexture.Play();
//             // The origin texture will be destroyed after call "Stop",
//             // Rebind the texture.
//             CaptureImage.texture = RGBCamTexture.GetTexture();
//         }

//         /// <summary> Pauses this object. </summary>
//         public void Pause()
//         {
//             RGBCamTexture?.Pause();
//         }

//         /// <summary> Stops this object. </summary>
//         public void Stop()
//         {
//             RGBCamTexture?.Stop();
//             RGBCamTexture = null;
//         }

//         /// <summary> Executes the 'destroy' action. </summary>
//         void OnDestroy()
//         {
//             RGBCamTexture?.Stop();
//             RGBCamTexture = null;
//         }
    
// }
// }