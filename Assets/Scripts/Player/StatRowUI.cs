using TMPro;
using UnityEngine;

public class StatRowUI : MonoBehaviour
{
    [SerializeField] private TMP_Text statName;
    [SerializeField] private TMP_Text statValue;

    public void SetData(StatType type, StatData data)
    {
        statName.text = type.ToString();
        statValue.text = $"Lv {data.Level} ({data.XP} XP)";
    }
}
