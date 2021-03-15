using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public void Execute(int damage)
    {
        Debug.Log($"[공격] 데미지 {damage}!");
    }
}
