using UnityEngine;
using System.Collections;

public class Popup_Result : MonoBehaviour {

    private GameManager m_GameManager;

    public UILabel m_LabelTitle = null;
    public UILabel m_LabelTotalScore = null;
    public UILabel m_LabelTotalScoreValue = null;
    public UILabel m_LabelFeverScore = null;
    public UILabel m_LabelFeverScoreValue = null;
    public UILabel m_LabelReset = null;
    public UILabel m_LabelExit = null;
    
    // Use this for initialization
    public void SetTotalScroe (int _iscore)
    {
        m_LabelTotalScoreValue.text = _iscore.ToString("#,##0");
    }

    public void SetFeverScroe(int _iscore)
    {
        m_LabelFeverScoreValue.text = _iscore.ToString("#,##0");
    }


    // Update is called once per frame
    void Update () {
	
	}

    public void SetGameManager(GameManager _gamemanager)
    {
        m_GameManager = _gamemanager;
    }

    public void PushReStart()
    {
        m_GameManager.Restart();
    }

    public void PushExit()
    {
        m_GameManager.Exit();
    }
}
