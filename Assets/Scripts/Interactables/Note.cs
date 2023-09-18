using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    public void ToggleVisibility()
    {
        canvas.SetActive(!canvas.activeInHierarchy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.playerController.OnAction.AddListener(ToggleVisibility);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.playerController.OnAction.RemoveListener(ToggleVisibility);
        }
    }
}
