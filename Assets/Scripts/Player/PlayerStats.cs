using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public event Action OnStatsChanged;

    [SerializeField] private int maxStressLimit = 15;

    private Dictionary<StatType, StatData> stats = new();

    public IReadOnlyDictionary<StatType, StatData> Stats => stats;
    public int MaxStressLimit => maxStressLimit;

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

        if (stat == StatType.Stress && stats[stat].XP >= maxStressLimit)
        {
            ConfirmationUI.Instance.Show(
                "Your stress levels have exceeded the limit! Restart game?",
                () => GameManager.Instance.RestartGame(),
                () => SceneManager.LoadScene(0)
            );
        }
    }

    public StatData GetStat(StatType stat)
    {
        return stats[stat];
    }
}