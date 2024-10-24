using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private PlayerUI pd;
    private DataManager dm;
    public TMP_Text hptxt;
    public Image hpBar;

    void Start()
    {
        pd = GameObject.Find("PlayerData").GetComponent<PlayerUI>();
        dm = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    void Update()
    {
        hpBar.fillAmount = dm.nowPlayer.curHp / dm.nowPlayer.maxHp;
        hptxt.text = $"{dm.nowPlayer.curHp}/{dm.nowPlayer.maxHp} ";
    }

}
