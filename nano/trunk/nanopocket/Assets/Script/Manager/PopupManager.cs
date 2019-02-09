using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopupManager 
{
    private static PopupManager _Instance = null;

    public static PopupManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new PopupManager();
            }

            return _Instance;
        }
    }

    private static Stack m_StackPopup = new Stack();

    private List<GameObject> m_objCreatPopupList = new List<GameObject>();

    public GameObject MakePopup(EnumDefine.PopupT popupT)
    {
        string strPathName = "Prefab/UI/";
        string strName = "";
        GameObject objpopup = null;

        switch (popupT)
        {
            case EnumDefine.PopupT.COMMON: { strName = "PopUp_Common"; } break;
            case EnumDefine.PopupT.PAUSE: { strName = "PopUp_Pause"; } break;
            case EnumDefine.PopupT.RESULT: { strName = "PopUp_Result"; } break;
            case EnumDefine.PopupT.STAGE: { strName = "PopUp_Stage"; } break;
            case EnumDefine.PopupT.SHOP: { strName = "PopUp_Shop"; } break;
            case EnumDefine.PopupT.SETTING: { strName = "PopUp_Setting"; } break;
            case EnumDefine.PopupT.LOGIN: { strName = "PopUp_Login"; } break;

        }

        for (int i = 0; i < m_objCreatPopupList.Count; i++)
        {
            if (m_objCreatPopupList[i].name == strName)
            {
                objpopup = m_objCreatPopupList[i];

                objpopup.SetActive(true);

                m_StackPopup.Push(m_objCreatPopupList[i]);

                return objpopup;
            }
        }

        objpopup = (GameObject)UnityEngine.GameObject.Instantiate(Resources.Load(strPathName + strName));

        GameObject Achor = GameObject.Find("Anchor(Center)");
        objpopup.transform.parent = Achor.transform;
        objpopup.transform.localScale = Vector3.one;
        objpopup.transform.localPosition = Vector3.zero;

        objpopup.SetActive(true);

        objpopup.name = strName;

        m_StackPopup.Push(objpopup); 
        m_objCreatPopupList.Add(objpopup);
        
        return objpopup;
    }

    public void ClearPopup()
    {
        m_StackPopup.Clear();
        m_objCreatPopupList.Clear();

    }

    public void ClosePopup()
    {
        GameObject objpopup = (GameObject)m_StackPopup.Pop();

        objpopup.SetActive(false);
    }
}
