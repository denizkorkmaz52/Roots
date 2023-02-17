using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;

    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }
    public void PlayButton()
    {
        if (!PlayerPrefs.HasKey("Difficulty"))
        {
            PlayerPrefs.SetInt("Difficulty", 0);
        }
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void BackButton()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }
}
