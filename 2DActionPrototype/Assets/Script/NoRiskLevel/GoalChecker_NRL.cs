using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalChecker_NRL : MonoBehaviour
{
    public GameObject player;
    bool CheckGoal = false; //プレイヤーがゴールに到達したかを判断

    void Start()
    {
        //Find関数で各オブジェクトの呼び出し
        this.player = GameObject.Find("Player");
    }

    void Update()
    {
        if (this.CheckGoal)
        {
            this.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero; //プレイヤーの停止
            this.player.GetComponent<PlayerController>().enabled = false; //「PlayerController」の停止(操作不能にする)

            if (Input.GetKey(KeyCode.Joystick1Button8))　//ゴールした時、スペースキー入力でリトライ
            {
                SceneManager.LoadScene("NoRiskLevel");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //タグ「Player」でキャラがゴールにに触れた時を検出する
        if (other.gameObject.CompareTag("Player"))
        {
            this.CheckGoal = true;
        }
    }
}
