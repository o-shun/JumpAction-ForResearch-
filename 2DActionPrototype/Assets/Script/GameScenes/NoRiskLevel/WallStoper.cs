using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStoper : MonoBehaviour
{
    GameObject player; //ゲームオブジェクト「Player」を収納
    GameObject resultWriter; //ゲームオブジェクト「Resultdirector」を収納
    GameObject endSceneDirector; //ゲームオブジェクト「EndSceneDirector」を収納
    public bool CheckGoal = false;
    public bool EndChecker = true;

    void Start()
    {
        //各ゲームオブジェクト を取得
        this.player = GameObject.Find("Player");
        this.resultWriter = GameObject.Find("ResultDirector");
        this.endSceneDirector = GameObject.Find("EndSceneDirector");
    }

    void Update()
    {
        //プレイヤーが壁に触れた時に速度を殺す
        if (this.CheckGoal && this.EndChecker)
        {
            //Debug.Break(); //テスト用画面停止
            this.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero; //プレイヤーの停止
            this.player.GetComponent<PlayerController>().enabled = false; //「PlayerController」の停止(操作不能にする)
            this.resultWriter.GetComponent<ResultWriter>().Result = 2; //ゲームの結果が失敗だった事を「Resultdirector」に通達する
            this.EndChecker = false;
        }

        //ボタンを押して進む
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) && !EndChecker)
        {
            this.endSceneDirector.GetComponent<EndSceneSetter>().EndSceneMode = 2; //リザルト後の選択画面を表示
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //タグ「Player」でキャラが触れた時を検出する
        if (other.gameObject.CompareTag("Player"))
        {
            this.CheckGoal = true;
        }
    }
}
