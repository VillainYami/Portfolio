using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public UIManager ui;
    public DataManager dm;
    public float HP
    {
        get { return dm.nowPlayer.curHp; }
        set
        {
            dm.nowPlayer.curHp = value;
            ui.hpBar.fillAmount = dm.nowPlayer.curHp / dm.nowPlayer.maxHp;
            ui.hptxt.text = $"{dm.nowPlayer.curHp}";
        }
    }
    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        dm = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    void Update()
    {
        
    }
}
