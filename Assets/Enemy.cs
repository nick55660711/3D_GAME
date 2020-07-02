using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
     float SCSpeed;
     public float HP;
    float SCHP;

    private void Start()
    {
        SCSpeed = Speed;
        SCHP = HP;
        Physics.IgnoreLayerCollision(8, 8);
    }


    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * SCSpeed);

    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name == "mazu_wall")
        {
            SCSpeed = 0;
            GetComponent<Animator>().SetBool("Atk", true);
        }
    }






     private void OnTriggerExit(Collider other)
    {
        
        if (other.name == "mazu_wall")
        {
            SCSpeed = Speed;
            GetComponent<Animator>().SetBool("Atk", false);
        }


    }


    public void Damage(float damage,float dis)
    {
        SCHP -= damage;
        SCHP = Mathf.Clamp(SCHP, 0, HP);
        transform.position += new Vector3(0,0,dis);
        if(SCHP <= 0)
        {
            GetComponent<Animator>().SetTrigger("Dead");
            SCSpeed = 0;
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<GM>().DeadCount();
            gameObject.tag = "Untagged";
            
            Destroy(gameObject,3f);
        }



    }



    public void AttackPlayer()
    {

        FindObjectOfType<Player>().Hurt(10f);


    }





}
