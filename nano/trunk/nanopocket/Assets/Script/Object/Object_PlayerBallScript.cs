using UnityEngine;
using System.Collections;

public class Object_PlayerBallScript : Object_BallScript
{
    public GameManager m_GameManager = null;
    private bool m_IsActiveSkill = false;

    private float m_fSkillTime;
    private string m_CurWell;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (m_IsActiveSkill == true)
        {
            if (0 < m_fSkillTime)
            {
                Debug.LogWarning(m_fSkillTime);
                m_fSkillTime -= Time.deltaTime;
                Vector3 explosionForce = transform.position;
                Collider[] colliding = Physics.OverlapSphere(explosionForce, 1f);

                foreach (Collider hit in colliding)
                {
                    /*
                    if (hit.transform.tag == "Ball")
                    {
                        m_GameManager.SetDieBall(hit.gameObject, hit.GetComponent<Object_BallScript>().GetID(),false);
                    }*/

                    if (hit.name != m_CurWell)
                    {
                        if (hit.transform.tag == "Well" || hit.transform.tag == "Ball")
                        {
                            m_CurWell = hit.name;
                            Debug.LogWarning("!!" + hit.name);
                            Vector3 acc = gameObject.GetComponent<Rigidbody>().velocity * 2f;
                            gameObject.GetComponent<Rigidbody>().AddForce(acc);
                        }
                    }
                }
            }
            else if(0 > m_fSkillTime)
            {
                m_fSkillTime = 0;
                m_IsActiveSkill = false;
                m_GameManager.SetSkill(false);
            }
        }
    }

    public void SetGameManager(GameManager _manager)
    {
        m_GameManager = _manager;
    }

    public void SkillDestroyBall(float _ftime)
    {
        if (m_IsActiveSkill == true)
            return;

        m_fSkillTime = _ftime;
        m_IsActiveSkill = true;
        m_GameManager.SetSkill(true);
    }

    public void SkillBomb(float _fBombpow)
    {
        //m_CameraManager.CameraShake(0.2f);

        Vector3 explosionForce = transform.position;
        Collider[] colliding = Physics.OverlapSphere(explosionForce, 4f);

        foreach (Collider hit in colliding)
        {
            if (hit.GetComponent<Object_BallScript>() != null && hit.GetComponent<Object_BallScript>().GetID() != 0)
            {
                Debug.LogWarning(hit.name);
                hit.GetComponent<Rigidbody>().AddExplosionForce(1000.0f, explosionForce, _fBombpow);
            }
        }
    }

    public void SkillBigBall(float _fBallSize)
    {
        //m_CameraManager.CameraShake(0.2f);
        transform.localScale = new Vector3(_fBallSize, _fBallSize, _fBallSize);
    }

    public void SetSkill(int _iID)
    {
        DataDefine.SkillData sdata = SkillManager.instance.GetInfo(_iID);
        Debug.LogWarning(sdata.ID);
    }
}
