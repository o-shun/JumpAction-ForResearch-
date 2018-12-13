using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    Vector2 PlayerVector; //プレイヤーのベクトルを収納

    float PlayerLimit = 0.0f; //プレイヤーの最高速度を収納
    float MoveLimit = 5.0f; //歩行時の最高速度
    float DashLimit = 10.0f;　//ダッシュ時の最高速度
    float ForcePower = 10.0f; // 慣性のかかり具合(大きければすぐ最高速に)

    float JumpForce = 1000.0f; //ジャンプ力を収納する

    //ジョイコンのスティックの入力を検出する
    float JoyconHor; //水平方向の検出を収納
    //float JoyconVer; //垂直方向の検出を収納。念のため記載

    void Start()
    {
        //コンポーネント「Rigidbody2D」取得
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //ジョイコンのスティック入力がされているかと、その方向を取得する。
        this.JoyconHor = Input.GetAxis("Horizontal1") * -1; //水平方向。-１…左移動、1…右移動
        //this.JoyconVer = Input.GetAxis("Vertical1"); //垂直方向。念のため記載

        //ジョイコンの←のボタンで、最高速度を上げダッシュができる。押されてない時は最高速度を下げる。
        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            this.PlayerLimit = this.DashLimit; //ダッシュ時の最高速度を代入　
        }
        else
        {
            this.PlayerLimit = this.MoveLimit; //歩行時の最高速度を代入
        }

        //ジョイコンの↓ボタンでジャンプをする
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) && this.rigid2D.velocity.y == 0)
        {
            this.rigid2D.AddForce(transform.up * this.PlayerVector.y); //ジャンプ力の適応
        }
    }

    //FixedUpdate() → 秒間に呼ばれる回数が一定のUpdate()。Rigidbodyの更新はここでやるのが良い。GetKeyはダメ。
    void FixedUpdate()
    {
        this.PlayerVector = Vector2.zero; // 移動速度を初期化
        this.PlayerVector.x = this.PlayerLimit * this.JoyconHor; //ジョイコンの向きを判定
        this.PlayerVector.y = this.JumpForce; //ダッシュ時以外はジャンプ力を統一

        //ダッシュ時に速度が大きいほどでジャンプ力を強くする
        if ((int)this.rigid2D.velocity.x > 5.0f) //右向きの時
        {
            this.PlayerVector.y += ((int)this.rigid2D.velocity.x - 5.0f * (int)this.JoyconHor) * 50.0f;
        }
        else if ((int)this.rigid2D.velocity.x < -5.0f) //左向きの時
        {
            this.PlayerVector.y -= ((int)this.rigid2D.velocity.x - 5.0f * (int)this.JoyconHor) * 50.0f;
        }

        //ジョイコンの指定した方向に力を加える
        //「moveVector - this.rigid2D.velocity」で、最高速度に近づくたび、かける力を弱くする。「this.ForcePower」で効率の調整。
        this.rigid2D.AddForce(transform.right * this.ForcePower * (this.PlayerVector - this.rigid2D.velocity));

        //１フレームごとの速度(velocity)と、ニュートン(AddForceの強さ)の表示用
        //Debug.Log(this.rigid2D.velocity.x + "、" + this.ForcePower * (moveVector.x - this.rigid2D.velocity.x));
    }
}