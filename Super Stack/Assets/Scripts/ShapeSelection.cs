using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using TMPro;

public class ShapeSelection : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private int maxShapes;
    [SerializeField] private GameObject PowerUpUI;
    [SerializeField] private GameObject shapepickup;
    [SerializeField] private GameLogic gameLogic;
    private TMP_Text maxShapesText;
    public GameStart gameStart;
    private bool powerupstart = true;

    void Start()
    {
        maxShapesText = transform.GetChild(2).GetComponent<TMP_Text>();
        maxShapesText.text = "x " + maxShapes.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(maxShapes > 0 && !spawnManager.ShapeSpawnedOrNot())
        {
            spawnManager.SpawnShape(gameObject.tag);
            maxShapes--;
            gameLogic.ShapesLeft();
            maxShapesText.text = "x " + maxShapes.ToString();
        }


        if (gameStart != null)
        {

            if (gameStart.isFirstTime() && powerupstart)
            {
                shapepickup.SetActive(false);
                PowerUpUI.SetActive(true);
                powerupstart = false;
            }
        }
    }
}
