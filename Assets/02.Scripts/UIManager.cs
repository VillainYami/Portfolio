using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    //private PlayerUI pd;
    private DataManager dm;
    public TMP_Text hptxt;
    public Image hpBar;
    public TMP_Text exptxt;
    public Image expBar;
    public TMP_Text leveltxt;
    public TMP_Text nametxt;

    void Start()
    {
        //pd = GameObject.Find("PlayerData").GetComponent<PlayerUI>();
        dm = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    void Update()
    {
        SeeHP();
        SeeEXP();
        SeeLevelandName();
        dm.PlayerLevelUP(dm.nowPlayer.maxExp,dm.nowPlayer.curExp);
    }

    void SeeHP()
    {
        hpBar.fillAmount = dm.nowPlayer.curHp / dm.nowPlayer.maxHp;
        hptxt.text = $"HP {dm.nowPlayer.curHp}/{dm.nowPlayer.maxHp} ";
    }
    void SeeEXP()
    {
        expBar.fillAmount = dm.nowPlayer.curExp / dm.nowPlayer.maxExp;
        exptxt.text = $"EXP {dm.nowPlayer.curExp}/{dm.nowPlayer.maxExp}";
    }
    void SeeLevelandName()
    {
        leveltxt.text = $"LV   {dm.nowPlayer.level}";
        nametxt.text = $"{dm.nowPlayer.name}";
    }

    public void DataSaveBT()
    {
        dm.SaveData();
    }
}
