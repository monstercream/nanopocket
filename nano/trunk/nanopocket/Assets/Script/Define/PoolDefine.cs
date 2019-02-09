using UnityEngine;
using System.Collections;

public class PoolDefine : MonoBehaviour 
{
    public enum StageObjectT
    {
        WhiteBall = 0,
        Ball_1,
        Ball_2,
        Ball_3,
        Ball_4,
        Ball_5,
        Ball_6,
        Ball_7,
        Ball_8,
        Ball_9,
        Ball_10,
        Ball_11,
        Ball_12,
        Ball_13,
        Ball_14,
        Ball_15,
        Hall_1,
        Fever,
        MAX,
    }

    public static string[] m_StageObjectSrcPaths = 
	{
		// 0.
		"Prefab/Ball/PF_Ball_00",
		"Prefab/Ball/PF_Ball_01",
		"Prefab/Ball/PF_Ball_02",
		"Prefab/Ball/PF_Ball_03",
		"Prefab/Ball/PF_Ball_04",
        
        // 5.
		"Prefab/Ball/PF_Ball_05",
		"Prefab/Ball/PF_Ball_06",
		"Prefab/Ball/PF_Ball_07",
		"Prefab/Ball/PF_Ball_08",
		"Prefab/Ball/PF_Ball_09",

        // 10.
		"Prefab/Ball/PF_Ball_10",
		"Prefab/Ball/PF_Ball_11",
		"Prefab/Ball/PF_Ball_12",
		"Prefab/Ball/PF_Ball_13",
		"Prefab/Ball/PF_Ball_14",
        
        // 15
		"Prefab/Ball/PF_Ball_15",
        "Prefab/Hall/PF_Hall_00",
		"Prefab/Ball/PF_Ball_Fever",

    };

    public static int[] m_StageObjectPoolSizes = 
	{
		// 0.
		1,
        1,
        1,
        1,
        1,
        
        // 5.
        1,
        1,
        1,
        1,
        1,
        
        // 10.
        1,
        1,
        1,
        1,
        1,
              
        // 15  
        1,
        6,
        10,


    };
}
