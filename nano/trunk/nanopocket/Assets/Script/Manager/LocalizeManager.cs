using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class LocalizeManager
{
    #region Singleton 
    private static LocalizeManager _Instance = null;
    public static LocalizeManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new LocalizeManager();
            }

            return _Instance;
        }
    }
    private LocalizeManager() { Init(); }
    #endregion

    public List<DataDefine.Localize> m_localizes = new List<DataDefine.Localize>();
    public Dictionary<string, string> m_localizeDic = new Dictionary<string, string>();
    public EnumDefine.Nation m_CurrentNation = EnumDefine.Nation.None;
    
    private void Init()
    {
        InitNation();        
        XmlLoad();
        KeySetting();
    }

    private void InitNation(EnumDefine.Nation nation = EnumDefine.Nation.None)
    {
        if(nation != EnumDefine.Nation.None)
        {
            m_CurrentNation = nation;
            return;
        }

        if(Application.systemLanguage == SystemLanguage.Korean)
        {
            m_CurrentNation = EnumDefine.Nation.KO;
        }
        else
        { 
            m_CurrentNation = EnumDefine.Nation.USA;
        }
    }

    private void XmlLoad()
    {
        m_localizes.Clear();

        TextAsset xmlFile = (TextAsset)Resources.Load("Xml/Localize");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.text);
        XmlNodeList nodes = xmlDoc.SelectNodes("Root/Localize");
        foreach (XmlNode node in nodes)
        {
            DataDefine.Localize loData = new DataDefine.Localize();
            loData.Id = int.Parse(node.SelectSingleNode("id").InnerText);
            loData.Key = node.SelectSingleNode("key").InnerText;
            loData.Ko = node.SelectSingleNode("ko").InnerText;
            loData.En = node.SelectSingleNode("en").InnerText;
            m_localizes.Add(loData);
        }
    }

    private void KeySetting()
    {
        if (m_localizes == null)
            return;

        if (m_localizes.Count == 0)
            return;

        m_localizeDic.Clear();

        if (m_CurrentNation == EnumDefine.Nation.KO)
        {
            foreach(var local in m_localizes)
            {
                m_localizeDic.Add(local.Key, local.Ko);
            }            
        }
        else
        {
            foreach (var local in m_localizes)
            {
                m_localizeDic.Add(local.Key, local.En);
            }
        }
    }

    public void SetLabel(UILabel label, int ilocalkey)
    {
        if (m_CurrentNation == EnumDefine.Nation.KO)
        {
            label.text = m_localizes[ilocalkey].Ko;
        }
        else if (m_CurrentNation == EnumDefine.Nation.USA)
        {
            label.text = m_localizes[ilocalkey].En;
        }
    }

    public string LocallizeStr(string key)
    {
        return m_localizeDic[key];
    }
}
