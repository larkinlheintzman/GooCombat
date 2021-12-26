using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class ShopScreen : MonoBehaviour
{
  public Orb[] orbTypes;
  private RectTransform rect;
  // template for text n that
  public GameObject shopCardFab;
  public PlayerCenter orbCenter;

  public TMP_Text moneyDisplay;

  public void Awake()
  {
    rect = GetComponent<RectTransform>();
    orbCenter = FindObjectOfType<PlayerCenter>();
    if (orbCenter == null)
    {
      // if it's the first round, we need to add the player center
      GameObject orbCenterObj = new GameObject();
      orbCenterObj.name = "playerOrbCenter";
      orbCenter = orbCenterObj.AddComponent<PlayerCenter>();
      orbCenter.transform.position += new Vector3(4.0f, 0.0f, 1.5f);
    }

    moneyDisplay.text = orbCenter.money.ToString() + "$ remaining";
    orbTypes = Resources.LoadAll<Orb>("Orbs");
    for(int i = 0; i < orbTypes.Length; i++)
    {
      // load up each orb type and fill out image and stats of orb
      // ... script-y dont it?
      CardDisplay newShopCard = Instantiate(shopCardFab).GetComponent<CardDisplay>();
      newShopCard.transform.SetParent(transform);
      newShopCard.transform.localScale = Vector3.one;
      newShopCard.transform.localPosition = Vector3.zero;
      newShopCard.transform.localPosition += new Vector3(0.0f, -75.0f*(i-4), 0.0f);
      newShopCard.orb = orbTypes[i];

      // assign buttery buttons
      newShopCard.button.onClick.AddListener(delegate {BuyOrb(newShopCard.orb);});
      newShopCard.UpdateDisplay();
    }
    rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (orbTypes.Length+1f)*75f);
    // rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + orbTypes.Length*75f/2f);
  }

  public void BuyOrb(Orb orbToBuy)
  {
    if (orbCenter.money >= orbToBuy.cost)
    {
      orbCenter.money -= orbToBuy.cost;
      orbCenter.AddOrb(orbToBuy);
      moneyDisplay.text = orbCenter.money.ToString() + "$ remaining";
    }
    else
    {
      // print("dont have enough money for that");
    }
  }

  public void AddOrb()
  {
    return;
  }

  public void RemoveOrb()
  {
    return;
  }

}
