using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void NextScene()
    {

      // tell center the game is starting
      // PlayerCenter orbCenter = FindObjectOfType<PlayerCenter>();
      // if (orbCenter != null) orbCenter.gameStarted = true;

      // call next scene
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void LastScene()
    {

      // PlayerCenter orbCenter = FindObjectOfType<PlayerCenter>();
      // if (orbCenter != null) orbCenter.gameStarted = false;

      // call last scene
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
