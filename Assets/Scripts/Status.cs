using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringFloat : SerializableDictionary<string, float> { }

public class Status : MonoBehaviour
{

    public StringFloat status = new StringFloat();
}
