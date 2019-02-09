using UnityEngine;
using System.Collections;

public class LobbyStageManager : MonoBehaviour
{
    public GameObject m_stagePanelPrefab;
    public UIGrid m_stageGrid;
    public UILabel m_levelLabel;
    public GameObject m_leftButton;
    public GameObject m_rightButton;
    public bool m_isMoveAni;
    public float m_moveDuration = 1.0f;
    public AnimationCurve m_animationCurve;

    private int m_currentStateNo;
    private int m_ClearStageNum;

    void Awake()
    {
        m_ClearStageNum =  DataManager.Instance.GetClearStage();

        CreateStagePanel();
        UpdateStage();
    }

    private void CreateStagePanel()
    {
        for(int i = 0; i < GeneralDefine.STAGE_NUM; i++)
        {
            GameObject g = Instantiate(m_stagePanelPrefab);
            g.GetComponent<LobbyStagePanel>().Init(i, i + 1 * (m_currentStateNo + 1));
            g.name = string.Format("stagePanel{0:00}", i * GeneralDefine.SLOT_NUM);
            g.transform.SetParent(m_stageGrid.transform, false);
        }

        m_stageGrid.Reposition();
    }
    
    public void OnClickRightButton()
    {
        if (IsMove(m_currentStateNo))
        {
            return;
        }

        m_currentStateNo++;
        if(m_currentStateNo >= GeneralDefine.STAGE_NUM)
        {
            m_currentStateNo = GeneralDefine.STAGE_NUM - 1;
            return;
        }
        
        UpdateStage();
    }

    public void OnClickLeftButton()
    {
        if (IsMove(m_currentStateNo))
        {
            return;
        }

        m_currentStateNo--;
        if (m_currentStateNo < 0)
        {
            m_currentStateNo = 0;
            return;
        }

        UpdateStage();
    }
    
    private void UpdateStage()
    {
        UpdateArrow();
        UpdateLabel();
        UpdatePosition();
    }

    private void UpdateArrow()
    {
        m_leftButton.SetActive(m_currentStateNo != 0);
        m_rightButton.SetActive(m_currentStateNo != GeneralDefine.STAGE_NUM - 1);
    }

    private void UpdateLabel()
    {
        m_levelLabel.text = m_currentStateNo + 1 + "/" + GeneralDefine.STAGE_NUM;
    }

    private void UpdatePosition()
    {
        if (m_isMoveAni)
        {
            TweenPosition tw = TweenPosition.Begin(
                gameObject,
                m_moveDuration,
                CurrentStageLocalPosition());
            tw.animationCurve = m_animationCurve;
        }
        else
        {
            transform.localPosition = CurrentStageLocalPosition();
        }
    }

    private Vector3 CurrentStageLocalPosition()
    {
        return StageLocalPosition(m_currentStateNo);
    }

    private Vector3 StageLocalPosition(int stageNo)
    {
        return new Vector3(
                    -GeneralDefine.ScreenWidth * stageNo,
                    transform.localPosition.y,
                    transform.localPosition.z);
    }

    private bool IsMove(int stageNo)
    {
        return transform.localPosition != StageLocalPosition(stageNo);
    }
}
