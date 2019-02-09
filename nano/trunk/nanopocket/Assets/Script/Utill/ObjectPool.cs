/*
 * Name: ObjectPool.cs
 * Comment: Object Pooling.
 * */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool
{
	private bool	   m_bInit = false;
	private GameObject m_BaseGameObj;
	private int        m_AllocNum;
	private int        m_ReAllocNum;

	private  List<GameObject> m_IdleList  = new List<GameObject>();
	private  List<GameObject> m_UsingList = new List<GameObject>();
	
	public ObjectPool()
	{
		m_bInit = false;
		m_AllocNum    = 0;
		m_ReAllocNum  = 0;
	}
	
	~ObjectPool()
	{
		m_IdleList.Clear();
		m_UsingList.Clear();

		m_BaseGameObj = null;
		m_IdleList    = null;
		m_UsingList   = null;
	}

	public bool IsInit() { return m_bInit; }
	public int GetAllocNum() { return m_AllocNum; }
	public int GetReAllocNum() { return m_ReAllocNum; }

	public void Init( GameObject baseobj, int allocnum, int reallocnum )
	{
		m_bInit = true;
		m_BaseGameObj = baseobj;
		m_AllocNum    = allocnum;
		m_ReAllocNum  = reallocnum;
		
		Alloc( m_AllocNum );
	}

	public void Clean()
	{
		for( int i = 0; i < m_UsingList.Count; ++ i )
			Object.DestroyImmediate( m_UsingList[i] );

		for( int i = 0; i < m_IdleList.Count; ++ i )
			Object.DestroyImmediate( m_IdleList[i] );
	}

    public void Show(bool _IsShow)
    {
        for (int i = 0; i < m_UsingList.Count; ++i)
            m_UsingList[i].SetActive(_IsShow);
    }

	public void Alloc( int allocnum )
	{
		for( int i = 0; i < allocnum; i++ )
		{
			GameObject obj = (GameObject)UnityEngine.Object.Instantiate( m_BaseGameObj, Vector3.zero, Quaternion.identity ) as GameObject;	
			
			if( obj == null )
			{
				Debug.LogError( "Alloc Error In ObjectPool:Alloc()" );
				return;
			}

            _BaseObject baseobj = obj.GetComponent<_BaseObject>();

			obj.SetActive( true );
            baseobj.Init(ObjectManager.GetUid());
            baseobj.Disable();
			obj.gameObject.SetActive( false );
		
			m_IdleList.Add( obj );
		}
	}
		
	public bool Push( GameObject pushobj )
	{
		bool bMatched = m_UsingList.Exists( delegate(GameObject obj){return obj == pushobj;} );	
			
		if( bMatched == true )
		{
			if( pushobj.activeSelf == false )
				pushobj.SetActive( true );

			pushobj.SendMessage( "Disable" );
			pushobj.gameObject.SetActive( false );
			m_IdleList.Add ( pushobj );
			m_UsingList.Remove( pushobj );
			
			return true;
		}

		Debug.LogError( "Push Error In ObjectPool:Push()" + pushobj.ToString() );
		return false;
	}
	
	public GameObject Pop()
	{
		if( m_IdleList.Count == 0 )
		{
			if( m_ReAllocNum == 0 )
			{
				if( m_UsingList.Count > 0 )
				{
					GameObject obj = m_UsingList[0];
					m_IdleList.Add ( obj );
					m_UsingList.Remove( obj );
				}
				else
				{
					Debug.LogError( "Pop Error In ObjectPool:Pop() - 1" );
				}
			}
			else
			{
				Alloc ( m_ReAllocNum );
			}
		}

		if( m_IdleList.Count <= 0 )
		{
			Debug.LogError( "Pop Error In ObjectPool:Pop() - 2" );
			return null;
		}
		
		GameObject popObj = m_IdleList[0];
		popObj.gameObject.SetActive( true );
		m_UsingList.Add ( popObj );
		m_IdleList.Remove ( popObj );
		return popObj;
	}
	
	public List<GameObject> GetUsingList()
	{
		return m_UsingList;
	}
}
