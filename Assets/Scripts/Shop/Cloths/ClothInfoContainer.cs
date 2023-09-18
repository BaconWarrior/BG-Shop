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
    public Button myButton;
    
    public SI_Cloths ReturnCloth()
    {
        return clothInfo;
    }

    public void SelfDestruction()
    {
        Destroy(this.gameObject);
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
