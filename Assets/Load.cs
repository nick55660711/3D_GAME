using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public GameObject GM_;
    private void Awake()
    {
        if (FindObjectOfType<GM>() == null)
        {

        Instantiate(GM_);
        }
    }
}
