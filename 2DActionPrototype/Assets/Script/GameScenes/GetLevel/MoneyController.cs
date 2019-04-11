using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        //一定の位置を越したらオブジェクトを抹殺
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }
}
