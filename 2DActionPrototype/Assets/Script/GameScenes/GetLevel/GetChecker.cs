using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetChecker : MonoBehaviour
{
    GameObject goal; //ゲームオブジェクト「Goal」を収納
    bool CheckGet = false; //プレイヤーがゴールに到達したかを判断

    void Start()
    {
        this.goal = GameObject.Find("Goal");
    }

    void Update()
    {
        //アイテム取得がされた時
        if (this.CheckGet)
        {
            this.goal.GetComponent<GoalChecker>().GetItem = true; //アイテム取得を「GoalChecker」に通知
            Destroy(gameObject); //アイテム取得でオブジェクトを消去
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