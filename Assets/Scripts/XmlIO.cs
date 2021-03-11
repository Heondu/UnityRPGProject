using System.Collections.Generic;
using System.Xml;
using System.IO;

public class XmlIO
{
    private const string dataPath = "Assets/Resources/";

    public static void Write(List<Item> ItemList, string fileName)
    {
        IsExists(dataPath + fileName + ".xml");

        XmlDocument Document = new XmlDocument();
        XmlElement ItemListElement = Document.CreateElement("ItemList");
        Document.AppendChild(ItemListElement);

        foreach (Item Item in ItemList)
        {
            XmlElement ItemElement = Document.CreateElement("Item");
            ItemElement.SetAttribute("ID", Item.id.ToString());
            ItemElement.SetAttribute("Name", Item.name);
            ItemElement.SetAttribute("ATK", Item.status.ATK.ToString());
            ItemElement.SetAttribute("criticalHitChance", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("globalCriticalMultiplier", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("DEF", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("HP", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("SPI", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("fireTypeStrength", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("waterTypeStrength", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("darkTypeStrength", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("brightTypeStrength", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("fireTypeREG", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("waterTypeREG", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("darkTypeREG", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("brightTypeREG", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("durabilityNegation", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("damageMultiplier", Item.status.criticalHitChance.ToString());
            ItemElement.SetAttribute("ATKMultiplier", Item.status.criticalHitChance.ToString());
            ItemListElement.AppendChild(ItemElement);
        }
        Document.Save(dataPath + fileName + ".xml");
    }

    public static List<Item> Read(string fileName)
    {
        IsExists(dataPath + fileName + ".xml");

        XmlDocument Document = new XmlDocument();
        Document.Load(dataPath + fileName + ".xml");
        XmlElement ItemListElement = Document["ItemList"];

        List<Item> ItemList = new List<Item>();

        foreach (XmlElement ItemElement in ItemListElement.ChildNodes)
        {
            Item Item = new Item();
            Item.id = System.Convert.ToInt32(ItemElement.GetAttribute("ID"));
            Item.name = ItemElement.GetAttribute("Name");
            Item.status.ATK = System.Convert.ToSingle(ItemElement.GetAttribute("ATK"));
            Item.status.criticalHitChance = System.Convert.ToSingle(ItemElement.GetAttribute("criticalHitChance"));
            Item.status.globalCriticalMultiplier = System.Convert.ToSingle(ItemElement.GetAttribute("globalCriticalMultiplier"));
            Item.status.DEF = System.Convert.ToSingle(ItemElement.GetAttribute("DEF"));
            Item.status.HP = System.Convert.ToSingle(ItemElement.GetAttribute("HP"));
            Item.status.SPI = System.Convert.ToSingle(ItemElement.GetAttribute("SPI"));
            Item.status.fireTypeStrength = System.Convert.ToSingle(ItemElement.GetAttribute("fireTypeStrength"));
            Item.status.waterTypeStrength = System.Convert.ToSingle(ItemElement.GetAttribute("waterTypeStrength"));
            Item.status.darkTypeStrength = System.Convert.ToSingle(ItemElement.GetAttribute("darkTypeStrength"));
            Item.status.brightTypeStrength = System.Convert.ToSingle(ItemElement.GetAttribute("brightTypeStrength"));
            Item.status.fireTypeREG = System.Convert.ToSingle(ItemElement.GetAttribute("fireTypeREG"));
            Item.status.waterTypeREG = System.Convert.ToSingle(ItemElement.GetAttribute("waterTypeREG"));
            Item.status.darkTypeREG = System.Convert.ToSingle(ItemElement.GetAttribute("darkTypeREG"));
            Item.status.brightTypeREG = System.Convert.ToSingle(ItemElement.GetAttribute("brightTypeREG"));
            Item.status.durabilityNegation = System.Convert.ToSingle(ItemElement.GetAttribute("durabilityNegation"));
            Item.status.damageMultiplier = System.Convert.ToSingle(ItemElement.GetAttribute("damageMultiplier"));
            Item.status.ATKMultiplier = System.Convert.ToSingle(ItemElement.GetAttribute("ATKMultiplier"));
            ItemList.Add(Item);
        }
        return ItemList;
    }

    private static void IsExists(string path)
    {
        if (File.Exists(path)) return;
        else Create(path);
    }

    private static void Create(string path)
    {
        File.Create(path).Close();
    }
}
