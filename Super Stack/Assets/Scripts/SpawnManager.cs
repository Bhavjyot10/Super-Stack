using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPt;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform afterSpawnParent;
    [SerializeField] private Transform afterSpawnUI;
    [SerializeField] private GameObject[] shapesPrefabs;
    [SerializeField] private GameObject PowerUpUI;
    [SerializeField] private GameObject flagHeightTutorial;
    [SerializeField] private GameObject flagHeightCol;
    public GameLogic GameLogic;
    private GameObject shapeToSpawn;
    private GameObject currentShape;
    public GameStart gameStart;
    private bool powerupstart = true;
    private int spawnCount = 0;
    private bool isShapeSpawned = false;
    public void SpawnShape(string tag)
    {
        isShapeSpawned = true;
        spawnCount++;
        ShapeSwitch(tag);
        currentShape = Instantiate(shapeToSpawn, spawnPt.position, Quaternion.identity, spawnParent);
        if (spawnCount == 1)
            currentShape.GetComponent<ShapesController>().ChangeCanTumble();
        afterSpawnUI.gameObject.SetActive(true);
    }

    void ShapeSwitch(string tag)
    {
        switch (tag)
        {
            case "Circle_UI": shapeToSpawn = shapesPrefabs[0]; break;
            case "Rectangle_UI": shapeToSpawn = shapesPrefabs[1]; break;
            case "Square_UI": shapeToSpawn = shapesPrefabs[2]; break;
            case "Triangle_UI": shapeToSpawn = shapesPrefabs[3]; break;
            case "Pentagon_UI": shapeToSpawn = shapesPrefabs[4]; break;
        }
    }

    public void DropShape()
    {
        if (currentShape != null)
        {
            currentShape.layer = 0;
            currentShape.GetComponent<Rigidbody2D>().gravityScale = 1f;
            currentShape.GetComponent<ShapesController>().ChangeIsDrop();
            currentShape.GetComponent<LeanTouch>().enabled = false;            
            GameLogic.DropCount();
            currentShape.transform.SetParent(afterSpawnParent, true);            
            currentShape = null;
            afterSpawnUI.gameObject.SetActive(false);
            if (gameStart != null)
            {
                if (gameStart.isFirstTime() && powerupstart)
                {
                    flagHeightTutorial.SetActive(true);
                    PowerUpUI.SetActive(false);
                    powerupstart = false;
                    StartCoroutine(FadeFlagHeight());
                }
            }
            isShapeSpawned = false;
        }
    }

    IEnumerator FadeFlagHeight()
    {
        yield return new WaitForSeconds(4f);
        flagHeightTutorial.SetActive(false);
    }

    public bool ShapeSpawnedOrNot()
    {
        return isShapeSpawned;
    }
    
}
