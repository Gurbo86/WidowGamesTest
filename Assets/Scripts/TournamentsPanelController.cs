using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DataTournaments : Data
{
    public Attributes attributes;
}

[Serializable]
public class Attributes
{
    public string createdAt;
}

[Serializable]
public class TournamentList
{
    public List<DataTournaments> data;
}

public class TournamentsPanelController : MonoBehaviour
{
    public string path = "tournaments";
    public GameObject rowGO;
    public RectTransform content;

    private ClientHandler clientHandler;
    private StringBuilder stringBuilder = new StringBuilder();

    private void ServerResponseOk(string JsonResponse)
    {
        TournamentList dataList = new TournamentList();
        JsonUtility.FromJsonOverwrite(JsonResponse, dataList);

        GameObject GO;
        TournamentRow tr;

        foreach (DataTournaments data in dataList.data)
        {
            GO = Instantiate(rowGO, Vector3.zero, Quaternion.identity, content);
            tr = GO.GetComponent<TournamentRow>();
            tr.id.text = data.id;
            tr.date.text = data.attributes.createdAt;
            GO.SetActive(true);
        }

    }

    public void GetListOfTournaments()
    {
        StartCoroutine(
            RESTHandler
            .Instance
            .Get(clientHandler.url + path
                , clientHandler.apiKey
                , ServerResponseOk
            )
        );
    }

    void Start()
    {
        clientHandler = FindObjectOfType<ClientHandler>();
    }
}
