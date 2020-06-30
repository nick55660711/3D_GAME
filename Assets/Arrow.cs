using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Speed, DeltaTime;
    private void Start()
    {
        Destroy(gameObject, DeltaTime);


    }


    private void Update()
    {
         transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Damage(30f, 5f);
            Destroy(gameObject);
        }
        
        if (other.CompareTag("Boss"))
        {
            other.GetComponent<Enemy>().Damage(20f, 3f);
            Destroy(gameObject);
        }


    }



}
