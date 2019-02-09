using UnityEngine;
using System.Collections;

public class Popup_Shop : MonoBehaviour
{
    public static readonly int SHOP_COUNT = 6;
    public GameObject m_unitPrefab;
    public UIGrid m_shopGrid;

    void Awake()
    {
        CreateUnit();
    }

    private void CreateUnit()
    {
        for (int i = 0; i < SHOP_COUNT; i++)
        {
            GameObject g = Instantiate(m_unitPrefab);
            g.GetComponent<PopUp_ShopUnit>().Init(i);
            g.name = string.Format("Unit{0:00}", i);
            g.transform.SetParent(m_shopGrid.transform, false);
        }
        m_shopGrid.Reposition();
    }

    public void Init()
    {

    }
    
    public void PushClose()
    {
        PopupManager.Instance.ClosePopup();
    }
}
