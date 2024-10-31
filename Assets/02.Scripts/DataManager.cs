using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData nowPlayer = new PlayerData();
    public string path;
    public int nowSlot;
    public class PlayerData
    {
        public string name;
        public int level = 1;
        public int coin = 1;
        public float maxExp = 100;
        public float curExp = 0;
        public float curHp = 200;
        public float maxHp = 200;
        public float moveSpeed = 6f;
        public float atkSpeed = 0.8f;
        public float damage = 15;

        public PlayerItem nowPlayerItem = new PlayerItem();
    }

    public class PlayerItem
    {
        public bool isGetSlot;
        public string itemName;
        public int itemCode;
        public int nowCount = 0;

    }

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
        path = Application.persistentDataPath + "/PlayerDataSave";
        print(path);
    }

    void Start()
    {
        
    }

    #region 데이터 세이브 and 로드
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        string itemdata = JsonUtility.ToJson(nowPlayer.nowPlayerItem);
        File.WriteAllText(path + nowSlot.ToString(), data + itemdata);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
    #endregion

    #region 플레이어 데이터 업데이트 관리
    public void PlayerLevelUP(float maxexp, float curexp)
    {
        if (curexp >= maxexp)
        {
            nowPlayer.curExp -= maxexp;
            nowPlayer.level++;
            nowPlayer.damage += 2;
            nowPlayer.maxHp += 10;
            nowPlayer.curHp += 10;
            nowPlayer.maxExp *= 1.3f;
        }
    }
    #endregion

}
