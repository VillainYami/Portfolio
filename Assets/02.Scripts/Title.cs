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
        // ���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�.
        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))	// �����Ͱ� �ִ� ���
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
        DataManager.instance.nowSlot = number;	// ������ ��ȣ�� ���Թ�ȣ�� �Է���.

        if (savefile[number])
        {
            DataManager.instance.LoadData();	// �����͸� �ε��ϰ�
            GoGame();
        }
        else
        {
            Creat();
        }
    }

    public void Creat()	// �÷��̾� �г��� �Է� UI�� Ȱ��ȭ�ϴ� �޼ҵ�
    {
        creat.SetActive(true);
    }
    public void CreatCancelBT()
    {
        creat.SetActive(false);
    }

    public void GoGame()	// ���Ӿ����� �̵�
    {
        if (!savefile[DataManager.instance.nowSlot])	// ���� ���Թ�ȣ�� �����Ͱ� ���ٸ�
        {
            if (newPlayerName == null) return;

            else
            {
                DataManager.instance.nowPlayer.name = newPlayerName.text; // �Է��� �̸��� �����ؿ�
                DataManager.instance.SaveData(); // ���� ������ ������.
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
