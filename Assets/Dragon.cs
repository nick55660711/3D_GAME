using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) { other.GetComponent<Enemy>().Damage(100, 10f); }
        if (other.CompareTag("Boss")) { other.GetComponent<Enemy>().Damage(50, 5f); }
        if (other.CompareTag("Floor")) {
            Destroy(transform.parent.gameObject);
            FindObjectOfType<Player>().CanCreateMagic = false;
            FindObjectOfType<GM>().MagicSC = 0;
        }






    }

}
