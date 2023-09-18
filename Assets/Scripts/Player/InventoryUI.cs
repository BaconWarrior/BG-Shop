using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<ClothInfoContainer> inventoryItemsBtns;
    public ClothInfoContainer currentSelectedItem;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI sellGoldText;
    public Button sellBtn;

    public void SetGoldText(string _amount)
    {
        if (goldText == null) return;
        goldText.text = _amount;
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
        currentSelectedItem = _selectedItem;
        sellGoldText.text = _selectedItem.ReturnCloth().SellingPrice.ToString();
        sellBtn.interactable = true;
    }
}
