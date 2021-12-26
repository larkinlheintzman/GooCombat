using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TMP_Text))]
public class Countdown : MonoBehaviour
{
    public TMP_Text countText;
    public float interval = 1.0f;
    public float counter = 4.0f;
    public string sceneTarget = "GameScene";

    public void Awake()
    {
      countText = gameObject.GetComponent<TMP_Text>();
    }

    public void Update()
    {
      counter -= Time.deltaTime*interval;
      countText.text = Mathf.Floor(counter).ToString();

      if (counter <= 0.0f)
      {
        // fire off scene change
        SceneManager.LoadScene(sceneTarget);
      }
    }
}
