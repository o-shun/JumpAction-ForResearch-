using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{ 
    GameObject player; //ゲームオブジェクト「Player」を収納
    GameObject clearEvent; //ゲームオブジェクト「ClearEventDirector」を収納
    GameObject resultWriter; //ゲームオブジェクト「Resultdirector」を収納
    GameObject endSceneDirector; //ゲームオブジェクト「EndSceneDirector」を収納
    int GetLevelNumber; //レベルの種類を取得する。０…ノーリスクレベル、１…リスクレベル、２…ゲットレベル

    bool CheckGoal = false; //プレイヤーがゴールに到達したかを判断
    bool PlayerStop = false; //プレイヤーが停止したかを判断
    public bool GetItem = false; //アイテム取得がされたかを「GetChecker」から受け取る

    void Start()
    {
        //Find関数で各オブジェクトの呼び出し
        this.player = GameObject.Find("Player");
        this.resultWriter = GameObject.Find("ResultDirector");
        this.endSceneDirector = GameObject.Find("EndSceneDirector");

        //レベルを取得する
        GetLevelNumber = Keysetter.LevelGetter();

        //ゲットレベルの場合、クリア後のイベンターを呼び出し
        if (GetLevelNumber == 2)
        {
            this.clearEvent = GameObject.Find("ClearEventDirector");
        }
    }

    void Update()
    {
        //プレイヤーがゴールした時
        if (this.CheckGoal)
        {
            if (!this.PlayerStop)
            {
                Debug.Log(this.player.transform.position.x);
                this.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero; //プレイヤーの停止
                PlayerStop = true;
            }
            this.player.GetComponent<PlayerController>().enabled = false; //「PlayerController」の停止(操作不能にする)

            //リスクレベル、ノーリスクレベルはゴールに到着した時点でクリアを判定
            if (GetLevelNumber != 2)
            {
                this.resultWriter.GetComponent<ResultWriter>().Result = 1; //ゲームの結果が成功だった事を「Resultdirector」に通達する
            }

            //ゲットレベルの時、アイテムが取得されていた場合、プレイヤーのX軸ポジションを収納＆「ClearEventDirector」に送信
            if (GetLevelNumber == 2)
            {
                if (GetItem) //アイテム取得時の場合
                {
                    this.clearEvent.GetComponent<ClearEventer>().DropPos = this.player.transform.position.x;
                    this.resultWriter.GetComponent<ResultWriter>().Result = 1; //ゲームの結果が成功だった事を「Resultdirector」に通達する
                }
                else if (!GetItem) //アイテム未取得の場合
                {
                    this.resultWriter.GetComponent<ResultWriter>().Result = 2; //ゲームの結果が失敗だった事を「Resultdirector」に通達する
                }
            }

            //ボタンを押して進む
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                this.endSceneDirector.GetComponent<EndSceneSetter>().EndSceneMode = 2; //リザルト後の選択画面を表示
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
