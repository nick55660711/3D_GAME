using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float TotalHP;
     float SCHP;
    public Image HP_Bar;

    public Image Magic_bar;
    public bool CanCreateMagic;
    public GameObject MagicObj;
    GameObject MagicObjPrefab;

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

    public void Hurt(float atk)
    {
        SCHP -= atk;
        HP_Bar.fillAmount = SCHP / TotalHP;


        if(HP_Bar.fillAmount == 0)
        {
            FindObjectOfType<GM>().isWin = false;
            FindObjectOfType<GM>().GameOver(0);
        }

        if (SCHP / TotalHP < 0.5) { Destroy(GameObject.FindGameObjectWithTag("NPC")); } 

    }

    public void PointerDownMagic()
    {
        if(Magic_bar.fillAmount == 1)
        {
            CanCreateMagic = true;
        }

    }






    private void Start()
    {
        Ani = GetComponent<Animator>();
        SCHP = TotalHP;
    }

    private void Update()
    {
        //按住滑鼠左鍵，玩家會持續面向滑鼠點擊的位置
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Hits = Physics.RaycastAll(Camera.main.transform.position, ray.direction, 100);

            for (int i = 0; i < Hits.Length; i++)
            {
                if(Hits[i].collider.CompareTag("Floor"))
                {
                    targetPos = new Vector3(Hits[i].point.x, transform.position.y, Hits[i].point.z);
                    if (!CanCreateMagic)
                    {
                        LookPos = Vector3.Lerp(LookPos, targetPos, Time.deltaTime * 10);
                        transform.LookAt(LookPos);
                        Ani.SetBool("Att", true);
                    }
                    else
                    {
                        if (MagicObjPrefab == null)
                            MagicObjPrefab = Instantiate(MagicObj) as GameObject;

                        if(!MagicObjPrefab.GetComponentInChildren<Rigidbody>().useGravity)
                            MagicObjPrefab.transform.position = targetPos;

                    }

                }


            }


             
        }


        else { Ani.SetBool("Att", false); }

        if(Input.GetMouseButtonUp(0) && CanCreateMagic)
        {
            MagicObjPrefab.GetComponentInChildren<Rigidbody>().useGravity = true;
        }

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
