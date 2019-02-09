using UnityEngine;
using System.Collections;

public static class ObjectManager
{
    private static GameObject[] m_StageObjectSources;
    private static ObjectPool[] m_StageObjectLists;

    // Unique ID.
    private static int m_UidCount;
    public static int GetUid() { return ++m_UidCount; }
        
    public static void Clean()
    {
        if (m_StageObjectLists != null)
        {
            for (int i = 0; i < m_StageObjectLists.Length; ++i)
            {
                m_StageObjectLists[i].Clean();
                m_StageObjectLists[i] = null;
            }
        }
        m_StageObjectLists = null;
    }
    
    public static void Show(bool _IsShow, int _ID)
    {
        m_StageObjectLists[_ID].Show(_IsShow);
    }

    public static void InitInGame()
    {
        LoadStageObjectSources();
        InitStageObject();
    }

    #region MemoryPool_StageObject

    private static void LoadStageObjectSources()
	{
        m_StageObjectSources = new GameObject[PoolDefine.m_StageObjectSrcPaths.Length];

        for (int i = 0; i < m_StageObjectSources.Length; ++i)
            m_StageObjectSources[i] = Resources.Load(PoolDefine.m_StageObjectSrcPaths[i], typeof(GameObject)) as GameObject;		
	}

    public static void InitStageObject()
    {
        m_StageObjectLists = new ObjectPool[m_StageObjectSources.Length];
        for (int i = 0; i < m_StageObjectLists.Length; ++i)
            m_StageObjectLists[i] = new ObjectPool();
    }

    public static GameObject IssueBall(int iId)
    {
        int srcIdx = iId;

        if (srcIdx >= m_StageObjectSources.Length)
        {
            Debug.LogError("Not Found srcIdx: " + srcIdx.ToString());
            return null;
        }

        GameObject res = null;

        if (m_StageObjectLists[srcIdx].IsInit() == false)
        {
            string path = "Assets/Resources/Prefab/" + PoolDefine.m_StageObjectSrcPaths[srcIdx] + ".prefab";

            m_StageObjectSources[srcIdx] = Resources.Load(PoolDefine.m_StageObjectSrcPaths[srcIdx], typeof(GameObject)) as GameObject;
      
            m_StageObjectLists[srcIdx].Init(m_StageObjectSources[srcIdx], PoolDefine.m_StageObjectPoolSizes[srcIdx], 0);
        }

		res = m_StageObjectLists[srcIdx].Pop();

		if( res == null )
			Debug.LogError( "In use all Object: " + srcIdx.ToString() );
		
		res.SendMessage( "SetId", iId );

        return res;
    }

    public static void PayBackStageObject(int iballId, GameObject pbObj)
    {
        int srcIdx = iballId;

        if (srcIdx >= m_StageObjectSources.Length)
        {
            Debug.LogError("Not Found srcIdx: " + srcIdx.ToString());
            return;
        }

        pbObj.transform.parent = null;

        bool result = m_StageObjectLists[srcIdx].Push(pbObj);

        if (result == false)
            Debug.LogError("Error PayBackBall : " + srcIdx.ToString());
    }

#endregion

}
