using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{

  public Orb orb;

  public TMP_Text nameText;
  public TMP_Text damageText;
  public TMP_Text lifeText;
  public TMP_Text costText;
  public Image image;
  public Button button;

  // Start is called before the first frame update
  public void UpdateDisplay()
  {
    // update text and colors n that
    nameText.text = orb.name;
    damageText.text = orb.damage.ToString() + " damage";
    lifeText.text = orb.life.ToString() + " life";
    costText.text = orb.cost.ToString() + "$";
    Color tmpColor = orb.color;
    tmpColor.a = 0.5f;
    image.color = tmpColor;

    return;
  }

}
