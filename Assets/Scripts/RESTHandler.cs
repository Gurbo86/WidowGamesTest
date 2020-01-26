using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RESTHandler : MonoBehaviour
{
    private static RESTHandler _instance;

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

    public IEnumerator Get(string url, string apiKey, Action<string> response)
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
                    string result = www.downloadHandler.text;
                    response.Invoke(result);
                }
            }
        }
    }
}
