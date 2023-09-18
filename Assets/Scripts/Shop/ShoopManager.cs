using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShoopManager : MonoBehaviour
{
    [SerializeField] private List<SI_Cloths> shopItemsCloths;
    [SerializeField] private List<ClothInfoContainer> shopItemsBtns;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject playerShopUI;
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private GameObject clothPrefab;
    [SerializeField] private Transform headMenu;
    [SerializeField] private TextMeshProUGUI sellingPriceText;
    [SerializeField] private Button sellingBtn;

    [SerializeField] private ClothInfoContainer currentSelectedItem;

    void Start()
    {
        FillItemList();
        Invoke("enablesell", 1);
    }

    void enablesell()
    {
        GameManager.Instance.playerInventory.ReturnInventory().sellBtn.onClick.AddListener(BuyFromPlayer);
    }

    public void AddItem(SI_Cloths _addedItem)
    {
        shopItemsCloths.Add(_addedItem);
        GameObject _tempCloth = Instantiate(clothPrefab, headMenu);
        ClothInfoContainer temp_ClothScript = _tempCloth.GetComponent<ClothInfoContainer>();
        temp_ClothScript.UpdateInfo(_addedItem,false);
        shopItemsBtns.Add(temp_ClothScript);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.playerController.OnAction.AddListener(StartConversation);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.playerController.OnAction.RemoveListener(StartConversation);
        }
    }

    public void SelectItem(ClothInfoContainer _selectedItem)
    {
        currentSelectedItem = _selectedItem;
        sellingPriceText.text = _selectedItem.ReturnCloth().BuyingPrice.ToString();
        sellingBtn.interactable = true;
    }

    public void SellSelectedItem()
    {
        //if we Cant pay just give a sound Feedback
        if(!GameManager.Instance.playerInventory.BuyCloth(currentSelectedItem.ReturnCloth()))
        {
            return;
        }
        //Remove Purhcased Items from the menu
        shopItemsCloths.Remove(currentSelectedItem.ReturnCloth());
        shopItemsBtns.Remove(currentSelectedItem);
        currentSelectedItem.SelfDestruction();
        //Disable buy button
        sellingPriceText.text = "---";
        sellingBtn.interactable = false;
        SortClothList();
    }

    public void BuyFromPlayer()
    {
        AddItem(GameManager.Instance.playerInventory.SellCloth());
        
    }

    public void OpenPlayerSellMenu()
    {
        currentSelectedItem = null;
        playerShopUI.SetActive(true);
        shopUI.SetActive(false);
        dialogUI.SetActive(false);
        GameManager.Instance.playerInventory.ReturnInventory().sellBtn.gameObject.SetActive(true);
        GameManager.Instance.playerController.SetBussy(true);
    }

    public void OpenShopMenu()
    {
        currentSelectedItem = null;
        shopUI.SetActive(true);
        playerShopUI.SetActive(false);
        dialogUI.SetActive(false);
        GameManager.Instance.playerController.SetBussy(true);
    }

    public void CloseCurrentShopMenu()
    {
        playerShopUI.SetActive(false);
        shopUI.SetActive(false);
        dialogUI.SetActive(true);
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
        Debug.Log("Tag= " + tagToCompare);
        if(tagToCompare == SI_Cloths.ClothTag.Null)
        {
            for (int i = 0; i < shopItemsBtns.Count; i++)
            {
                shopItemsBtns[i].gameObject.SetActive(true);
            }
            return;
        }
        for (int i = 0; i < shopItemsBtns.Count; i++)
        {
            shopItemsBtns[i].gameObject.SetActive(shopItemsBtns[i].CompareClothTag(tagToCompare));
        }
    }

    public void StartConversation()
    {
        dialogUI.SetActive(true);
        GameManager.Instance.playerController.SetBussy(true);
    }

    public void CloseConversation()
    {
        dialogUI.SetActive(false);
        GameManager.Instance.playerController.SetBussy(false);
    }

    void FillItemList()
    {//Create shop items and loaded them into a list
        for(int i = 0; i< shopItemsCloths.Count; i++)
        {
            GameObject _tempCloth = Instantiate(clothPrefab, headMenu);
            ClothInfoContainer temp_ClothScript =  _tempCloth.GetComponent<ClothInfoContainer>();
            temp_ClothScript.UpdateInfo(shopItemsCloths[i], false);
            temp_ClothScript.myButton.onClick.AddListener(() => SelectItem(temp_ClothScript));
            shopItemsBtns.Add(temp_ClothScript);
        }
        SortClothList();
    }

    void SortClothList()
    {
        shopItemsCloths.Sort((left, right) => left.ClothName.CompareTo(right.ClothName));
    }

    
}
