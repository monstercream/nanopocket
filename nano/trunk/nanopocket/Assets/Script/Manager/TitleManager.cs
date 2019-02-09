using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Login();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PushLoadLobby()
    {
        PopupManager.Instance.MakePopup(EnumDefine.PopupT.LOGIN);
    }

    private void Login()
    {
        //StartCoroutine(UserInfoDownload);

        //Dummy////////////////////////////
        DataDefine.UserDataInfo info = new DataDefine.UserDataInfo();
        List<DataDefine.StageData> stageinfolist = new List<DataDefine.StageData>();

        for (int i = 0; i < GeneralDefine.Max_Stage; i++)
        {
            DataDefine.StageData stageinfo = new DataDefine.StageData();
            stageinfo.ID = i;
            stageinfo.Rank = EnumDefine.RankT.Rank_C;
            stageinfo.HighScore = i * 100;
            stageinfo.StarCount = Random.Range(1, 4);

            if (i < 4)
            {
                stageinfo.IsClear = true;
            }

            stageinfolist.Add(stageinfo);
        }

        info.m_iUID = 1;
        info.m_iLV = 7;
        info.m_iExp = 17;
        info.m_iCoin = 120;
        info.m_iCash = 1400;
        info.m_strName = "TestUser";
        info.m_StageInfoList = stageinfolist;

        DataManager.Instance.SetUserData(info);

        ///////////////////////////////////

        // shop Data
        var shopDatas = XMLManager.Instance.GetLoadShopData();
        DataManager.Instance.SetShopData(shopDatas);        
    }

    IEnumerator UserInfoDownload()
    {
        return null;
    }
}
