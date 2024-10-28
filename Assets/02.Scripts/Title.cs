using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Title : MonoBehaviour
{
    public GameObject creat;	
    public TMP_Text[] slotText;		
    public TMP_Text newPlayerName;
    public GameObject panel;
    bool[] savefile = new bool[4];
    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단.
        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))	// 데이터가 있는 경우
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                slotText[i].text = DataManager.instance.nowPlayer.name;
            }
            else
            {
                slotText[i].text = "Empty";
            }
        }
        DataManager.instance.DataClear();
    }
    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;	// 슬롯의 번호를 슬롯번호로 입력함.

        if (savefile[number])
        {
            DataManager.instance.LoadData();	// 데이터를 로드하고
            GoGame();
        }
        else
        {
            Creat();
        }
    }

    public void Creat()	// 플레이어 닉네임 입력 UI를 활성화하는 메소드
    {
        creat.SetActive(true);
    }
    public void CreatCancelBT()
    {
        creat.SetActive(false);
    }

    public void GoGame()	// 게임씬으로 이동
    {
        if (!savefile[DataManager.instance.nowSlot])	// 현재 슬롯번호의 데이터가 없다면
        {
            if (newPlayerName == null) return;

            else
            {
                DataManager.instance.nowPlayer.name = newPlayerName.text; // 입력한 이름을 복사해옴
                DataManager.instance.SaveData(); // 현재 정보를 저장함.
            }
        }
        SceneManager.LoadScene(1);
    }

    public void StartBT()
    {
        panel.SetActive(true);
    }

    public void LoadBT()
    {
        panel.SetActive(true);
    }

    public void OpsionsBT()
    {

    }
}
