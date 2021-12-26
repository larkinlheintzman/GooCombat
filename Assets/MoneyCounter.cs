using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
  private PlayerCenter playerCenter;
  private TMP_Text moneyText;
  public int moneyTarget = 0;
  private float moneyCurrent = 0f;

  public void Awake()
  {
    playerCenter = FindObjectOfType<PlayerCenter>();
    moneyText = gameObject.GetComponent<TMP_Text>();
    moneyTarget = playerCenter.money - playerCenter.oldMoney;
    moneyCurrent = 0.0f;
  }

  public void Update()
  {
    if (playerCenter != null)
    {
      moneyText.text = "Earned Money: " + (Mathf.Round(moneyCurrent)).ToString();
      moneyCurrent += 0.01f*(moneyTarget - moneyCurrent);
    }

  }
}
