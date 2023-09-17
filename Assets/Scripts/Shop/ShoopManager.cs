using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoopManager : MonoBehaviour
{
    [SerializeField] private List<SI_Cloths> shopItemsCloths;
    [SerializeField] private List<Btn_ClothShop> shopItemsBtns;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private GameObject clothPrefab;
    [SerializeField] private Transform headMenu;
    private Btn_ClothShop currentSelectedItem;

    void Start()
    {
        FillItemList();
    }

    public void AddItem(SI_Cloths _addedItem)
    {
        shopItemsCloths.Add(_addedItem);
        GameObject _tempCloth = Instantiate(clothPrefab, headMenu);
        Btn_ClothShop temp_ClothScript = _tempCloth.GetComponent<Btn_ClothShop>();
        temp_ClothScript.UpdateInfo(_addedItem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            dialogUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            shopUI.SetActive(false);
            dialogUI.SetActive(false);
        }
    }

    public void SelectItem(Btn_ClothShop _selectedItem)
    {
        currentSelectedItem = _selectedItem;
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

    void FillItemList()
    {
        for(int i = 0; i< shopItemsCloths.Count; i++)
        {
            GameObject _tempCloth = Instantiate(clothPrefab, headMenu);
            Btn_ClothShop temp_ClothScript =  _tempCloth.GetComponent<Btn_ClothShop>();
            temp_ClothScript.UpdateInfo(shopItemsCloths[i]);
            temp_ClothScript.myButton.onClick.AddListener(() => SelectItem(temp_ClothScript));
            shopItemsBtns.Add(temp_ClothScript);
        }
    }
}
