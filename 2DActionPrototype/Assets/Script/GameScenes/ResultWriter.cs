using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultWriter : MonoBehaviour 
{
    GameObject Clear;
    GameObject Miss;
    GameObject Camera;

    public int Result = 0; //結果を判定する。１…成功、２…失敗
    float WritePos = 0; //リザルトを描画するX軸ポジションを収納

    private float timeCnt = 0.0f; //ゲームが終了してからの時間経過を収納する
    private float stopTime = 0.2f; //ゲームが終了してから結果を表示するまでの時間を収納する

    void Start () 
    {
        Clear = GameObject.Find("ClearWord");
        Miss = GameObject.Find("MissWord");
        Camera = GameObject.Find("Main Camera");
    }
	
	void Update () 
    {
		if(Result != 0)
        {
            //タイマー進行
            timeCnt += Time.deltaTime;
            //想定時間に到達した時点で「Debug.Break()」
            if (timeCnt >= stopTime)
            {
                WritePos = Camera.transform.position.x;

                if (Result == 1)
                    Clear.transform.position = new Vector3(WritePos, 2.0f, 0);
                if (Result == 2)
                    Miss.transform.position = new Vector3(WritePos, 2.0f, 0);
            }
        }
    }
}
