using UnityEngine;
using System.Collections;

public class LobbyStagePanel : MonoBehaviour
{
    public GameObject m_slotUnitPrefab;
    public UIGrid m_slotUnitGrid;

    private int m_index;
    private int m_level;

    public void Init(int index , int level)
    {
        m_index = index;
        m_level = level;
        CreateSlot();
    }

    private void CreateSlot()
    {
        for (int i = 0; i < GeneralDefine.SLOT_NUM; i++)
        {
            GameObject g = Instantiate(m_slotUnitPrefab);
            g.GetComponent<LobbyStageSlotUnit>().Init(i, m_level);
            g.name = string.Format("slotUnit{0:00}", i);
            g.transform.SetParent(m_slotUnitGrid.transform, false);
        }

        m_slotUnitGrid.Reposition();
    }
}