using UnityEngine;
using System.Collections.Generic;

public class Factory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        items = XmlIO.Read("ItemDB");
    }
}
