using UnityEngine;
using System.Collections;

public class _BaseObject : MonoBehaviour
{
    protected int m_Id;
    protected int m_Uid;

    public virtual void Init(int uid)
    {
        m_Uid = uid;
    }

    public void SetId(int id)
    {
        m_Id = id;
    }

    public virtual void Disable()
    {

    }
}
