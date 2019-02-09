using UnityEngine;
using System.Collections;

public class Popup_Setting : MonoBehaviour {

    class SettingInfo
    {
        public bool bVibrtion = false;
        public bool bMusic = false;
        public bool bSound = false;
        public bool bMessages = false;
    }

    private bool m_bVibrtion = false;
    private bool m_bMusic = false;
    private bool m_bSound = false;
    private bool m_bMessages = false;

    // Use this for initialization
    void Start ()
    {
        LoadSetting();
        SetSetting();
    }

    private void SetSetting()
    {

    }

    private void LoadSetting()
    {
        string data = PlayerPrefs.GetString("SettingInfo", "");

        if (data != "")
        {
            SettingInfo Settinginfo = JsonUtility.FromJson<SettingInfo>(data);

            this.m_bMessages = Settinginfo.bMessages;
            this.m_bMusic = Settinginfo.bMusic;
            this.m_bSound = Settinginfo.bSound;
            this.m_bVibrtion = Settinginfo.bVibrtion;
        }
    }

    private void SaveSetting()
    {
        SettingInfo data = new SettingInfo();

        data.bMessages = this.m_bMessages;
        data.bMusic = this.m_bMusic;
        data.bSound = this.m_bSound;
        data.bVibrtion = this.m_bVibrtion;

        string strSettinginfo = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SettingInfo", strSettinginfo);
    }

    public void PushCancel()
    {
        PopupManager.Instance.ClosePopup();
    }

    public void PushSave()
    {
        SaveSetting();
        GameObject popup = PopupManager.Instance.MakePopup(EnumDefine.PopupT.COMMON);
        popup.GetComponent<Popup_Common>().SetPopupInfo(1);
        popup.GetComponent<Popup_Common>().SetCommonPopup(1, Confirm);
    }

    void Confirm()
    {
        PopupManager.Instance.ClosePopup();
    }
}
