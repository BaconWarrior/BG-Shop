using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClothInfoContainer : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI clothName;
    [SerializeField] private TextMeshProUGUI clothPrice;
    [SerializeField] private SI_Cloths clothInfo;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private GameObject selectionImage;
    public Button myButton;
    
    public SI_Cloths ReturnCloth()
    {
        return clothInfo;
    }

    public void SelectionVisibility(bool _state)
    {
        selectionImage.SetActive(_state);
    }

    public void SelfDestruction()
    {
        Destroy(this.gameObject);
    }

    public void PlayHoverSound()
    {
        GameManager.Instance.PlaySound(hoverSound);
    }
    public void PlayClickSound()
    {
        GameManager.Instance.PlayClickSound();
    }

    public void UpdateInfo(SI_Cloths _newInfo, bool _selling)
    {
        clothInfo = _newInfo;
        iconImage.sprite = clothInfo.ClothSprite;
        clothName.text = clothInfo.ClothName;
        clothPrice.text = _selling ? clothInfo.SellingPrice.ToString() : clothInfo.BuyingPrice.ToString();
    }

    public void ClearInfo()
    {
        clothInfo = null;
        iconImage.sprite = null;
        clothName.text = null;
        clothPrice.text = null;
    }

    public bool CompareClothTag(SI_Cloths.ClothTag _tag)
    {
        return clothInfo.MyTag == _tag;
    }

}
