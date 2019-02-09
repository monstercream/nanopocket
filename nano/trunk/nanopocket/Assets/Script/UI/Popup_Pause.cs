using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using System;

public class Popup_Pause : MonoBehaviour {

    private GameManager m_GameSceneManager;
    ShowOptions _ShowOpt = new ShowOptions();

	// Use this for initialization
	void Start () 
    {
        String strAdsID = "";

        if (false)
        {
            strAdsID = "1013015";
        }
        else
        {
            strAdsID = "1013016";
        }

        Debug.LogWarning(strAdsID);
        //Advertisement.Initialize(strAdsID, false);

        _ShowOpt.resultCallback = OnAdsShowResultCallBack;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnAdsShowResultCallBack(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.LogWarning("Add OK");
        }
    }

    public void PushResumeBtn()
    {
        m_GameSceneManager.PushPreStage();
    }

    public void PushExitBtn()
    {
        Time.timeScale = 1f;

        LoadLevelManager.Instance.LoadLebel("01_lobby");
    }

    public void PushAD()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(null, _ShowOpt);
        }
        else
        {
            Debug.LogWarning("No Ready");
        }
    }

    public void SetGameSceneManager(GameManager _gamesceneManager)
    {
        m_GameSceneManager = _gamesceneManager;
    }
}
