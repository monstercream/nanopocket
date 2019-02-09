using UnityEngine;
using System.Collections;

public class LoadLevelManager
{

    #region Singleton 
    private static LoadLevelManager _Instance = null;

    public static LoadLevelManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new LoadLevelManager();
            }

            return _Instance;
        }
    }
    #endregion

    public void LoadLebel(string str)
    {
        PopupManager.Instance.ClearPopup();
        Application.LoadLevel(str);
    }
}
