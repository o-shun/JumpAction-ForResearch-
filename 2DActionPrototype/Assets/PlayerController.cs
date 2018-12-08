using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    float JumpForce = 0.0f; //ジャンプのベクトルを収納する
    float JumpLowest = 1000.0f; //ジャンプのベクトルの下限を収納する
    float MoveForce = 20.0f; //横移動のベクトルを収納する
    float MoveLimit = 5.0f; //横移動時のベクトル上限を収納する
    float DashForce = 25.0f; //ダッシュ時のベクトルを収納する
    float DashLimit = 10.0f; //ダッシュ時のベクトル上限を収納する

    float PlayerForce = 0.0f; //プレイヤーのX軸ベクトルを収納する
    float PlayerLimit = 0.0f; //プレイヤーのX軸ベクトル上限を収納する

    bool JumpOnGround = false; //プレイヤーのジャンプを１回に制御する
    float JumpVelocity = 0.0f; //ジャンプした瞬間の速度を収納する

    void Start()
    {
        //コンポーネント「Rigidbody2D」取得
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //「Zキーが押された時」にジャンプ
        if (Input.GetKeyDown(KeyCode.Z) && this.JumpOnGround)
        {
            //ジャンプした時の速度を収納
            this.JumpVelocity = Mathf.Floor(this.rigid2D.velocity.x); //Floorで少数端数をカット
            //ジャンプの最大値の調整
            if (this.JumpVelocity > 10.0f) 
            {
                this.JumpVelocity = 10.0f;
            }

            //助走がついた時にジャンプ力を増す様に
            if (this.JumpVelocity > 5.0f && Input.GetKey(KeyCode.X)) //右向きの時
            {
                this.JumpForce = (this.JumpVelocity - 5.0f) * 50.0f + JumpLowest;
            }
            else if(this.JumpVelocity < -5.0f && Input.GetKey(KeyCode.X)) //左向きの時
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
        if (Input.GetKey(KeyCode.RightArrow)) //「十字右キーが押されている時」に右へ移動
        {
            //速度制限をつける
            if (this.rigid2D.velocity.x < this.PlayerLimit)
            {
                this.rigid2D.AddForce(transform.right * this.PlayerForce);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) //「十字左キーが押されている時」に左へ移動
        {
            //速度制限をつける
            if (this.rigid2D.velocity.x > this.PlayerLimit * -1)
            {
                this.rigid2D.AddForce(transform.right * this.PlayerForce * -1);
            }
        }
        else //ブレーキ
        {
            if (this.rigid2D.velocity.x > 0.0f && this.JumpOnGround) //右向きの時
            {
                rigid2D.velocity = Vector2.zero;
            }
            else if (this.rigid2D.velocity.x < 0.0f && this.JumpOnGround) //左向きの時
            {
                rigid2D.velocity = Vector2.zero;
            }
        }


        //プレイヤーの速度と最高速を状況判断で決定
        if (Input.GetKey(KeyCode.X)) //「Xキーが押されている時」にダッシュ
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
}