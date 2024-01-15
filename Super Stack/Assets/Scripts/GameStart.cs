using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Transform FirstTimeUI;
    private bool firstTime = false;
    private void Awake()
    {
        if(!PlayerPrefs.HasKey("first time"))
        {
            FirstTimeUI.gameObject.SetActive(true);
            PlayerPrefs.SetInt("first time", 0);
            firstTime = true;
        }
    }

    public bool isFirstTime()
    { return firstTime; }
}
