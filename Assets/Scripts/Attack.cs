using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int damage;

    public void Execute()
    {
        Debug.Log($"[공격] 데미지 {damage}!");
    }
}
