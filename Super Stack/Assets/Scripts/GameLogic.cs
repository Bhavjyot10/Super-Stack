using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform afterSpawnParent;
    [SerializeField] private Transform finishPt;
    [SerializeField] private Transform groundPt;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TMP_Text powerUpCount;
    [SerializeField] private AudioSource gameOverAudio;
    [SerializeField] private AudioSource powerUpAudio;
    [SerializeField] private AudioSource collisionAudio;
    private bool victory = false;
    private int powerUps = 2;
    private float initialTimeCount = 0f;
    private float finalTimeCount = 0f;
    private float timeCount = 0f;
    private int stars = 0;
    private bool tumbled = false;
    private int shapesUsed = 0;
    private bool check = true;
    [SerializeField] private int totalShapes;
    private int shapesLeft;
    private int shapedrops = 0;
    private bool failed = false;
    public GameObject[] VFX;
    private bool[] star = new bool[3];
    [SerializeField] private int noOfShapesToUse;
    [SerializeField] private float timeLeft;


    private void Awake()
    {
        initialTimeCount = Time.time;
    }

    private void Start()
    {
        shapesLeft = totalShapes;
        powerUpCount.text = "x " + powerUps.ToString();
    }

    private void Update()
    {
        if (check && victory)
        {
            AfterVictory();
        }

        if (victory)
            StopCoroutine(GameOver());
    }

    public void Finish()
    {
        victory = true;
        Debug.Log(victory);
    }

    void AfterVictory()
    {
        check = false;
        if (victory)
        {
            CountTime();
            CountStars();
        }
    }
    void CountTime()
    {
        finalTimeCount = Time.time;
        timeCount = finalTimeCount - initialTimeCount;
        ShapesCount();
    }

    void ShapesCount()
    {
        shapesUsed = afterSpawnParent.childCount;
    }

    public void Tumbled()
    { tumbled = true; }

    void CountStars()
    {
        if (timeCount < timeLeft)
        {
            stars++;
            star[1] = true;
        }

        if (shapesUsed <= noOfShapesToUse)
        {
            stars++;
            star[0] = true;
        }

        if (!tumbled)
        {
            stars++;
            star[2] = true;
        }

        if (stars == 0)
            stars = 1;

        if (failed)
            stars = 0;
        uiManager.UpdateGameOver(shapesUsed, timeCount, tumbled, stars, star);
        GameOverPanel.SetActive(true);
        gameOverAudio.Play();
    }

    public void ApplyPowerUp()
    {
        Animator anim = spawnParent.transform.GetChild(0).transform.GetComponent<Animator>();
        if (powerUps != 0 && !anim.GetBool("Wide"))
        {
            anim.SetBool("Wide", true);
            GameObject temp = Instantiate(VFX[1], spawnParent.transform.GetChild(0).transform.position, Quaternion.identity);
            Destroy(temp, 4f);
            powerUpAudio.Play();
            powerUps--;
            powerUpCount.text = "x " + powerUps.ToString();
        }
    }

    public void ShapesLeft()
    {
        shapesLeft--;
        Debug.Log(shapesLeft);
        
    }

    public void DropCount()
    {
        shapedrops++;
        Debug.Log(shapedrops);
        if(shapedrops == totalShapes)
            StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5f);
        if (shapesLeft <= 0 && shapedrops==totalShapes)
        {
            failed = true;
            Finish();
        }
        
    }

    public void AddVFX(Vector2 pos)
    {
        GameObject temp = Instantiate(VFX[0], pos, Quaternion.identity);
        Destroy(temp, 4f);
    }

    public void PlayCollisionAudio()
    {
        collisionAudio.Play();
    }
}
