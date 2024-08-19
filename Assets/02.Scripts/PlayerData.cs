using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public UIManager ui;
    public float curHp = 200;
    public float maxHp = 200;
    public float HP
    {
        get { return curHp; }
        set
        {
            curHp = value;
            ui.hpBar.fillAmount = curHp / maxHp;
            ui.hptxt.text = $"{curHp}";
        }
    }
    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {
        
    }
}
