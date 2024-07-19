using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TrainStatusChecker : MonoBehaviour
{
    public InputField userInputField;
    public Text statusText;

    public void CheckStatus()
    {
        string lineName = userInputField.text;
        string url = GetUrlForLine(lineName);

        StartCoroutine(GetStatusFromWeb(url));
    }

    string GetUrlForLine(string lineName)
    {
        switch (lineName)
        {
            case "東海道線":
                return "https://transit.yahoo.co.jp/traininfo/detail/27/0/";
            case "山手線":
                return "https://transit.yahoo.co.jp/traininfo/detail/22/0/";
            case "成田線":
                return "https://transit.yahoo.co.jp/diainfo/67/0/";
	    case "東葉高速":
                return "https://transit.yahoo.co.jp/diainfo/142/0";
            default:
                return "";
        }
    }

    IEnumerator GetStatusFromWeb(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            statusText.text = "運行状況の取得に失敗しました。";
        }
        else
        {
            string html = request.downloadHandler.text;
            string status = ParseHtmlForStatus(html);
            statusText.text = status;
        }
    }

    string ParseHtmlForStatus(string html)
    {
        // Example implementation using Regex (not recommended for complex parsing)
        if (html.Contains("trouble"))
        {
            return "遅延しています";
        }
        else
        {
            return "通常運転です";
        }
    }
}

