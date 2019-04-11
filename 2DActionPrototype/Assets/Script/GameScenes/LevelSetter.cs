using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetter : MonoBehaviour 
{
    public int GetLevelNumber; //レベルの種類を取得する。０…ノーリスクレベル、１…リスクレベル、２…ゲットレベル
    public int GetStageNumber; //ステージの種類を取得する。難易度

    //各レベルでステージ構成に必要なオブジェクトを取得
    GameObject Land;
    GameObject Goal;
    GameObject Money;
    GameObject Wall;

    void Start () 
    {
        //選択されたレベルとステージを取得
        GetLevelNumber = Keysetter.LevelGetter();
        GetStageNumber = Keysetter.StageGetter();

        //ノーリスクレベルの時
        if (GetLevelNumber == 0)
        {
            Wall = GameObject.Find("Wall");

            if (GetStageNumber == 0)
            {
               Wall.transform.position = new Vector3(15.0f, -2.0f, 0);
            }
            if (GetStageNumber == 1)
            {
                Wall.transform.position = new Vector3(15.0f, -1.9f, 0);
            }
            if (GetStageNumber == 2)
            {
                Wall.transform.position = new Vector3(15.0f, -1.85f, 0);
            }
            if (GetStageNumber == 3)
            {
                Wall.transform.position = new Vector3(15.0f, -1.8f, 0);
            }
            if (GetStageNumber == 4)　
            {
                Wall.transform.position = new Vector3(15.0f, -1.75f, 0);
            }
        }

        //リスクレベルの時
        if (GetLevelNumber == 1)
        {
            Land = GameObject.Find("Land");
            Goal = GameObject.Find("Goal");
            GetStageNumber = Keysetter.StageGetter();

            if (GetStageNumber == 0)
            {
                Goal.transform.position = new Vector3(25.1f, -6.0f, 0);
            }
            if (GetStageNumber == 1)
            {
                Goal.transform.position = new Vector3(25.6f, -6.0f, 0);
            }
            if (GetStageNumber == 2)
            {
                Goal.transform.position = new Vector3(26.1f, -6.0f, 0);
            }
            if (GetStageNumber == 3)
            {
                Goal.transform.position = new Vector3(26.6f, -6.0f, 0);
            }
            if (GetStageNumber == 4)
            {
                Goal.transform.position = new Vector3(26.63f, -6.0f, 0);
            }
        }

        //ゲットレベルの時
        if (GetLevelNumber == 2)
        {
            Money = GameObject.Find("Money");

            if (GetStageNumber == 0)
            {
                Money.transform.position = new Vector3(17f, 5.0f, 0);
            }
            if (GetStageNumber == 1)
            {
                Money.transform.position = new Vector3(17.25f, 5.1f, 0);
            }
            if (GetStageNumber == 2)
            {
                Money.transform.position = new Vector3(17.5f, 5.15f, 0);
            }
            if (GetStageNumber == 3)
            {
                Money.transform.position = new Vector3(17.6f, 5.157f, 0);
            }
            if (GetStageNumber == 4)
            {
                Money.transform.position = new Vector3(17.87f, 5.157f, 0);
            }
        }

    }
	
	void Update () 
    {
		
	}
}
