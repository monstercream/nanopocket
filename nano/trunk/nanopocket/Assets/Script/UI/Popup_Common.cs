using UnityEngine;
using System.Collections;


public class Popup_Common : MonoBehaviour
{
    public GameObject[] m_objPopupBtn;

    public UILabel[] m_LabelBtn = null;
    public UILabel m_LabelTitle = null;
    public UILabel m_LabelDesc = null;


    private EnumDefine.CommonPopupT m_CurrentPopupT;

    private System.Action[] m_EventBtn = new System.Action[3];

    private void Awake()
    {
        for (int i = 0; i < m_objPopupBtn.Length; i++)
        {
            m_objPopupBtn[i].SetActive(false);
        }
    }

    private void Init()
    {
    }

    public void SetPopupInfo(int idesckey, int ititlekey = 0)
    {
        LocalizeManager.Instance.SetLabel(m_LabelTitle, ititlekey);
        LocalizeManager.Instance.SetLabel(m_LabelDesc, idesckey);
    }

    public void SetCommonPopup(int ibtnkey1, System.Action btnEvent1, int ibtnkey2 = 0, System.Action btnEvent2 = null, int ibtnkey3 = 0, System.Action btnEvent3 = null)
    {
        if (btnEvent2 == null)
        {
            m_CurrentPopupT = EnumDefine.CommonPopupT.TYPE1;
            m_objPopupBtn[1].SetActive(true);
            LocalizeManager.Instance.SetLabel(m_LabelBtn[1], ibtnkey1);
            m_EventBtn[0] = btnEvent1;
        }
        else if (btnEvent3 == null)
        {
            m_CurrentPopupT = EnumDefine.CommonPopupT.TYPE2;
            m_objPopupBtn[0].SetActive(true);
            m_objPopupBtn[2].SetActive(true);

            LocalizeManager.Instance.SetLabel(m_LabelBtn[0], ibtnkey1);
            LocalizeManager.Instance.SetLabel(m_LabelBtn[2], ibtnkey2);

            m_EventBtn[0] = btnEvent1;
            m_EventBtn[2] = btnEvent2;
        }
        else
        {
            m_CurrentPopupT = EnumDefine.CommonPopupT.TYPE3;
            m_objPopupBtn[0].SetActive(true);
            m_objPopupBtn[1].SetActive(true);
            m_objPopupBtn[2].SetActive(true);

            LocalizeManager.Instance.SetLabel(m_LabelBtn[0], ibtnkey1);
            LocalizeManager.Instance.SetLabel(m_LabelBtn[1], ibtnkey2);
            LocalizeManager.Instance.SetLabel(m_LabelBtn[2], ibtnkey3);
            
            m_EventBtn[0] = btnEvent1;
            m_EventBtn[1] = btnEvent2;
            m_EventBtn[2] = btnEvent3;
        }
    }

    public void PushFirstButton()
    {
        m_EventBtn[0]();
    }

    public void PushSecondButton()
    {
        m_EventBtn[1]();
    }

    public void PushThirdButton()
    {
        m_EventBtn[2]();
    }
}
