using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private GameObject difficulty;
    public void DifficultyChanged(int value)
    {
        PlayerPrefs.SetInt("Difficulty", value);
    }
}
