using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<SI_Cloths> playerCloths;
    [SerializeField] private int playerMoney;
    private int PlayerMoney
    {
        get => playerMoney;
        set
        {
            playerMoney = value;
            myInventory.SetGoldText(value.ToString());
        }
    }
    [SerializeField] private GameObject containerPrefab;
    [SerializeField] private InventoryUI myInventory;

    [SerializeField] private SpriteRenderer headSprite;
    [SerializeField] private SpriteRenderer torsoSprite;
    [SerializeField] private SpriteRenderer pantsSprite;

    [SerializeField] private Sprite defaultHeadSprite;
    [SerializeField] private Sprite defaultTorsoSprite;
    [SerializeField] private Sprite defaultPantsSprite;

    [SerializeField] AudioClip dressSound;
    [SerializeField] AudioClip undressSound;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.playerInventory = this;
        PlayerMoney = 1000;
    }


    public void GainMoney(int _amount)
    {
        Mathf.Clamp(PlayerMoney += _amount, 0, 1000);
    }
    

    public void DressCloth()
    {
        if (myInventory.currentSelectedItem == null) return;
        switch (myInventory.currentSelectedItem.ReturnCloth().MyTag)
        {
            case SI_Cloths.ClothTag.Head:
                    headSprite.sprite = myInventory.currentSelectedItem.ReturnCloth().ClothSprite;
                break;
            case SI_Cloths.ClothTag.Torso:
                    torsoSprite.sprite = myInventory.currentSelectedItem.ReturnCloth().ClothSprite;
                break;
            case SI_Cloths.ClothTag.Pants:
                    pantsSprite.sprite = myInventory.currentSelectedItem.ReturnCloth().ClothSprite;
                break;
        }
        GameManager.Instance.PlaySound(dressSound);
        myInventory.dresBtn.interactable = false;
        myInventory.undressBtn.interactable = true;
    }

    public void UndressCloth()
    {
        if (myInventory.currentSelectedItem == null) return;
        switch (myInventory.currentSelectedItem.ReturnCloth().MyTag)
        {
            case SI_Cloths.ClothTag.Head:
                headSprite.sprite = defaultHeadSprite;
                break;
            case SI_Cloths.ClothTag.Torso:
                torsoSprite.sprite = defaultTorsoSprite;
                break;
            case SI_Cloths.ClothTag.Pants:
                pantsSprite.sprite = defaultPantsSprite;
                break;
        }
        GameManager.Instance.PlaySound(undressSound);
        myInventory.dresBtn.interactable = true;
        myInventory.undressBtn.interactable = false;
    }

    public bool BuyCloth(SI_Cloths _newCloth)
    {
        SI_Cloths clothCopy = _newCloth;
        //Check if we can pay the Cloth
        if (_newCloth.BuyingPrice <= playerMoney)
        {
            playerCloths.Add(_newCloth);
            PlayerMoney -= _newCloth.BuyingPrice;
            myInventory.AddInventoryButton(_newCloth);
            return true;
        }
        else
            return false;
    }

    public void EnableDressButtons()
    {
        bool canBeDress = true;
        switch (myInventory.currentSelectedItem.ReturnCloth().MyTag)
        {
            case SI_Cloths.ClothTag.Head:
                    canBeDress = headSprite.sprite == myInventory.currentSelectedItem.ReturnCloth().ClothSprite;
                break;
            case SI_Cloths.ClothTag.Torso:
                    canBeDress = torsoSprite.sprite == myInventory.currentSelectedItem.ReturnCloth().ClothSprite;
                break;
            case SI_Cloths.ClothTag.Pants:
                    canBeDress = pantsSprite.sprite == myInventory.currentSelectedItem.ReturnCloth().ClothSprite;
                break;
        }
        myInventory.dresBtn.interactable = !canBeDress;
        myInventory.undressBtn.interactable = canBeDress;
    }

    public SI_Cloths SellCloth()
    {
        //If the cloth we are selling is the same we are using
        //change to default cloth
        switch(myInventory.currentSelectedItem.ReturnCloth().MyTag)
        {
            case SI_Cloths.ClothTag.Head:
                if(headSprite.sprite == myInventory.currentSelectedItem.ReturnCloth().ClothSprite)
                    headSprite.sprite = defaultHeadSprite;
                break;
            case SI_Cloths.ClothTag.Torso:
                if (torsoSprite.sprite == myInventory.currentSelectedItem.ReturnCloth().ClothSprite)
                    torsoSprite.sprite = defaultTorsoSprite;
                break;
            case SI_Cloths.ClothTag.Pants:
                if (pantsSprite.sprite == myInventory.currentSelectedItem.ReturnCloth().ClothSprite)
                    pantsSprite.sprite = defaultPantsSprite;
                break;
        }
        SI_Cloths selledCloth = myInventory.currentSelectedItem.ReturnCloth();
        //Give player the money of the cloth
        GainMoney(myInventory.currentSelectedItem.ReturnCloth().SellingPrice);
        //Remove the cloth form the player inventory
        playerCloths.Remove(myInventory.currentSelectedItem.ReturnCloth());
        myInventory.RemoveInventoryButton(myInventory.currentSelectedItem);
        myInventory.sellBtn.interactable = false;

        return selledCloth;
    }

    public InventoryUI ReturnInventory()
    {
        return myInventory;
    }
}
