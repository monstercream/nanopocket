using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Object_TableScrip : MonoBehaviour
{
    public MeshRenderer m_texTable = null;
    public Material[] m_matTable = null;

    public Transform[] m_PosBall = null;
    public Transform[] m_PosHall = null;
    public Transform[] m_PosFeverBall = null;

    private List<GameObject> m_ObjBallList = new List<GameObject>();
    private List<GameObject> m_ObjHallList = new List<GameObject>();
    private List<GameObject> m_ObjFeverBallList = new List<GameObject>();

    private GameManager m_GameManager;

    private int m_iStageID = 0;

    void Start()
    {
        SetFeverMode(false);
        CreateStageObject();

        m_GameManager.SetStageMaxBallCount(m_ObjBallList);
        m_GameManager.SetStageMaxFeverBallCount(m_ObjFeverBallList);
    }

    private void CreateStageObject()
    {
        for (int i = 0; i < m_PosBall.Length; i++)
        {
            GameObject objball = ObjectManager.IssueBall(i + 1);

            objball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            objball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            m_ObjBallList.Add(objball);
            objball.transform.localPosition = new Vector3(m_PosBall[i].position.x, 0.88f, m_PosBall[i].position.z);
        }
        
        for (int i = 0; i < m_PosHall.Length; i++)
        {
            GameObject objHall = ObjectManager.IssueBall((int)PoolDefine.StageObjectT.Hall_1);
            m_ObjHallList.Add(objHall);

            objHall.transform.localPosition = new Vector3(m_PosHall[i].position.x, 0.88f, m_PosHall[i].position.z);
        }
    }

    public void SetGameManager(GameManager _gamemanager)
    {
        m_GameManager = _gamemanager;
    }

    public void SetFeverMode(bool _IsFever)
    {
        if (_IsFever == true)
        {
            m_texTable.material = m_matTable[1];

            for (int i = 0; i < m_PosBall.Length; i++)
            {
                ObjectManager.Show(false, i + 1);
            }

            for (int i = 0; i < m_PosFeverBall.Length; i++)
            {
                GameObject objball = ObjectManager.IssueBall(17);

                objball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                objball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                m_ObjFeverBallList.Add(objball);
                objball.transform.localPosition = new Vector3(m_PosFeverBall[i].position.x, 0.88f, m_PosFeverBall[i].position.z);
            }
        }
        else
        {
            m_texTable.material = m_matTable[0];

            for (int i = 0; i < m_PosBall.Length; i++)
            {
                ObjectManager.Show(true, i + 1);
            }

            ObjectManager.Show(false, 17);
        }
    }

    void Update()
    {

    }

}
