using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ClothItem", menuName = "ScriptableObjects/CreateClothItem")]
public class SI_Cloths : ScriptableObject
{
    [Serializable]
    public enum ClothTag
    {
        Null,
        Head,
        Torso,
        Pants
    }

    public ClothTag MyTag;
    public string ClothName;
    public float BuyingPrice;
    public float SellingPrice;
    public Sprite ClothSprite;

    public void Initialice(SI_Cloths _info)
    {
        MyTag = _info.MyTag;
        ClothName = _info.ClothName;
        BuyingPrice = _info.BuyingPrice;
        SellingPrice = _info.SellingPrice;
        ClothSprite = _info.ClothSprite;
    }
}
