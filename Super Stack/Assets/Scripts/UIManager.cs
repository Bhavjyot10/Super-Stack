using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform starsStats;
    [SerializeField] private Transform stars;
    [SerializeField] private Sprite activeStar;
    private TMP_Text star1;
    private TMP_Text star2;
    private TMP_Text star3;

    private void Start()
    {
        star1 = starsStats.GetChild(0).GetComponent<TMP_Text>();
        star2 = starsStats.GetChild(1).GetComponent<TMP_Text>();
        star3 = starsStats.GetChild(2).GetComponent<TMP_Text>();
    }
    public void UpdateGameOver(int shapes, float time, bool topple, int stars, bool[] star)
    {
        star1.text = shapes.ToString();
        star2.text = ((int)time).ToString() + "s";
        if (topple)
            star3.text = "YES";
        else
            star3.text = "NO";
        UpdateStars(star);
    }

    void UpdateStars(bool[] star)
    {
        for(int i = 0; i < stars.childCount; i++)
        {
            if (star[i])
                this.stars.GetChild(i).GetComponent<Image>().sprite = activeStar;


            Debug.Log(i.ToString() + star[i]);
        }
    }
}
