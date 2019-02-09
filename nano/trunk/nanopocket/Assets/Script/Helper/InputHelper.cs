using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Input 을 공용으로 제어하기 위해 만듦

public class InputHelper
{
	public static bool IsPressed
	{
		get
		{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
			return Input.GetMouseButtonDown(0);
#else
            foreach (Touch t in Input.touches)
            {
                if(t.phase == TouchPhase.Began)
                    return true;
            }

            return false;
#endif
		}
	}

	public static bool IsPress
	{
		get
		{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
			return Input.GetMouseButton(0);
#else
            return Input.touchCount > 0;
#endif
		}
	}

	public static bool IsPressedBackButton
	{
		get
		{
			return Input.GetKeyDown(KeyCode.Escape);
		}
	}

    public static bool IsLeave
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            return Input.GetMouseButtonUp(0);
#else
            foreach (Touch t in Input.touches)
            {
                if(t.phase == TouchPhase.Ended)
                    return true;
            }

            return false;
#endif
        }
    }

	public static Vector3 TouchPos
	{
		get
		{
			if (!IsPress)
				return Vector3.zero;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
			return Input.mousePosition;
#else
            return Input.touches[0].position;
#endif
		}
	}

	public static Vector3 TouchPos01 // screen size x :0 ~ 1 , y : 0 ~ 1
	{
		get
		{
			Vector3 pos = TouchPos;
			pos.x = pos.x/Screen.width;
			pos.y = pos.y/Screen.height;

			return pos;
		}
	}

	public static Vector3[] TouchPosArray
	{
		get
		{
			List<Vector3> vPos = new List<Vector3>();
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
			vPos.Add(TouchPos);
#else
			foreach (Touch touch in Input.touches)
			{
				vPos.Add(touch.position);
			}
#endif
			return vPos.ToArray();
		}
	}

	public static Vector3 tilt
	{
		get
		{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
			Vector3 direction = Vector3.zero;
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				direction.x -= 0.6f;
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				direction.x += 0.6f;
			}
			
			if (Input.GetKey(KeyCode.UpArrow))
			{
				direction.y += 0.6f;
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				direction.y -= 0.6f;
			}

			return direction;
#else
			return Input.acceleration;
#endif
		}
	}
}
