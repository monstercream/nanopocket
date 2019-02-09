using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public UILabel m_LabelScore = null;
    public UILabel m_LabelNotiGetScore = null;
    public UILabel m_LabelNotiPerfectGetScore = null;

    public int GetTotalScore
    {
        get { return m_iGoalScore; }
    }

    public int GetFeverScore
    {
        get { return m_iFeverScore; }
    }
    private int m_addScore = 1;

    private int m_iFeverScore = 0;
    private int m_iBonusScore = 0;

    private int m_iScore = 0;
    private int m_iGoalScore = 0;

    private void Init()
    {
        m_LabelScore.text = "SCORE : " + m_iScore.ToString("#,##0");
        m_LabelNotiGetScore.gameObject.SetActive(false);
        m_LabelNotiPerfectGetScore.gameObject.SetActive(false);
    }

    public void Reset()
    {
        m_iScore = 0;
        m_iGoalScore = 0;
        m_iFeverScore = 0;
        Init();
    }

    public void AddScore(int _iscore, EnumDefine.ScoreT _type)
    {
        if (_type == EnumDefine.ScoreT.NORMAL)
        {
            NotiGetScore(_iscore);
            m_iGoalScore = m_iGoalScore + _iscore;
        }
        else if (_type == EnumDefine.ScoreT.FEVER)
        {
            NotiGetScore(_iscore);
            m_iFeverScore += _iscore;
            m_iGoalScore = m_iGoalScore + _iscore;
        }
        else if (_type == EnumDefine.ScoreT.PERFECT)
        {
            NotiGetPerfectScore(_iscore);
            m_iBonusScore += _iscore;
            m_iGoalScore = m_iGoalScore + _iscore;
        }
    }
    protected void NotiGetPerfectScore(int _iScore)
    {
        if (m_LabelNotiPerfectGetScore.gameObject.activeSelf == false)
        {
            m_LabelNotiPerfectGetScore.gameObject.SetActive(true);
        }

        if (m_LabelNotiPerfectGetScore.GetComponent<Animation>().isPlaying == false)
        {
            m_LabelNotiPerfectGetScore.GetComponent<Animation>().Play();
        }

        m_LabelNotiPerfectGetScore.text = _iScore.ToString("#,##0");
    }

    protected void NotiGetScore(int _iScore)
    {
        if (m_LabelNotiGetScore.gameObject.activeSelf == false)
        {
            m_LabelNotiGetScore.gameObject.SetActive(true);
        }

        if (m_LabelNotiGetScore.GetComponent<Animation>().isPlaying == false)
        {
            m_LabelNotiGetScore.GetComponent<Animation>().Play();
        }

        m_LabelNotiGetScore.text = _iScore.ToString("#,##0");
    }

	// Update is called once per frame
	void Update () 
    {
        if (m_iScore != m_iGoalScore)
        {
            m_addScore++;
            m_iScore += m_addScore;

            if (m_iScore > m_iGoalScore)
            {
                m_iScore = m_iGoalScore;
                m_addScore = 1;
            }

            if (m_LabelScore.GetComponent<Animation>().isPlaying == false)
            {
                m_LabelScore.GetComponent<Animation>().Play();
            }

            m_LabelScore.text = "SCORE : " + m_iScore.ToString("#,##0");
        }
	}
}
