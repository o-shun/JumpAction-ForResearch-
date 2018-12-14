using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalChecker_GL : MonoBehaviour
{
    GameObject player; //ゲームオブジェクト「Player」を収納
    GameObject clearEvent; //ゲームオブジェクト「ClearEventDirector」を収納
    bool CheckGoal = false; //プレイヤーがゴールに到達したかを判断
    public bool GetItem = false; //アイテム取得がされたかを「GetChecker」から受け取る

    void Start()
    {
        //Find関数で各オブジェクトの呼び出し
        this.player = GameObject.Find("Player");
        this.clearEvent = GameObject.Find("ClearEventDirector");
    }

    void Update()
    {
        //プレイヤーがゴールした時
        if (this.CheckGoal)
        {
            this.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero; //プレイヤーの停止
            this.player.GetComponent<PlayerController>().enabled = false; //「PlayerController」の停止(操作不能にする)

            //アイテムが取得されていた場合、プレイヤーのX軸ポジションを収納＆「ClearEventDirector」に送信
            if (GetItem)
            {
                this.clearEvent.GetComponent<ClearEventer>().DropPos = this.player.transform.position.x;
            }

            //ゴールした時、スペースキー入力でリトライ
            if (Input.GetKey(KeyCode.Joystick1Button8))
            {
                SceneManager.LoadScene("GetLevel");
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
