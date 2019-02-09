using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager
{
    private static DataManager _Instance = null;

    public static DataManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new DataManager();
            }

            return _Instance;
        }
    }

    DataDefine.UserDataInfo m_Userinfo = new DataDefine.UserDataInfo();

    public void SetUserData(DataDefine.UserDataInfo info)
    {
        m_Userinfo = info;
    }

    public DataDefine.UserDataInfo GetUserData()
    {
        return m_Userinfo;
    }

    public DataDefine.StageData GetStageInfo(int id)
    {
        for (int i = 0; i < m_Userinfo.m_StageInfoList.Count; i++)
        {
            if(i == id)
            {
                return m_Userinfo.m_StageInfoList[i];
            }
        }
        return null;
    }

    public int GetClearStage()
    {
        for (int i = 0; i < m_Userinfo.m_StageInfoList.Count; i++)
        {
            if (i + 1 == m_Userinfo.m_StageInfoList.Count)
            {
                return m_Userinfo.m_StageInfoList.Count;
            }

            if (m_Userinfo.m_StageInfoList[i].IsClear == false)
            {
                return i;
            }
        }
        return 0;
    }

    public bool IsClearStage(int stage)
    {
        if (m_Userinfo.m_StageInfoList.Count > stage)
        {
            return m_Userinfo.m_StageInfoList[stage - 1].IsClear;
        }

        return false;
    }

    List<DataDefine.ShopData> m_shopDatas = new List<DataDefine.ShopData>();

    public void SetShopData(List<DataDefine.ShopData> datas)
    {
        m_shopDatas = datas;
    }

    public List<DataDefine.ShopData> GetShopData()
    {
        return m_shopDatas;
    }
}

