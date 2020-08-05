using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Power;
    public float speed;
    SpriteRenderer SP;
    Rigidbody2D rig;
    Vector3 BeganPosition;
    Vector3 EndPosition;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        SP = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.velocity = new Vector2(0, Power);
        }

        //手指點擊
        if (Input.touchCount > 0)
        {
            rig.velocity = new Vector2(0, Power);
        }

        /*
        //手指移動
         if(Input.touchCount == 1)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                BeganPosition = Input.touches[0].position;

            }

            if(Input.touches[0].phase == TouchPhase.Ended && Input.touches[0].phase == TouchPhase.Canceled)
            {

                EndPosition = Input.touches[0].position;


            }
            //偏向水平滑動時
            if(Mathf.Abs(BeganPosition.x-EndPosition.x)> Mathf.Abs(BeganPosition.y - EndPosition.y))
            {

                rig.velocity = new Vector2(0, Power);
            }
            //偏向垂直滑動時
            else
            {
                rig.velocity = new Vector2(0, Power);
            }*/

        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    bool trun;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Border"))
        {
            trun = !trun;
            transform.Rotate(Vector3.up * 180);
        }
     
    }

 
}



