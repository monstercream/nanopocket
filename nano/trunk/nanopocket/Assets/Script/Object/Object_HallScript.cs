using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Object_HallScript : _BaseObject
{
    private GameManager m_GameManager = null;
    public ParticleSystem[] m_parGoalParticle = null;

    void Awake()
    {
        GameObject findObj = GameObject.Find("_GamesceneManager");

        if (findObj != null)
        {
            m_GameManager = findObj.GetComponent<GameManager>();
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        GameObject objBall = col.gameObject;
        Vector3 posBall = objBall.transform.position;
        Object_BallScript bs = objBall.GetComponent<Object_BallScript>();

        if (bs != null)
        {
            if (col.GetComponent<Rigidbody>().velocity.magnitude < 10f)
            {
                if (Vector3.Distance(posBall, gameObject.transform.position) < 1.7f)
                {
                    m_GameManager.SetDieBall(col.gameObject, bs.GetID());

                    for (int i = 0; i < m_parGoalParticle.Length; i++)
                    {
                        if (m_parGoalParticle[i].isPlaying == false)
                            m_parGoalParticle[i].Play();
                    }
                }
            }
            else
            {
                if (Vector3.Distance(posBall, gameObject.transform.position) < 3f)
                {
                    m_GameManager.SetDieBall(col.gameObject, bs.GetID());

                    for (int i = 0; i < m_parGoalParticle.Length; i++)
                    {
                        if (m_parGoalParticle[i].isPlaying == false)
                            m_parGoalParticle[i].Play();
                    }
                }
            }
        }

        
        /*
        Object_BallScript bs = other.gameObject.GetComponent<Object_BallScript>();

        m_GameManager.SetDieBall(other.gameObject, bs.GetID());

        for (int i = 0; i < m_parGoalParticle.Length; i++)
        {
            if (m_parGoalParticle[i].isPlaying == false)
                m_parGoalParticle[i].Play();
        }
         */
    }
}
