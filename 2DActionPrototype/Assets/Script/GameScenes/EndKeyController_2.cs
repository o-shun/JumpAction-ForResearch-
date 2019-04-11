using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndKeyController_2 : MonoBehaviour 
{
    //キーの現在位置番号
    public int KeyPos = 0; //０…１、１…２、２…３、３…４、４…５(KeyPosの数値 … キーが指してるステージ番号)

    //ジョイコンのスティックの入力検出
    public float JoyconHor; //水平方向の検出を収納
    //float JoyconVer; //垂直方向の検出を収納。念のため記載

    //左右それぞれが検出されたかを判断する
    public bool GetDownLeft = false;
    public bool GetDownRight = false;

    //現在のレベルを取得し収納する
    int NowLevel;

    void Start () 
    {
        //レベル選択画面で決定したレベルを取得する
        NowLevel = Keysetter.SendLevel;
    }

	void Update () 
    {
        //ジョイコンのスティック入力がされているかと、その方向を取得する。
        this.JoyconHor = Input.GetAxis("Horizontal1") * -1; //水平方向。-１…左移動、1…右移動
        //this.JoyconVer = Input.GetAxis("Vertical1"); //垂直方向。念のため記載


        //スティックを左に倒した時
        if (this.JoyconHor < 0 && !GetDownLeft)
        {
            //スティックが左に倒された事を検出
            GetDownLeft = true;

            //ポジション別に位置を移動
            if (this.KeyPos == 0)
            {
                KeyPos = 1;
                Vector3 pos = transform.position;
                this.gameObject.transform.position = new Vector3(pos.x + 4.7f, pos.y, pos.z);
            }

            else if (this.KeyPos == 1)
            {
                KeyPos = 0;
                Vector3 pos = transform.position;
                this.gameObject.transform.position = new Vector3(pos.x - 4.7f, pos.y, pos.z);
            }
        }

        //スティックを右に倒した時
        if (this.JoyconHor > 0 && !GetDownRight)
        {
            //スティックが右に倒された事を検出
            GetDownRight = true;

            //ポジション別に位置を移動
            if (this.KeyPos == 0)
            {
                KeyPos = 1;
                Vector3 pos = transform.position;
                this.gameObject.transform.position = new Vector3(pos.x + 4.7f, pos.y, pos.z);
            }

            else if (this.KeyPos == 1)
            {
                KeyPos = 0;
                Vector3 pos = transform.position;
                this.gameObject.transform.position = new Vector3(pos.x - 4.7f, pos.y, pos.z);
            }
        }

        //スティックが降ろされていないかを検出
        if (this.JoyconHor == 0)
        {
            GetDownLeft = false;
            GetDownRight = false;
        }

        //ジョイコン↓ボタンで難易度決定
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            if (this.KeyPos == 0) //「もう１度プレイ」を選択した時
            {
                if (NowLevel == 0)
                {
                    SceneManager.LoadScene("NoRiskLevel");
                }
                if (NowLevel == 1)
                {
                    SceneManager.LoadScene("RiskLevel");
                }
                if (NowLevel == 2)
                {
                    SceneManager.LoadScene("GetLevel");
                }
            }
            else if (this.KeyPos == 1) //「タイトルに戻る」を選択した時
            {
                SceneManager.LoadScene("LevelSelect");
            }
        }
    }
}
