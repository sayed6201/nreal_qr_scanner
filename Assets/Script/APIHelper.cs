using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Net;

//unitywebreq
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;

public class APIHelper 
{
    private static string url = "https://raw.githubusercontent.com/sayed6201/restapi_img_trk/main/nest";

    private static Dictionary<string, string> companyInfo;

    public static string DEFAULT_DISPLAY_TEXT = "Look at the Qr code and hold down the controller button  to scan";

    //private APIHelper apiHelper = null;

    //public APIHelper init()
    //{
    //    if (apiHelper == null) return new APIHelper();
    //    else this.apiHelper; 
    //}

    public static InfoObj getInfo()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        Debug.Log(json);
        return JsonUtility.FromJson<InfoObj>(json);

    }

    public static async Task<InfoObj> getInfoWithUnityWebReq()
    {
        InfoObj mydata = null;
        using var www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Content-Type", "application/json");
        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        var jsonResponse = www.downloadHandler.text;

        if (www.result == UnityWebRequest.Result.Success)
            Debug.Log($"success: {www.downloadHandler.text}");
        else
            Debug.Log($"failed: {www.error}");

        try
        {
            
            mydata = JsonUtility.FromJson<InfoObj>(jsonResponse);

            Debug.Log($"my list {mydata.data[0]}");
            DataSaver.saveData(mydata, MyConst.CACHE_NAME);

            return mydata;
        }
        catch(Exception e)
        {
            Debug.Log(e);
            return mydata;
        }
    }


    /*
    As an alternative you could use my SimpleJSON parser which does not
    deserialize the json into your own datastructures,
    but just parses the json into its own internal structures and provides a convenient way of accessing the data.
    It can handle any valid json data.

     JSONNode root = JSON.Parse(jsonResponse);
 
     var hist = root[0]["envStatData"]["historical"][0];
     Debug.Log(hist["pm25"].AsFloat+" "+hist["so2"].AsFloat); 
     
     */

    public static string getCompanyInfo(string qrKey)
    {
        companyInfo = new Dictionary<string, string>(){
                    {"https://product101", "Key Takeaways. The world's only major large passenger aircraft manufacturers are Boeing and Airbus. Boeing's and Airbus's established jet brands are the 7-series and A-series, respectively. Up-and-coming large passenger airplane makers include Comac in China, Mitsubishi in Japan, and UAC in Russia."},
                    {"https://car-seller", "Tulip Tech. is an Uk automotive and clean energy company based in Austin, Texas. Tulip designs and manufactures electric vehicles, battery energy storage from home to grid-scale, solar panels and solar roof tiles, and related products and services"},
                    {"https://shoes.com", "Nike, Inc. (/ˈnaɪki/ or /ˈnaɪk/) is an American multinational corporation that is engaged in the design, development, manufacturing, and worldwide marketing and sales of footwear, apparel, equipment, accessories, and services. The company is headquartered near Beaverton, Oregon, in the Portland metropolitan area."}
                };

        return companyInfo[qrKey];
    }


}
