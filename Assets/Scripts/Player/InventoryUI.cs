using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<ClothInfoContainer> inventoryItemsBtns;
    [SerializeField] private PlayerInventory myPlayer;
    public ClothInfoContainer currentSelectedItem;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject shopOnlyBtn;

    [SerializeField] private Transform container;
    [SerializeField] private Transform camFocus;

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI sellGoldText;
    public Button sellBtn;
    public Button dresBtn;
    public Button undressBtn;


    public void SetGoldText(string _amount)
    {
        if (goldText == null) return;
        goldText.text = _amount;
    }
    public void FilterByTag(string _tag)
    {
        SI_Cloths.ClothTag tagToCompare;
        switch (_tag)
        {
            case "head":
                tagToCompare = SI_Cloths.ClothTag.Head;
                break;
            case "torso":
                tagToCompare = SI_Cloths.ClothTag.Torso;
                break;
            case "pants":
                tagToCompare = SI_Cloths.ClothTag.Pants;
                break;
            default:
                tagToCompare = SI_Cloths.ClothTag.Null;
                break;
        }
        if (tagToCompare == SI_Cloths.ClothTag.Null)
        {
            for (int i = 0; i < inventoryItemsBtns.Count; i++)
            {
                if(inventoryItemsBtns[i].ReturnCloth() != null)
                    inventoryItemsBtns[i].gameObject.SetActive(true);
            }
            return;
        }
        for (int i = 0; i < inventoryItemsBtns.Count; i++)
        {
            if (inventoryItemsBtns[i].ReturnCloth() != null)
            {
                if(inventoryItemsBtns[i].CompareClothTag(tagToCompare))
                    inventoryItemsBtns[i].gameObject.SetActive(true);
                else
                    inventoryItemsBtns[i].gameObject.SetActive(false);
            }
            
        }
        GameManager.Instance.PlayClickSound();
    }

    public void AddInventoryButton(SI_Cloths _addedItem)
    {
        //Similar way to add items to the menu
        //this ways its not necesary to delete anything
        if(inventoryItemsBtns.Count >0)
        {
            for(int i = 0; i<inventoryItemsBtns.Count; i++)
            {
                if(inventoryItemsBtns[i].ReturnCloth() == null)
                {
                    inventoryItemsBtns[i].UpdateInfo(_addedItem,true);
                    inventoryItemsBtns[i].gameObject.SetActive(true);
                    return;
                }
            }
        }
        //If there are no objects to recicle, create a new one
        GameObject _tempCloth = Instantiate(buttonPrefab, container);
        ClothInfoContainer temp_ClothScript = _tempCloth.GetComponent<ClothInfoContainer>();
        temp_ClothScript.UpdateInfo(_addedItem,true);
        temp_ClothScript.myButton.onClick.AddListener(() => SelectItem(temp_ClothScript));
        inventoryItemsBtns.Add(temp_ClothScript);
    }
     
    public void RemoveInventoryButton(ClothInfoContainer _bottonToRemove)
    {
        _bottonToRemove.ClearInfo();
        _bottonToRemove.gameObject.SetActive(false);
    }

    public void SelectItem(ClothInfoContainer _selectedItem)
    {
        if(currentSelectedItem != null)
            currentSelectedItem.SelectionVisibility(false);
        currentSelectedItem = _selectedItem;
        sellGoldText.text = _selectedItem.ReturnCloth().SellingPrice.ToString();
        sellBtn.interactable = true;
        currentSelectedItem.SelectionVisibility(true);
        myPlayer.EnableDressButtons();
    }

    public void ToggleInventory()
    {
        shopOnlyBtn.SetActive(false);
        inventoryMenu.SetActive(!inventoryMenu.activeInHierarchy);
        GameManager.Instance.playerController.SetBussy(inventoryMenu.activeInHierarchy);
        if(inventoryMenu.activeInHierarchy)
            GameManager.Instance.FocusObjective(camFocus, 4);
        else
            GameManager.Instance.UnfocusCam();
    }
}
