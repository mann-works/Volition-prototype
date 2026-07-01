using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public event Action OnStatsChanged;

    private Dictionary<StatType, StatData> stats = new();

    public IReadOnlyDictionary<StatType, StatData> Stats => stats;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (StatType stat in Enum.GetValues(typeof(StatType)))
        {
            stats.Add(stat, new StatData());
        }
    }

    public void AddXP(StatType stat, int amount)
    {
        stats[stat].AddXP(amount);
        OnStatsChanged?.Invoke();

        if (stat == StatType.Stress && stats[stat].XP >= 15)
        {
            DialogUI.Instance.Show(
                "System",
                "Your stress levels have exceeded the limit! You collapsed. Game Over!",
                () => GameManager.Instance.RestartGame()
            );
        }
    }

    public StatData GetStat(StatType stat)
    {
        return stats[stat];
    }
}