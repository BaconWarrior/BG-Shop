using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Btn_ClothShop : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI clothName;
    [SerializeField] private TextMeshProUGUI clothPrice;
    [SerializeField] private SI_Cloths clothInfo;
    public Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateInfo(SI_Cloths _newInfo)
    {
        clothInfo = _newInfo;
        iconImage.sprite = clothInfo.ClothSprite;
        clothName.text = clothInfo.ClothName;
        clothPrice.text = clothInfo.BuyingPrice.ToString();
    }

    public bool CompareClothTag(SI_Cloths.ClothTag _tag)
    {
        return clothInfo.MyTag == _tag;
    }
}
