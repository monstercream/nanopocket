using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

public class StageManager
{
    private static StageManager s_Instance = null;

    public static StageManager instance
    {
        get
        {
            if (null == s_Instance)
            {
                s_Instance = new StageManager();
            }

            return s_Instance;
        }
    }

    public List<DataDefine.StageData> m_StageList = new List<DataDefine.StageData>();
    
    public void Init()
    {
        TextAsset xmlFile = (TextAsset)Resources.Load("Xml/Stage");

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
        XmlNodeList nodes = xmlDoc.SelectNodes("Root/Stage");

        foreach(XmlNode node in nodes)
        {
            DataDefine.StageData sData = new DataDefine.StageData();

            sData.ID = int.Parse( node.SelectSingleNode("id").InnerText );
            sData.BallNum = int.Parse(node.SelectSingleNode("ballcount").InnerText);
            
            m_StageList.Add(sData);
        }
    }

    public DataDefine.StageData GetStageInfo(int _stageid)
    {
		Debug.Log("stageList : " + m_StageList.Count);
        foreach (DataDefine.StageData item in m_StageList)
        {
            if(item.ID == _stageid)
            {
                return item;
            }
        }

        return null;
    }
}
