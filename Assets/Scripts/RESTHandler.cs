using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RESTHandler : MonoBehaviour
{
    private static RESTHandler _instance;
    //private static string user = "leonel.maguet@gmail.com";
    //private static string pass = "Njimko@86";
    private static string apiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI5OGE5ZmUzMC0yMWRmLTAxMzgtMzg2ZS03NTczNzdiNTc2MzMiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNTc5OTg0MjkxLCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6Imxlb25lbC1tYWd1ZXQtIn0.jx9OZgLYB2etNWMEL0liK2EtRU_WRynoQBg15ha98N8";

    public static RESTHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RESTHandler>();

                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(RESTHandler).Name;
                    _instance = go.AddComponent<RESTHandler>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public IEnumerator Get(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://api.pubg.com/tournaments"))
        {
            www.SetRequestHeader("Accept", "application/vnd.api+json");
            www.SetRequestHeader("Authorization", "Bearer " + apiKey);

            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log("Server error. It is" + www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log("We have a result. Is :" + result);
                }
            }
        }
    }
}
