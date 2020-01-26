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
    public Text text;

    private ClientHandler clientHandler;
    private StringBuilder stringBuilder = new StringBuilder();

    private void ServerResponseOk(string JsonResponse)
    {
        TournamentList dataList = new TournamentList();
        JsonUtility.FromJsonOverwrite(JsonResponse, dataList);

        stringBuilder.AppendLine("Tournament Id:\t\t\tTournament Date:");
        foreach (DataTournaments data in dataList.data)
        {
            stringBuilder.Append(data.id);
            stringBuilder.Append("\t\t\t");
            stringBuilder.AppendLine(data.attributes.createdAt);
        }

        text.text = stringBuilder.ToString();
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
