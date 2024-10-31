using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCode
{
    Posion = 1,
    Weapon = 2,
    Gem = 3
}

public abstract class Item : MonoBehaviour
{
    public DataManager dm;
    public Sprite itemImage;
    public string itemName;
    public bool isGet = false;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !isGet)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isGet = true;
            }
        }
    }
}
