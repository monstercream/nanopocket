using UnityEngine;
using System.Collections;

public class FeverGauge : MonoBehaviour 
{
    public UISprite m_sprGauge = null;

    float fGoalGauge = 0;
    float fCurrentGauge = 0;

    bool isGoal = false;

    void Awake()
    {
        m_sprGauge.fillAmount = 0;
    }

    public void SetGoalGauge(float _fgauge)
    {
        isGoal = true;
        fGoalGauge = _fgauge / GeneralDefine.MaxFever;
    }

    public void SetGauge(float _fgauge)
    {
        isGoal = false;

        m_sprGauge.fillAmount = _fgauge / GeneralDefine.MaxFever;
    }

    public void OnSetGaugeFinish()
    {
        isGoal = true;
        fCurrentGauge = 0;
        fGoalGauge = 0;
        m_sprGauge.fillAmount = 0;

    }

    //public void 

    void Update()
    {
        if (isGoal == true)
        {
            if (fCurrentGauge < fGoalGauge)
            {
                fCurrentGauge += 0.01f;

                if (fCurrentGauge > fGoalGauge)
                {
                    fCurrentGauge = fGoalGauge;
                }

                m_sprGauge.fillAmount = fCurrentGauge;
            }
        }
    }

}
