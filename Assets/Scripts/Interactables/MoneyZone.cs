using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyZone : MonoBehaviour
{
    [SerializeField] private AudioClip goldSound;
    void GiveMoneyToPlayer()
    {
        GameManager.Instance.PlaySound(goldSound);
        GameManager.Instance.playerInventory.GainMoney(100);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.playerController.OnAction.AddListener(GiveMoneyToPlayer);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.playerController.OnAction.RemoveListener(GiveMoneyToPlayer);
        }
    }

}
