using UnityEngine;
using System.Collections;

public class LobbyMapPanel : MonoBehaviour 
{	
	public const float MAP_WIDTH = 1280.0f;

	public UIPanel m_scrollView;

	public void OnToggleValueChange()
	{
		bool val = UIToggle.current.value;

		if(val == true)
		{
			string name = UIToggle.current.gameObject.name;
			int chapter = int.Parse( name.Substring("Toggle_".Length) );

            m_scrollView.transform.localPosition = new Vector3((chapter * 1280) * -1, 0, 0);
            m_scrollView.clipOffset = new Vector2((chapter * 1280), 0);
//
//			Vector4 co = m_scrollView.clipOffset;
//			co.x = -(MAP_WIDTH) * chapter;
//			co.y = 0;
//			m_scrollView.clipOffset = co;
//
//			m_scrollView.transform.localPosition = new Vector3(co.x, co.y, 0);
		}	
	}
}
