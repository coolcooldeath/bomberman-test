using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Button dropBombButton;
    [HideInInspector]
    public float yDirection;
    [HideInInspector]
    public float xDirection;
    public Animator animator;
    private BoxCollider2D boxCollider2D;
    double roundX;
    double roundY;
    private Vector3 moveDelta;
    Vector3 pos;
    private IEnumerator coroutine;
    private RaycastHit2D hit;
    SpriteRenderer spriteRenderer;
    public GameObject bomb;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        dropBombButton.onClick.AddListener(DropBomb);


    }



    private void FixedUpdate()
    {


        moveDelta = new Vector3(xDirection, yDirection, 0);
        if (moveDelta.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(1, 1, 1);



        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, (moveDelta.y * Time.deltaTime), 0);
            if (yDirection == 1)

                animator.SetBool("isVerticalUp", true);

            else if (yDirection == -1)

                animator.SetBool("isVerticalDown", true);

            else if (yDirection == 0)
            {
                animator.SetBool("isVerticalDown", false);
                animator.SetBool("isVerticalUp", false);
            }
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            if (xDirection != 0)
            {
                animator.SetBool("isHorizontalRun", true);

            }
            else if (Input.GetAxisRaw("Horizontal") == 0)
                animator.SetBool("isHorizontalRun", false);
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);



        }





    }

    IEnumerator Resurection()
    {


        yield return new WaitForSeconds(1f);
        pos.x = -1.2f;
        pos.y = 0.24f;
        transform.position = pos;
        spriteRenderer.enabled = true;
        coroutine = null;


    }
    private void DropBomb()
    {

        if (bomb & GameObject.FindGameObjectsWithTag("bomb").Length < 5)
        {
            if (transform.position.x < 0)
                roundX = (transform.position.x - transform.position.x % 0.16 - 0.08);
            else
                roundX = (transform.position.x - transform.position.x % 0.16 + 0.08);
            if (transform.position.y < 0)
                roundY = (transform.position.y - transform.position.y % 0.16 - 0.08);
            else
                roundY = (transform.position.y - transform.position.y % 0.16 + 0.08);
            Instantiate(bomb, new Vector3((float)roundX,
                (float)roundY, bomb.transform.position.z), bomb.transform.rotation);


        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("explosion"))
        {
            coroutine = Resurection();
            spriteRenderer.enabled = false;
            StartCoroutine(coroutine);


        }
    }


}
