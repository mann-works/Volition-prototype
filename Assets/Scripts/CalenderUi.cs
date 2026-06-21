using System;
using TMPro;
using UnityEngine;

public class CalenderUi : MonoBehaviour
{
    public TMP_Text dateText;

    private DateTime currentDate = new DateTime(2020, 6, 19);

    void Start()
    {
        UpdateUI();
    }
    public void NextDay()
    {
        currentDate = currentDate.AddDays(1);
        UpdateUI();
    }

    void UpdateUI()
    {
        dateText.text =
            currentDate.ToString("dddd, dd MMMM yyyy");
    }
}
