using UnityEngine;
using System.Collections;

public class LobbyManager : MonoBehaviour
{
    #region Singleton 
    private static LobbyManager m_instance;
    private static LobbyManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = (LobbyManager)FindObjectOfType(typeof(LobbyManager));
                if (m_instance == null)
                {
                    GameObject go = new GameObject("LobbyManager");
                    m_instance = go.AddComponent<LobbyManager>();
                }
            }
            return m_instance;
        }
    }
    #endregion

    public UILabel m_LableHeart = null;
    public UILabel m_LableCash = null;
    public UILabel m_LableCoin = null;

    private DataDefine.UserDataInfo m_UserData;

    private void Start()
    {
       m_UserData = DataManager.Instance.GetUserData();
       SetPlayData();

    }

    private void SetPlayData()
    {
        m_LableCash.text = m_UserData.m_iCash.ToString();
        m_LableCoin.text = m_UserData.m_iCoin.ToString();
        m_LableHeart.text = m_UserData.m_iHeart.ToString();
    }

    public void PushStart()
    {
        LoadLevelManager.Instance.LoadLebel("03_game");
    }

    public void PushSetting()
    {
        GameObject shop = PopupManager.Instance.MakePopup(EnumDefine.PopupT.SETTING);
    }

    public void PushShop()
    {
        //throw new System.NotImplementedException();
        GameObject shop = PopupManager.Instance.MakePopup(EnumDefine.PopupT.SHOP);
        shop.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
    }

    public void PushTrophy()
    {

    }

    public void PushRank()
    {

    }
}