using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController_Level : MonoBehaviour
{
    //キーの現在位置番号
    public int KeyPos = 1; //０…ノーリスクレベル、１…リスクレベル、２…ゲットレベル

    //ジョイコンのスティックの入力検出
    public float JoyconHor; //水平方向の検出を収納
    //float JoyconVer; //垂直方向の検出を収納。念のため記載

    //左右それぞれが検出されたかを判断する
    public bool GetDownLeft = false;
    public bool GetDownRight = false;

    //ステージ選択画面に移行する為のディレクターを収納するオブジェクト
    GameObject KeySetter;

    void Start()
    {
        KeySetter = GameObject.Find("SceneDirector");
    }

    void Update()
    {
        transform.position = new Vector3(0, 12.0f, 0);

        //以下デモ版では未使用

        ////ジョイコンのスティック入力がされているかと、その方向を取得する。
        //this.JoyconHor = Input.GetAxis("Horizontal1") * -1; //水平方向。-１…左移動、1…右移動
        ////this.JoyconVer = Input.GetAxis("Vertical1"); //垂直方向。念のため記載

        ////スティックを左に倒した時
        //if (this.JoyconHor  < 0 && !GetDownLeft)
        //{
        //    //スティックが左に倒された事を検出
        //    GetDownLeft = true;

        //    //ポジション別に位置を移動
        //    if (this.KeyPos == 0)
        //    {
        //        KeyPos = 2;
        //        transform.position = new Vector3(5.5f, -4.0f, 0);
        //    }
        //    else if (this.KeyPos == 1)
        //    {
        //        KeyPos = 0;
        //        transform.position = new Vector3(-5.5f, -4.0f, 0);

        //    }
        //    else if (this.KeyPos == 2)
        //    {
        //        KeyPos = 1;
        //        transform.position = new Vector3(0, -4.0f, 0);
        //    }
        //}

        ////スティックを右に倒した時
        //if (this.JoyconHor  > 0 && !GetDownRight)
        //{
        //    //スティックが右に倒された事を検出
        //    GetDownRight = true;

        //    //ポジション別に位置を移動
        //    if (this.KeyPos == 0)
        //    {
        //        KeyPos =1;
        //        transform.position = new Vector3(0, -4.0f, 0);
        //    }
        //    else if (this.KeyPos == 1)
        //    {
        //        KeyPos = 2;
        //        transform.position = new Vector3(5.5f, -4.0f, 0);
        //    }
        //    else if (this.KeyPos == 2)
        //    {
        //        KeyPos = 0;
        //        transform.position = new Vector3(-5.5f, -4.0f, 0);
        //    }
        //}

        ////スティックが降ろされていないかを検出
        //if(this.JoyconHor == 0)
        //{
        //    GetDownLeft = false;
        //    GetDownRight = false;
        //}

        //ジョイコン↓ボタンでステージ決定
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            //キーのいた場所によって、移動するシーンを判断する
            //KeySetter.GetComponent<Keysetter>().SelectScene = this.KeyPos;

            //デモ版
            KeySetter.GetComponent<Keysetter>().SelectScene = 1;

            KeySetter.GetComponent<Keysetter>().LiveScene = 2;
        }
    }
}