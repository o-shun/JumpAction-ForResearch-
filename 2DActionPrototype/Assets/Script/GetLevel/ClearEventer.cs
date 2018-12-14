using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEventer : MonoBehaviour
{
    public GameObject MoneyPrefab;
    float span = 0.25f;
    float delta = 0.0f;

    void Start()
    {
    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if(this.delta > this.span)
        {
            this.delta = 0.0f;
            GameObject go = Instantiate(MoneyPrefab) as GameObject;
            int px = Random.Range(-6, 7);
            go.transform.position = new Vector2(px, 6);
        }
    }
}
