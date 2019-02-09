using UnityEngine;
using System.Collections;

public class PopUp_ShopUnit : MonoBehaviour
{
    public UISprite m_itemIconSprite;
    public UILabel m_nameLabel;
    public UILabel m_countLabel;
    public UISprite m_buyIconSprite;
    public UILabel m_buyPriceLabel;

    private int m_index;

    public void Init(int index)
    {
        m_index = index;
        
        // TODO : ui 처리
    }    

    public void PushBuy()
    {
        Debug.Log("PushBuy");
    }
}
