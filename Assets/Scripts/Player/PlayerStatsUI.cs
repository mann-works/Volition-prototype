using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private StatRowUI statRowPrefab;

    private Dictionary<StatType, StatRowUI> rows = new();

    private void Start()
    {
        BuildUI();

        PlayerStats.Instance.OnStatsChanged += RefreshUI;
    }

    private void OnDestroy()
    {
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.OnStatsChanged -= RefreshUI;
    }

    private void BuildUI()
    {
        foreach (var pair in PlayerStats.Instance.Stats)
        {
            StatRowUI row = Instantiate(statRowPrefab, content);

            row.SetData(pair.Key, pair.Value);

            rows.Add(pair.Key, row);
        }
    }

    private void RefreshUI()
    {
        foreach (var pair in PlayerStats.Instance.Stats)
        {
            rows[pair.Key].SetData(pair.Key, pair.Value);
        }
    }
}