using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Boss; 
    public GameObject EnemyCreatePoint;
    public float CreateTime;
    public int MaxNumber;
    int Number;

    public void CreateEnemy()
    {
        if (Number < MaxNumber)
        {
            Vector3 MaxValue = EnemyCreatePoint.GetComponent<Collider>().bounds.max;
            Vector3 MinValue = EnemyCreatePoint.GetComponent<Collider>().bounds.min;
            Vector3 RandomPox = new Vector3(Random.Range(MinValue.x, MaxValue.x), MinValue.y, MinValue.z);


            Instantiate(Enemy1, RandomPox - Vector3.right, EnemyCreatePoint.transform.rotation);

            Number++;
        }
        else if (Number == MaxNumber && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Instantiate(Boss, EnemyCreatePoint.transform.position, EnemyCreatePoint.transform.rotation);
            CancelInvoke();
        }

    }

    public void CreateBoss()
    {
       



    }




    private void Start()
    {
        InvokeRepeating("CreateEnemy", 0.2f, CreateTime);
    }



    private void Update()
    {







    }









}
