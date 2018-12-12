using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    float JumpForce = 0.0f; //ジャンプのベクトルを収納する
    float JumpLowest = 1000.0f; //ジャンプのベクトルの下限を収納する
    float MoveForce = 30.0f; //横移動のベクトルを収納する
    float MoveLimit = 5.0f; //横移動時のベクトル上限を収納する
    float DashForce = 40.0f; //ダッシュ時のベクトルを収納する
    float DashLimit = 10.0f; //ダッシュ時のベクトル上限を収納する

    float PlayerForce = 0.0f; //プレイヤーのX軸ベクトルを収納する
    float PlayerLimit = 0.0f; //プレイヤーのX軸ベクトル上限を収納する

    bool JumpOnGround = false; //プレイヤーのジャンプを１回に制御する
    float JumpVelocity = 0.0f; //ジャンプした瞬間の速度を収納する

    public int Brake = 0; //ゲーム終了を受け取る

    //ジョイコンのスティックの入力を検出する
    public float JoyconHor; //水平方向の検出を収納。-１…右移動、1…左移動
    public float JoyconVer; //垂直方向の検出を収納

    void Start()
    {
        //コンポーネント「Rigidbody2D」取得
        this.rigid2D = GetComponent<Rigidbody2D>();

        //０…ダッシュ、１…ジャンプ
        // int == (int)KeyCode.Joystick1Button0
    }

    void Update()
    {
        // Joy-Conのアナログスティックを検出する
        JoyconHor = Input.GetAxis("Horizontal1"); //水平方向
        JoyconVer = Input.GetAxis("Vertical1"); //垂直方向

        //「Zキーが押された時」にジャンプ
        if (Input.GetKey(KeyCode.Joystick1Button1) && this.JumpOnGround)
        {
            //ジャンプした時の速度を収納
            this.JumpVelocity = Mathf.Floor(this.rigid2D.velocity.x); //Floorで少数端数をカット
            //ジャンプの最大値の調整
            if (this.JumpVelocity > 10.0f) 
            {
                this.JumpVelocity = 10.0f;
            }

            //助走がついた時にジャンプ力を増す様に
            if (this.JumpVelocity > 5.0f && Input.GetKey(KeyCode.Joystick1Button0)) //右向きの時
            {
                this.JumpForce = (this.JumpVelocity - 5.0f) * 50.0f + JumpLowest;
            }
            else if(this.JumpVelocity < -5.0f && Input.GetKey(KeyCode.Joystick1Button0)) //左向きの時
            {
                this.JumpForce = (this.JumpVelocity * (-1) - 5.0f) * 50.0f + JumpLowest;
            }
            else
            {
                this.JumpForce = this.JumpLowest; //ダッシュ時以外のジャンプ力は統一
            }

            this.rigid2D.AddForce(transform.up * this.JumpForce); //ジャンプ力の適応
            this.JumpOnGround = false; //地面から離れたことを収納
        }

        //横移動
        if ((int)this.JoyconHor == -1) //「十字右キーが押されている時」に右へ移動
        {
            Debug.Log("Right");
            if (this.rigid2D.velocity.x < this.PlayerLimit) //速度制限
            {
                this.rigid2D.AddForce(transform.right * this.PlayerForce);
            }
        }
        else if ((int)this.JoyconHor == 1) //「十字左キーが押されている時」に左へ移動
        {
            Debug.Log("Left");
            if (this.rigid2D.velocity.x > this.PlayerLimit * -1) //速度制限
            {
                this.rigid2D.AddForce(transform.right * this.PlayerForce * -1);
            }
        }
        else if(this.JumpOnGround)
        {
            this.rigid2D.velocity = Vector2.zero;
        }

        //反発係数を変更できる…。現状保留のメモ用。
        //this.rigid2D.sharedMaterial.friction = 0.0f;

        //プレイヤーの速度と最高速を状況判断で決定
        if (Input.GetKey(KeyCode.Joystick1Button0) && this.JumpOnGround) //「Xキーが押されている時」にダッシュ
        {
            this.PlayerForce = this.DashForce;
            this.PlayerLimit = this.DashLimit;
        }
        else
        {
            this.PlayerForce = this.MoveForce;
            this.PlayerLimit = this.MoveLimit;
        }
    }

    //ジャンプを１回に制御する為に地面衝突を判定する関数
    void OnCollisionEnter2D(Collision2D other)
    {
        //タグ「Ground」で地面に触れた時という制限を追加
        if (other.gameObject.CompareTag("Ground"))
        {
            this.JumpOnGround = true;
        }
    }

    //デバッグ用表示
    //void OnGUI()
    //{
    //    GUILayout.Label((JoyconHor).ToString() + "、" + (JoyconVer).ToString());
    //}
}