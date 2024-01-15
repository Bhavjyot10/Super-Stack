using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShapesController : MonoBehaviour
{
    private bool isDrop = false;
    private bool shapeCol = false;
    private bool canTumble = true;
    private GameLogic gameLogic;

    private void Start()
    {
        gameLogic = GameObject.Find("Game Logic").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrop == false)
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.3f, 2.3f), (Mathf.Clamp(transform.position.y, 3.4f, 4.5f)), transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if (shapeCol && canTumble)
                gameLogic.Tumbled();
            gameLogic.AddVFX(collision.GetContact(0).point);
        }

        if (collision.gameObject.CompareTag("Shape"))
        {
            gameLogic.AddVFX(collision.GetContact(0).point);
            shapeCol = true;

        }

        gameLogic.PlayCollisionAudio();
    }

    private IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish"))
        {
            yield return new WaitForSeconds(2f);
            if(collision.IsTouching(transform.GetComponent<Collider2D>()))
                gameLogic.Finish();
        }
    }

    public void ChangeIsDrop()
    {
        isDrop = true;
    }

    public void ChangeCanTumble()
    {
        canTumble = false;
    }
}
