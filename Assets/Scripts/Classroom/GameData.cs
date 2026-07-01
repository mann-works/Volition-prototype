using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;


[Serializable]
public class GameDate
{
    public int year;
    public int month;
    public int day;

    public bool Equals(DateTime date)
    {
        return year == date.Year &&
               month == date.Month &&
               day == date.Day;
    }
}

