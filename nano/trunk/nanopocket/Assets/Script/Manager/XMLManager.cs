using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class XMLManager
{
    #region Singleton 
    private static XMLManager s_Instance = null;
    public static XMLManager Instance
    {
        get
        {
            if (null == s_Instance)
            {
                s_Instance = new XMLManager();
            }

            return s_Instance;
        }
    }
    private XMLManager() { }
    #endregion
    
    private XmlNodeList GetXmlNodeList(string filePath, string selectNode)
    {
        TextAsset xmlFile = (TextAsset)Resources.Load(filePath);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.text);        
        return xmlDoc.SelectNodes(selectNode);
    }

    public List<DataDefine.ShopData> GetLoadShopData()
    {
        var nodes = GetXmlNodeList("Xml/Shop", "Root/Shop");
        List<DataDefine.ShopData> shops = new List<DataDefine.ShopData>();
        foreach (XmlNode node in nodes)
        {
            DataDefine.ShopData sData = new DataDefine.ShopData();
            sData.ID = int.Parse(node.SelectSingleNode("id").InnerText);
            sData.ItemType = (EnumDefine.ItemT) int.Parse(node.SelectSingleNode("itemType").InnerText);
            sData.payType = (EnumDefine.PayT)int.Parse(node.SelectSingleNode("payType").InnerText);
            sData.NAME = node.SelectSingleNode("name").InnerText;
            sData.DESC = node.SelectSingleNode("desc").InnerText;
            sData.SpriteName = node.SelectSingleNode("spriteName").InnerText;
            sData.ItemCount = node.SelectSingleNode("itemCount").InnerText;
            sData.PRICE = float.Parse(node.SelectSingleNode("price").InnerText);
            shops.Add(sData);
        }
        return shops;
    }

}
