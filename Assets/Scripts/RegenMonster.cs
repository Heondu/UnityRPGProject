using System.Collections.Generic;
using UnityEngine;

public class RegenMonster : MonoBehaviour
{
    private GameObject monsterPrefab;

    private void Awake()
    {
        monsterPrefab = Resources.Load("Prefabs/Monster") as GameObject;
    }

    public void SpawnMonster(string monType, int regenNumMax, Vector2 position)
    {
        for (int x = 0; x < regenNumMax / 4; x++)
        {
            for (int y = 0; y < regenNumMax / 4; y++)
            {
                Vector2 size = monsterPrefab.GetComponent<BoxCollider2D>().size;
                Vector2 newPos = position + new Vector2(size.x * x, size.y * y);
                Vector2 offset = new Vector2(size.x * -(regenNumMax / 8), size.y * -(regenNumMax / 8));
                GameObject clone = Instantiate(monsterPrefab, newPos + offset, Quaternion.identity);
                clone.GetComponent<Enemy>().name = monType;
            }
        }
    }
}
