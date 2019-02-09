using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class SkillManager
{

    private static SkillManager s_Instance = null;

    public static SkillManager instance
    {
        get
        {
            if (null == s_Instance)
            {
                s_Instance = new SkillManager();
            }

            return s_Instance;
        }
    }

    public List<DataDefine.SkillData> m_SkillList = new List<DataDefine.SkillData>();
    private GameObject m_objPlayer;

    public void Init()
    {
        TextAsset xmlFile = (TextAsset)Resources.Load("Xml/Skill");

        XmlLoad(xmlFile.text);
    }


    void XmlLoad(string _xmlText)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_xmlText);

        //// ex 각 요소 별 가져오기
        //XmlNodeList idTag = xmlDoc.GetElementsByTagName("id");
        //int id = int.Parse(idTag[0].InnerText);
        //Debug.Log(id);

        // 전체 가져오기
        XmlNodeList nodes = xmlDoc.SelectNodes("Root/Skill");

        foreach (XmlNode node in nodes)
        {
            DataDefine.SkillData sData = new DataDefine.SkillData();

            sData.ID = int.Parse(node.SelectSingleNode("id").InnerText);
            sData.NAME = node.SelectSingleNode("name").InnerText;
            sData.DESC = node.SelectSingleNode("desc").InnerText;
            sData.VALUE = float.Parse(node.SelectSingleNode("value").InnerText);

            m_SkillList.Add(sData);
        }
    }

    public DataDefine.SkillData GetInfo(int _stageid)
    {
        Debug.LogWarning(m_SkillList.Count);
        foreach (DataDefine.SkillData item in m_SkillList)
        {
            Debug.LogWarning(item.ID + "+==+" + _stageid);
            if (item.ID == _stageid)
            {
                return item;
            }
        }

        return null;
    }
}
