using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float PlayerLimit = 10.0f;　// 最高速度
    float ForcePower = 10.0f; // 慣性のかかり具合(大きければすぐ最高速に)

    float JumpForce = 1000.0f;

    //ジョイコンのスティックの入力を検出する
    float JoyconHor; //水平方向の検出を収納
    float JoyconVer; //垂直方向の検出を収納

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.JoyconHor = Input.GetAxis("Horizontal1") * -1; //-１…右移動、1…左移動
        this.JoyconVer = Input.GetAxis("Vertical1");
    }

    //FixedUpdate() → 秒間に呼ばれる回数が一定のUpdate()。Rigidbody系はここでやるのが良い。
    void FixedUpdate()
    {
        Vector2 moveVector = Vector2.zero; // 移動速度を収納
        moveVector.x = this.PlayerLimit * this.JoyconHor; //ジョイコンの向きを判定

        //ジョイコンの指定した方向に力を加える
        this.rigid2D.AddForce(transform.right * this.ForcePower * (moveVector - this.rigid2D.velocity));
        //「moveVector - this.rigid2D.velocity」で、最高速度に近づくたび、かける力を弱くする。「this.ForcePower」で効率の調整。

        ////ジョイコン左の↓ボタンでジャンプをする
        //if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        //{
        //    this.rigid2D.AddForce(transform.up * this.JumpForce); //ジャンプ力の適応
        //}
    }

    //デバッグ用表示
    //void OnGUI()
    //{
    //    GUILayout.Label((this.rigid2D.velocity).ToString());
    //}
}