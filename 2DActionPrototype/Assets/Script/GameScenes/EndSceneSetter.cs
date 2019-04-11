using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneSetter : MonoBehaviour
{
    //表示させるオブジェクト
    GameObject Key;
    GameObject EndGameSelect_1;
    GameObject EndGameSelect_2;

    //位置を決めるために取得
    GameObject Camera;

    public int EndSceneMode = 0; //ゲーム終了時の画面の種類を判別する
    float WritePos = 0; //リザルトを描画するX軸ポジションを収納

    void Start()
    {
        //各オブジェクトを取得
        Key = GameObject.Find("SelectKey");
        EndGameSelect_1 = GameObject.Find("EndGameSelect");
        EndGameSelect_2 = GameObject.Find("EndGameSelect_2");
        Camera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        //ゲームが終了し、リザルト後の選択画面に入った時
        if (EndSceneMode != 0)
        {
            //描画の中心になるX軸をカメラ位置から取得
            WritePos = Camera.transform.position.x;

            //↓没
            if (EndSceneMode == 1)
            {
                Key.GetComponent<EndKeyController_1>().enabled = true;
                Key.transform.position = new Vector3(WritePos - 1.85f, -1.25f, 0);
                Key.transform.localScale = new Vector3(0.3f, 0.2f, 0);
                EndGameSelect_1.transform.position = new Vector3(WritePos, 0, 0);
                EndGameSelect_1.transform.localScale = new Vector3(1.1f, 1.2f, 0);
                EndSceneMode = 0;
            }

            //リザルト後の選択画面のセット
            if (EndSceneMode == 2)
            {
                Key.GetComponent<EndKeyController_2>().enabled = true;
                Key.transform.position = new Vector3(WritePos - 2.55f, -1.25f, 0);
                Key.transform.localScale = new Vector3(0.3f, 0.2f, 0);
                EndGameSelect_2.transform.position = new Vector3(WritePos, 0, 0);
                EndGameSelect_2.transform.localScale = new Vector3(1.1f, 1.2f, 0);
                EndSceneMode = 0;
            }
        }
    }


}
