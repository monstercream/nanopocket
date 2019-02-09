using UnityEngine;
using System.Collections;

public class Popup_Stage : MonoBehaviour
{
    public void Init()
    {
        TweenScale tw = GetComponent<TweenScale>();
        tw.PlayForward();
        tw.onFinished.Clear();
    }

    public void ClosePopup()
    {
        TweenScale tw = GetComponent<TweenScale>();
        tw.PlayReverse();
        tw.onFinished.Add(new EventDelegate(this, "OnFinished"));
    }

    public void OnBtnStageStart()
    {

        LoadLevelManager.Instance.LoadLebel("03_game");
    }

    void OnFinished()
    {
        PopupManager.Instance.ClosePopup();
    }
}
