using System;
using System.Collections.Generic;
using UnityEngine;

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
    private List<TournamentRow> rowPool;

    private void ServerResponseOk(string JsonResponse)
    {
        TournamentList dataList = new TournamentList();
        JsonUtility.FromJsonOverwrite(JsonResponse, dataList);

        TournamentRow tr;

        foreach (DataTournaments data in dataList.data)
        {
            tr = GetRow();
            tr.Id = data.id;
            tr.Date = data.attributes.createdAt;
            tr.transform.SetParent(content, false);
            tr.gameObject.SetActive(true);
        }

    }

    private TournamentRow GetRow()
    {
        TournamentRow row = rowPool
            .Find(
                x => x.gameObject.activeSelf == false
            );

        if (row == null)
        {
            row = Instantiate(
                rowGO
                , Vector3.zero
                , Quaternion.identity
            )
            .GetComponent<TournamentRow>();

            rowPool.Add(row);
        }

        return row;
    }

    private void clearAllRows()
    {
        foreach (TournamentRow row in rowPool)
        {
            row.gameObject.SetActive(false);
            row.transform.SetParent(transform);
            row.Clear();
        }
    }

    public void GetListOfTournaments()
    {
        if (rowPool.Count != 0)
        {
            clearAllRows();
        }

        StartCoroutine(
            RESTHandler
            .Instance
            .Get(clientHandler.url + path
                , clientHandler.apiKey
                , ServerResponseOk
            )
        );
    }

    public void CloseApp()
    {
        StopAllCoroutines();
        Application.Quit();
    }

    void Start()
    {
        clientHandler = FindObjectOfType<ClientHandler>();
        rowPool = new List<TournamentRow>();
    }
}
