using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public int intelligence;
    public int strength;
    public int agility;
    public int focus;

    public void AddIntelligence(int amount)
    {
        intelligence += amount;
        Debug.Log($"INT +{amount} | Total: {intelligence}");
    }

    public void AddStrength(int amount)
    {
        strength += amount;
        Debug.Log($"STR +{amount} | Total: {strength}");
    }

    public void AddAgility(int amount)
    {
        agility += amount;
        Debug.Log($"AGI +{amount} | Total: {agility}");
    }

    public void AddFocus(int amount)
    {
        focus += amount;
        Debug.Log($"FOC +{amount} | Total: {focus}");
    }
}
