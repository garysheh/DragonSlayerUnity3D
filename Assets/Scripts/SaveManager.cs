using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SavePlayData();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlayData();
        }
    }

    public void SavePlayData()
    {
        Save(GameManager.Instance.playerStats.characterStats, GameManager.Instance.playerStats.characterStats.name);
    }

    public void LoadPlayData()
    {
        Load(GameManager.Instance.playerStats.characterStats, GameManager.Instance.playerStats.characterStats.name);
    }

    public void Save(Object data, string key)
    {
        var jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }

    public void Load(Object data, string key)
    {
        if(PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        }
    }
}
