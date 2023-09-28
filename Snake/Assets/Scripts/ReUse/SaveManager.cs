using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int bestScore;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public PlayerInfo PlayerInfo;

    [DllImport("__Internal")] private static extern void SaveExtern(string date);
    [DllImport("__Internal")] private static extern void LoadExtern();

    [SerializeField] private string dateToUnlock;
    [SerializeField] private TMP_Text debug1, debug2;

    private bool isTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetIsTime();

#if !UNITY_EDITOR && UNITY_WEBGL
        LoadExtern();
#endif
    }

    // called from .jslib
    public void SetPlayerInfo(string value)
    {
        if (value == string.Empty)
        {
            return;
        }

        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);

        debug2.text = $"{JsonUtility.ToJson(PlayerInfo)}";
    }

    public void SaveBestScore(int bestScore)
    {
        PlayerInfo.bestScore = bestScore;
        Save();
    }

    public int GetBestScore()
    {
        return PlayerInfo.bestScore;
    }

    public void ClearData()
    {
        PlayerInfo.bestScore = 0;
        Save();
    }

    private void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
#if !UNITY_EDITOR && UNITY_WEBGL
        SaveExtern(jsonString);
#endif
        debug2.text = $"{JsonUtility.ToJson(PlayerInfo)}";
    }

    private void SetIsTime()
    {
        isTime = DateTime.Now >= DateTime.Parse(dateToUnlock); // time to show rewarded ads if now IS MORE than Date To Unlock
        debug1.text = $"Is time? = {isTime}. Date Now: {DateTime.Now}; dateToUnlock = {DateTime.Parse(dateToUnlock)}";
        GameManager.Instance.SetIsTimeForRewardedAds(isTime);
    }
}
