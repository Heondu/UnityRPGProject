public enum Type { fire = 0, water, dark, bright };

[System.Serializable]
public class SkillStatus
{
    public Type type;
    public float typeStrenght;
    public float magnitude;
    public bool isAbleCriticalHit;
    public int randDirection;
    public bool isGuidedMissile;
    public int cast;
    public float coolTime;
    public float boundsMultiplier;
    public float rangeMultiplier;
    public float speedMultiplier;
}
