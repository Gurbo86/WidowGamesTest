using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    // Start is called before the first frame update

    public string url = "https://api.pubg.com/tournaments";
    
    void Start()
    {
        StartCoroutine(RESTHandler.Instance.Get(url));
    }
}
