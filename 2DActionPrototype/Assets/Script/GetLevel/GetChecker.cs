using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetChecker : MonoBehaviour
{
    bool CheckGet = false; //プレイヤーがゴールに到達したかを判断

    void Start()
    {
    }

    void Update()
    {
        if (this.CheckGet)
        {
            //アイテム取得と同時にスプライトを消去
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //タグ「Player」でキャラがゴールにに触れた時を検出する
        if (other.gameObject.CompareTag("Player"))
        {
            this.CheckGet = true;
        }
    }
}