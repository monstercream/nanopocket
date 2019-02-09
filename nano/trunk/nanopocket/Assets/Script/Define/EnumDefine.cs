using UnityEngine;
using System.Collections;

public class EnumDefine 
{
    public enum Nation
    {
        None,
        KO,
        JP,
        USA,
    }

    public enum PopupT
    {
        NONE = -1,
        COMMON,
        PAUSE,
        RESULT,
        STAGE,
        SHOP,
        SETTING,
        LOGIN,
    }

    public enum GameState
    {
        NONE = -1,
        READY,
        CONTROL,
        WAITING,
        DIE,
        RESET,
        END,
        PAUSE,
        RESUME
    }

    public enum RankT
    {
        None = -1,
        Rank_S,
        Rank_A,
        Rank_B,
        Rank_C,
        Rank_D,
        MAX,
    }

    public enum CommonPopupT
    {
        NONE = -1,
        TYPE1,
        TYPE2,
        TYPE3
    }
    
    public enum CameraState
    {
        READY = 0,
        PLAY,
        CONTROL,
        DIE
    }

    public enum AssetBundleT
    {
        NONE = -1,
        ITEM = 0,
        MAX
    }

    public enum ScoreT
    {
        NONE = -1,
        NORMAL,
        FEVER,
        PERFECT,
    }

    public enum PayT
    {
        NONE = -1,
        MONEY,
        DIAMOND,
        GOLD,
    }

    public enum ItemT
    {
        NONE = -1,
        DIAMOND,
        GOLD,
        HEART,
    }
}
