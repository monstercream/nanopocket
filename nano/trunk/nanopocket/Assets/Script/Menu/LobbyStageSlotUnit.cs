using UnityEngine;
using System.Collections;

public class LobbyStageSlotUnit : MonoBehaviour
{
    public GameObject m_onLayer;
    public GameObject m_offLayer;
    public GameObject[] m_starObj;
    public UILabel m_IndexLabel;
    private DataDefine.StageData m_Stageinfo;

    private int m_index;
    private int m_level;
    private int m_stageid;

    public void Init(int index, int level)
    {
        m_index = index;
        m_level = level;
        m_stageid = ((m_index + 1) + (GeneralDefine.SLOT_NUM * (m_level - 1))) - 1;
        m_Stageinfo = DataManager.Instance.GetStageInfo(m_stageid);
        m_IndexLabel.text = ((m_index % GeneralDefine.SLOT_NUM) + 1).ToString();
        
        StageState(m_Stageinfo.IsClear);
        SetStageStar(m_Stageinfo.StarCount);
    }

    public void SetStageStar(int count)
    {
        for (int i = 1; i <= m_starObj.Length; i++)
        {
            m_starObj[i - 1].SetActive(count >= i);
        }
    }

    public void StageState(bool isclear)
    {
        m_onLayer.SetActive(isclear);
        m_offLayer.SetActive(!isclear);
    }

    public void OnClickButton()
    {
        //Debug.Log(m_index);
        GameObject common = PopupManager.Instance.MakePopup(EnumDefine.PopupT.STAGE);
        common.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
    }
}
