using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataDefine : MonoBehaviour
{
    public class UserDataInfo
    {
        public int m_iUID = 0;                     // 유저의 아이디
        public string m_strName = "";              // 유저의 닉네임
        public int m_iLV = 0;                      // 레벨
        public int m_iExp = 0;                     // 경험치
        public int m_iHeart = 0;                   // 게임을 플레이 할수있는 하트값
        public int m_iCash = 0;                    // 현금으로 충전가능한 포인트
        public int m_iCoin = 0;                    // 게임 포인트
        public List<StageData> m_StageInfoList = null; // 유저가 클리어 스테이지 정보
    }

    public class SkillData
    {
        public int ID = 0;
        public string NAME = "";
        public string DESC = "";
        public float VALUE = 0;
    }

    public class ShopData
    {
        public int ID = 0;
        public EnumDefine.ItemT ItemType = EnumDefine.ItemT.NONE;
        public EnumDefine.PayT payType = EnumDefine.PayT.NONE;
        public string NAME = "";
        public string DESC = "";
        public string SpriteName = "";
        public string ItemCount = "";
        public float PRICE = 0;

        public override string ToString()
        {
            return "id : " + ID + " itemType : " + ItemType + " payKey : " + payType +
                " name : " + NAME + " desc : " + DESC + " spriteName : " + SpriteName +
                " itemCount : " + ItemCount + " price : " + PRICE;
        }
    }

    public class StageData
    {
        public int ID = 0;                                    // 스테이지 아이디
        public EnumDefine.RankT Rank = EnumDefine.RankT.None; // 스테이지 랭크
        public int HighScore = 0;                             // 최고 득점
        public int StarCount = 0;                             // 획득한 별이 개수
        public bool IsClear = false;
        public int BallNum = 1;
    }

    public class FriendDataInfo
    {
        private int m_iUID = 0;
        private int m_iLV = 0;
    }

    public class Localize
    {
        public int Id;
        public string Key;
        public string Ko;
        public string En;
    }
}
