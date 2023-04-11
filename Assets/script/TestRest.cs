using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System.Net;
using System.IO;
using TMPro;
using System.Collections.Generic;

public class TestRest : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textData;

    [SerializeField]
    Camera mainCam;
    RaycastHit hit;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                DownloadHttpsData();
            }
        }
    }
    private void OnGUI()
    {
        if(GUI.Button(new Rect(100,100,100,100),"Weather Update"))
        {
            HttpWebRequest hwReq = (HttpWebRequest)WebRequest.Create("https://api.openweathermap.org/data/3.0/onecall?lat=33.44&lon=-94.04&exclude=hourly,daily&appid=b27513329b4a49f2aaf636693213ccd0");
            HttpWebResponse hwRes = (HttpWebResponse)hwReq.GetResponse();
            StreamReader strRed = new StreamReader(hwRes.GetResponseStream());

            string jData = strRed.ReadToEnd();
            Data dValue = JsonUtility.FromJson<Data>(jData);
            //textData.text = jData;
            textData.text = " TimeZone -  " + dValue.timezone + "\nWeather - " + dValue.current.weather[0].description + "\nHumidity - " + dValue.current.humidity + "\n Temprature - " + dValue.current.temp + "\n Wind Speed - " + dValue.current.wind_speed;

        }
    }
    private void DownloadHttpsData() => StartCoroutine(downloadHttpsData());

    IEnumerator downloadHttpsData()
    {
        UnityWebRequest www = new UnityWebRequest("https://api.openweathermap.org/data/3.0/onecall?lat=" + hit.collider.transform.eulerAngles.x + "&lon=" + (hit.collider.transform.eulerAngles.y + 90) + "&exclude=hourly,daily&appid=b27513329b4a49f2aaf636693213ccd0");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string datavalue = www.downloadHandler.text;
            Data dValue = JsonUtility.FromJson<Data>(datavalue);
            Debug.LogError(datavalue);
            textData.text = " TimeZone -  " + dValue.timezone + "\nWeather - " + dValue.current.weather[0].description + "\nHumidity - " + dValue.current.humidity + "\n Temprature - " + dValue.current.temp + "\n Wind Speed - " + dValue.current.wind_speed;
        }
    }
}

[System.Serializable]
public class Data
{
    public string timezone;
    public string timezone_offset;
    public DataValue current;
}

[System.Serializable]
public class DataValue
{
    public string sunrise;
    public string sunset;
    public string temp;
    public string feels_like;
    public string humidity;
    public string clouds;
    public string wind_speed;
    public List<Weather> weather;
}


[System.Serializable]
public class Weather
{
    public string id;
    public string main;
    public string description;
    public string icon;
}



