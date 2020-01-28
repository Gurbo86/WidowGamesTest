using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TournamentRow : MonoBehaviour
{
    public Text id;
    public Text date;

    private DateTime auxDate;

    public string Id
    {
        set
        {
            id.text = value;
        }
    }

    public string Date
    {
        set
        {
            if (
                DateTime.TryParseExact(
                    value
                    , "yyyy-MM-dd'T'HH:mm:ss'Z'"
                    , null
                    , DateTimeStyles.AssumeUniversal
                    , out auxDate
                )
            )
            {
                date.text = auxDate
                    .ToString(
                        "dd/MM/yyyy hh:mm:ss"
                        , CultureInfo.CurrentCulture
                    );
            }
            else
            {
                date.text = "Wrong Date Format";
            }
        }
    }

    public void Clear()
    {
        id.text = string.Empty;
        date.text = string.Empty;
    }
}
