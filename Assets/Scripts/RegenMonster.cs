using UnityEngine;

public class RegenMonster : MonoBehaviour
{
    private GameObject monsterPrefab;

    private void Awake()
    {
        monsterPrefab = Resources.Load("Prefabs/Monster") as GameObject;
    }

    public void SpawnMonster(string monType, int regenNumMax, Vector2 size)
    {
        for (int x = 0; x < regenNumMax / 4; x++)
        {
            for (int y = 0; y < regenNumMax / 4; y++)
            {
                Vector2 pos = new Vector2(size.x / (regenNumMax / 4) * x, size.y / (regenNumMax / 4) * y);
                Vector2 offset = new Vector2((regenNumMax / 8) - size.x / 2, (regenNumMax / 8) - size.y / 2);
                GameObject clone = Instantiate(monsterPrefab, pos + offset, Quaternion.identity);
                clone.GetComponent<Enemy>().name = monType;
            }
        }
    }
}
