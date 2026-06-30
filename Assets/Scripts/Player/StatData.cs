using UnityEngine;

[System.Serializable]
public class StatData 
{
    [SerializeField] private int xp;

    public int XP => xp;

    public int Level
    {
        get
        {
            if (xp < 20) return 1;
            if (xp < 50) return 2;
            if (xp < 90) return 3;
            if (xp < 140) return 4;

            return 5;
        }
    }

    public void AddXP(int amount)
    {
        xp += amount;
    }
}


