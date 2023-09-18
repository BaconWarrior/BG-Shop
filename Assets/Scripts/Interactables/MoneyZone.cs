using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
                GameManager.Instance.playerInventory.GainMoney(100);
        }
    }
}
