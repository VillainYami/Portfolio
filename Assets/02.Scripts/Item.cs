using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public Image itemImage;

    void Start()
    {

    }

    void Update()
    {
        if (itemImage != null)
        {
            SetColor(1);
        }
        else
        {
            SetColor(0);
        }
    }
    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
}
