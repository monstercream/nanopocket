using UnityEngine;
using System.Collections;

public class PopUp_Login : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnBtnLogin()
    {
        LoadLevelManager.Instance.LoadLebel("01_lobby");
    }

    public void OnBtnSignUp()
    {
        Debug.LogWarning("OnBtnSignUp");
    }

    public void OnBtnLostPassward()
    {
        Debug.LogWarning("OnBtnLostPassward");

    }

    public void OnBtnExit()
    {
        Debug.LogWarning("OnBtnExit");
        PopupManager.Instance.MakePopup(EnumDefine.PopupT.COMMON);
        
        GameObject popup = PopupManager.Instance.MakePopup(EnumDefine.PopupT.COMMON);
        popup.GetComponent<Popup_Common>().SetPopupInfo(1);
        popup.GetComponent<Popup_Common>().SetCommonPopup(1, Confirm, 1, Cancel);
    }

    public void Confirm()
    {
        Debug.LogWarning("Confirm");
        Application.Quit();
    }

    public void Cancel()
    {
        Debug.LogWarning("Cancel");
        PopupManager.Instance.ClosePopup();
    }
}
