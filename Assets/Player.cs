using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    RaycastHit[] Hits;
    Vector3 targetPos;
    Vector3 LookPos;
    Animator Ani;

    [Header("小招發射物件")]
    public GameObject Arrow;
    [Header("小招發射的位置")]
    public GameObject ArrowPos;



    public void CreateArrow()
    {

        Instantiate(Arrow, ArrowPos.transform.position,ArrowPos.transform.rotation);

    }









    private void Start()
    {
        Ani = GetComponent<Animator>();
    }

    private void Update()
    {
        //按住滑鼠左鍵，玩家會持續面向滑鼠點擊的位置
        if (Input.GetMouseButton(0))
        {
            Ani.SetBool("Att", true);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Hits = Physics.RaycastAll(Camera.main.transform.position, ray.direction, 100);

            for (int i = 0; i < Hits.Length; i++)
            {
                if(Hits[i].collider.CompareTag("Floor"))
                {
                    targetPos = new Vector3(Hits[i].point.x, transform.position.y, Hits[i].point.z);
                    LookPos = Vector3.Lerp(LookPos, targetPos, Time.deltaTime * 10);
                    transform.LookAt(LookPos);
                }


            }


             
        }


        else { Ani.SetBool("Att", false); }



    }

    private void OnDrawGizmos()
    {
        //圖示.顏色 = 顏色(R,G,B,A)

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Gizmos.color = new Color(1, 0, 0, 0.5f);

        // 圖示.繪製線條(起點,終點)
        Gizmos.DrawRay(Camera.main.transform.position, ray.direction * 100);
    }





}
