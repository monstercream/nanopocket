using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public EnumDefine.GameState m_currentState;
    public EnumDefine.GameState m_preState;

    public GameObject m_objPause;
    public GameObject m_objCamera;
    public GameObject m_objUICamera;
    public GameObject m_objBallDirection;
    public GameObject m_objCueDirection;
    public GameObject m_objNotiFever;
    public GameObject m_objBonusBallTarget;

    public UILabel m_lavelBallCount;
    
    const float MAX_SHOTPOWER = 1500f;
    const float AUGMENTER_BALLPOW = 100f;

    private int m_iCurrentStageID = 0;
    private int m_iStageMaxBallCount = 0;
    private int m_iStageMaxFeverBallCount = 0;
    private int m_iDieBallCount =0;
    private int m_iFeverDieBallCount = 0;
    private float m_fStopMagnitude = 0.5f;
    private float m_fWaitingTime = 0.5f;
    private float m_fReadyTime = 1f;
    private float m_fBallDirection = 180f;
    private float m_fBallPower = 0;
    private float m_fCurrentBallDirection = 180f;
    private float m_fTime = 0;

    private GameObject m_objCrrentPlayer;
    private GameObject m_objTable;
    private GameObject m_objPlayer;


    /// <Test>

    public int m_iTestTableNum = 0;

    public GameObject[] m_objTestTable;
    
    /// </Test>
    public CameraManager m_CameraManager;
    public ScoreManager m_ScoreManager;
    public FeverGauge m_FeverGauge;

    delegate void GameProcessDelegate();

    GameProcessDelegate CurrentProcess;
    private bool m_IsControl = false;
    private bool m_IsFever = false;

    private float m_fCurFever = 0;
    private List<GameObject> m_objBallList;
    private List<GameObject> m_objFeverBallList;
    private GameObject m_CurrentBonusBall;
    private bool m_IsUseItem = false;

    private bool m_IsBallSkill = false;

    public float GetBallPower()
    {
        return m_fBallPower;
    }

    void Awake()
    {
		Application.targetFrameRate = 60;
    }

    void Start()
    {
        m_objNotiFever.SetActive(false);
        m_iTestTableNum = 0;

        Init();
        StageStart(m_iTestTableNum);
    }

    private void Init()
    {
        ObjectManager.InitInGame();
        StageManager.instance.Init();
        SkillManager.instance.Init();
    }

    public void PushNextStage()
    {
        if (m_iTestTableNum < m_objTestTable.Length - 1)
        {
            m_iTestTableNum++;
        }
        else
        {
            m_iTestTableNum = 0;
        }

        StageStart(m_iTestTableNum);
        SetState(EnumDefine.GameState.RESET);
    }
    public void PushPreStage()
    {
        PushPause(m_currentState);
        //DeadBall();
        /*
        if (m_iCurrentStageID - 1 >= 0)
        {
            StageStart(m_iCurrentStageID - 1);
        }
         */ 
    }

    private void PushPause(EnumDefine.GameState state)
    {
        if (state == EnumDefine.GameState.PAUSE)
        {
            SetState(EnumDefine.GameState.RESUME);
            PopupManager.Instance.ClosePopup();
            Time.timeScale = 1;
        }
        else
        {
            SetState(EnumDefine.GameState.PAUSE);
            GameObject common = PopupManager.Instance.MakePopup(EnumDefine.PopupT.PAUSE);
            common.GetComponent<Popup_Pause>().SetGameSceneManager(this);
            Time.timeScale = 0;
        }
    }

    private void SelectBonusBall()
    {
        if (m_objBallList.Count != 0)
        {
            int irand = UnityEngine.Random.Range(0, m_objBallList.Count);
            m_CurrentBonusBall = m_objBallList[irand];
        }
    }

    private void StageStart(int istage)
    {
        m_ScoreManager.Reset();
        m_iCurrentStageID = istage;
        m_fBallPower = 0f;

        MakeTable(istage);
        MakePlayerBall();
        SetState(EnumDefine.GameState.READY);
        StopPlayerBall();
    }

    private void MakeTable(int istage)
    {
        if (m_objTable != null)
        {
            Destroy(m_objTable);
            m_objTable = null;
        }

        m_objTable = (GameObject)UnityEngine.Object.Instantiate(m_objTestTable[m_iCurrentStageID]);
        m_objTable.GetComponent<Object_TableScrip>().SetGameManager(this);
        m_objTable.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void MakePlayerBall()
    {
        m_objPlayer = ObjectManager.IssueBall(0);
        m_objPlayer.transform.localPosition = new Vector3(0, 0.88f, -5f);

        SetPlayer(m_objPlayer);
    }

    private void DeadBall()
    {
        ObjectManager.PayBackStageObject(0, m_objPlayer);
    }

    void Update()
    {
        if (CurrentProcess != null)
        {
            CurrentProcess();
        }

        if( m_IsFever == true)
        {
            FeverProcess();
        }
    }

    void FeverProcess()
    {
        m_fCurFever -= Time.deltaTime;

        m_FeverGauge.SetGauge(m_fCurFever);

        if (m_fCurFever <= 0)
        {
            m_FeverGauge.OnSetGaugeFinish();
            m_fCurFever = 0;
            m_IsFever = false;
            OnFever(false);
        }
    }


    public void SetPlayer(GameObject player)
    {
        m_objCrrentPlayer = player; 
        m_CameraManager.SetCameraTarget(player);
    }

    public void SetStageMaxBallCount(List<GameObject> _List)
    {
        m_iStageMaxBallCount = _List.Count;
        m_objBallList = _List;

    }

    public void SetStageMaxFeverBallCount(List<GameObject> _List)
    {
        m_iStageMaxFeverBallCount = _List.Count;
        m_objFeverBallList = _List;
    }

    public void SetSkill(bool _bskill)
    {
        m_IsBallSkill = _bskill;
    }

    public void SetDieBall(GameObject objBall, int iid, bool _bFever = true)
    {
        bool IsBonus = false;

        if (m_currentState == EnumDefine.GameState.END)
            return;

        if (m_CurrentBonusBall != null)
        {
            if (m_CurrentBonusBall.GetComponent<Object_BallScript>().GetID() == iid)
            {
                IsBonus = true;
                SelectBonusBall();
            }
        }

        if (PoolDefine.StageObjectT.WhiteBall == (PoolDefine.StageObjectT)iid)
        {
            // Ball Is Player Ball
            if(m_IsBallSkill == true)
                return;

            SetState(EnumDefine.GameState.DIE);
        }
        else if(PoolDefine.StageObjectT.Fever == (PoolDefine.StageObjectT)iid)
        {
            if (m_iFeverDieBallCount >= m_iStageMaxFeverBallCount)
            {
                Debug.LogWarning("PerfectBall");
                m_iFeverDieBallCount = 0;
                m_ScoreManager.AddScore(GeneralDefine.PerfectFeverBallScore, EnumDefine.ScoreT.PERFECT);
            }
            else
            {
                Debug.LogWarning("FeverBall");
                m_objFeverBallList.Remove(objBall);
                m_ScoreManager.AddScore(GeneralDefine.FeverBallScore, EnumDefine.ScoreT.FEVER);
                m_iFeverDieBallCount++;
            }
        }
        else
        {
            m_objBallList.Remove(objBall);
            m_iDieBallCount++;

            if (IsBonus == true)
            {
                Debug.LogWarning("BonusBall");
                m_ScoreManager.AddScore(GeneralDefine.BonusBallScore, EnumDefine.ScoreT.NORMAL);

                if(_bFever == true)
                {
                    AddFever(GeneralDefine.AddBonusFever);
                }
            }
            else
            {
                Debug.LogWarning("NormalBall");
                m_ScoreManager.AddScore(GeneralDefine.NormalBallScore, EnumDefine.ScoreT.NORMAL);
                if (_bFever == true)
                {
                    AddFever(GeneralDefine.AddFever);
                }
            }

            if (m_iDieBallCount >= m_iStageMaxBallCount)
            {
                m_iDieBallCount = 0;
                SetState(EnumDefine.GameState.END);
            }
        }

        ObjectManager.PayBackStageObject(iid, objBall);
    }

    private void Result()
    {
        GameObject obj = PopupManager.Instance.MakePopup(EnumDefine.PopupT.RESULT);
        Popup_Result sc = obj.GetComponent<Popup_Result>();
        sc.SetTotalScroe(m_ScoreManager.GetTotalScore);
        sc.SetFeverScroe(m_ScoreManager.GetFeverScore);
        sc.SetGameManager(this);
        m_objPause.SetActive(false);
    }

    public void Restart()
    {
        Debug.LogWarning("!!");
        PopupManager.Instance.ClosePopup();
        StageStart(m_iTestTableNum);
        SetState(EnumDefine.GameState.RESET);
    }

    public void Exit()
    {
        Debug.LogWarning("!");
        PopupManager.Instance.ClosePopup();
        LoadLevelManager.Instance.LoadLebel("01_lobby");
    }


    private void OnFever(bool _IsFever)
    {
        m_objNotiFever.SetActive(_IsFever);
        m_objTable.GetComponent<Object_TableScrip>().SetFeverMode(_IsFever);
        StopPlayerBall();
        DeadBall();
        MakePlayerBall();
        StopPlayerBall();

        if(_IsFever == false)
        {
            Debug.LogWarning("!!");
            SelectBonusBall();
            m_iFeverDieBallCount = 0;
        }
    }

    private void AddFever(float _faddpoint)
    {
        if (m_IsFever == false)
        {
            m_fCurFever += _faddpoint;
            
            m_FeverGauge.SetGoalGauge(m_fCurFever);

            if (m_fCurFever >= GeneralDefine.MaxFever)
            {
                m_IsFever = true;

                OnFever(m_IsFever);
            }
        }
    }

    private void StopPlayerBall()
    {
        m_objCrrentPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_objCrrentPlayer.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private void ShowBallDirection(bool IsShow)
    {
        m_objBallDirection.SetActive(IsShow);
        m_objCueDirection.SetActive(IsShow);

        if (m_IsFever == true)
        {
            m_objBonusBallTarget.SetActive(false);
        }
        else
        {
            m_objBonusBallTarget.SetActive(IsShow);
        }
    }

    private void SetState(EnumDefine.GameState state)
    {
        if (m_currentState != EnumDefine.GameState.PAUSE && m_currentState != EnumDefine.GameState.RESUME)
        {
            m_preState = m_currentState;
        }

        m_currentState = state;

        switch (state)
        {
            case EnumDefine.GameState.READY:
                Debug.Log("READY Start");
                ShowBallDirection(false);
                CurrentProcess = ReadyProcess;
                break;

            case EnumDefine.GameState.CONTROL:
                Debug.Log("CONTROL Start");
                m_CameraManager.ChangeCameraState(EnumDefine.CameraState.PLAY);
                CurrentProcess = ControlProcess;
                SelectBonusBall();
                ShowBallDirection(true);
                break;
            case EnumDefine.GameState.DIE:
                Debug.Log("Die Start");
                //Show Direction
                CurrentProcess = DieProcess;
                break;

            case EnumDefine.GameState.WAITING:
                Debug.Log("WAITING Start");
                m_CameraManager.ChangeCameraState(EnumDefine.CameraState.CONTROL);
                ShowBallDirection(false);
                CurrentProcess = WaitingProcess;
                break;
            case EnumDefine.GameState.RESET:
                Debug.Log("RESET Start");
                MakePlayerBall();

                CurrentProcess = ReadyProcess;
                break;
            case EnumDefine.GameState.END:
                Debug.Log("END Start");
                Result();
                CurrentProcess = EndProcess;
                break;
            case EnumDefine.GameState.PAUSE:
                Debug.Log("PAUSE Start");
                CurrentProcess = PauseProcess;
                break;
            case EnumDefine.GameState.RESUME:
                Debug.Log("RESUM Start");
                CurrentProcess = ResumeProcess;
                break;
        }
    }

    private void ResetProcess()
    {
        Debug.Log("Reset End");
        m_CameraManager.ChangeCameraState(EnumDefine.CameraState.READY);
    }

    private void ReadyProcess()
    {
        m_CameraManager.ChangeCameraState(EnumDefine.CameraState.PLAY);

        m_fTime += Time.deltaTime;

        if (m_fTime >= m_fReadyTime)
        {
            Debug.Log("Ready End");
            m_fTime = 0;
            SetState(EnumDefine.GameState.CONTROL);
        }
    }
    private void DieProcess()
    {
        m_CameraManager.ChangeCameraState(EnumDefine.CameraState.DIE);
        SetState(EnumDefine.GameState.RESET);
        StopPlayerBall();
    }

    private void ControlProcess()
    {
        GameInput();
        ArrowDirection();
    }

    private void WaitingProcess()
    {
        m_fTime += Time.deltaTime;

        if (m_fTime >= m_fWaitingTime)
        {
            if (m_objCrrentPlayer.GetComponent<Rigidbody>().velocity.magnitude <= m_fStopMagnitude)
            {
                StopPlayerBall();
                
                Debug.Log("Waiting End");
                SetState(EnumDefine.GameState.READY);
            }
        }
    }
        
    private void EndProcess()
    {
    }

    private void PauseProcess()
    {
    }

    private void ResumeProcess()
    {
        SetState(m_preState);
    }

    private void GameInput()
    {
        if (Input.acceleration.z < -1.5f || Input.acceleration.z > 1.5f || Input.acceleration.y < -1.5f || Input.acceleration.y > 1.5f)
        {
            PushPreStage();
        }

        // 마우스를 누른 상태
       if (InputHelper.IsPress)
        {
            if (m_IsControl == false)
            {
                Ray uiray = m_objUICamera.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hituiobj;

                if (Physics.Raycast(uiray, out hituiobj, Mathf.Infinity))
                {
                    if (hituiobj.transform.name == "UI")
                    {
                        return;
                    }
                }
            }

            m_IsControl = true;
           
            Ray ray = m_objCamera.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hitobj;

            if (Physics.Raycast(ray, out hitobj, Mathf.Infinity))
            {
                Vector3 direction;

                float fPow;
                direction = (hitobj.point - m_objCrrentPlayer.transform.position).normalized;

                m_fBallDirection = Quaternion.LookRotation(direction).eulerAngles.y;
                fPow = Vector3.Distance(hitobj.point, m_objCrrentPlayer.transform.position) * AUGMENTER_BALLPOW;

                m_fBallPower = Mathf.Min(fPow, MAX_SHOTPOWER);
            }
        }

        //마우스를 땐 상태
        if (InputHelper.IsLeave)
        {
            m_IsControl = false;

            if (m_fBallPower != 0)
            {
                Debug.Log("Control End");
                SetState(EnumDefine.GameState.WAITING);
                NPCMoving(m_fBallPower);
            }

            m_fBallPower = 0;
        }
    }

    private void NPCMoving(float fBallPow)
    {
        Quaternion QuaBallDirection = Quaternion.Euler(0, m_fBallDirection + 180f, 0);
        Vector3 posBallForce = QuaBallDirection * Vector3.forward * fBallPow;
        m_objCrrentPlayer.GetComponent<Rigidbody>().AddForce(posBallForce);
        m_fBallPower = 0f;
    }

    private void ArrowDirection()
    {
        Quaternion quaCrrentBallDirection = Quaternion.Euler(0, m_fBallDirection + 180f, 0);
        m_fCurrentBallDirection = m_fBallDirection;

        m_objCueDirection.transform.localPosition = m_objPlayer.transform.localPosition;
        m_objCueDirection.transform.localRotation = quaCrrentBallDirection;

        m_objBallDirection.transform.localScale = new Vector3(1f, 1f, ((m_fBallPower) / 300f) + 1);
        m_objBallDirection.transform.localPosition = m_objPlayer.transform.localPosition;
        m_objBallDirection.transform.localRotation = quaCrrentBallDirection;

        Vector3 ballpos = new Vector3(m_CurrentBonusBall.transform.localPosition.x, 1.5f, m_CurrentBonusBall.transform.localPosition.z);
        m_objBonusBallTarget.transform.localPosition = ballpos;
    }


    public void PushTestBtn()
    {
        m_IsUseItem = !m_IsUseItem;
        Debug.LogWarning("skill" + m_IsUseItem);
        Object_PlayerBallScript playersc = m_objPlayer.GetComponent<Object_PlayerBallScript>();
        playersc.SetGameManager(this);

        playersc.SkillDestroyBall(10);

        //skilldestroyBall(2.5f);
        //SkillBomb(10f);
        //SkillBigBall(1.4f);
    }

    void OnDrawGizmos()
    {
        if(m_objPlayer != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(m_objPlayer.transform.position, 4f);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_objPlayer.transform.position, 1f);
        }
    }

    /*
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100f, 100f), "skill" + m_IsUseItem))
        {
        }
    }*/
}
