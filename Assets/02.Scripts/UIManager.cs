using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private PlayerData pd;
    public TMP_Text hptxt;
    public Image hpBar;

    void Start()
    {
        pd = GameObject.Find("PlayerData").GetComponent<PlayerData>();
    }

    void Update()
    {
        hpBar.fillAmount = pd.curHp / pd.maxHp;
        hptxt.text = $"{pd.curHp}/{pd.maxHp} ";
    }

}
