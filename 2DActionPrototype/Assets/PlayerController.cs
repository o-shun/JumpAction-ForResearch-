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
    float DashForce = 50.0f; //ダッシュ時のベクトルを収納する
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

            //助走がついた時にジャンプ力を増す様に
            if (this.JumpVelocity > 5.0f && Input.GetKey(KeyCode.X))
            {
                this.JumpForce = (this.JumpVelocity - 5.0f) * 50.0f + JumpLowest;
            }
            else if(this.JumpVelocity < -5.0f && Input.GetKey(KeyCode.X))
            {
                this.JumpForce = (this.JumpVelocity * (-1) - 5.0f) * 50.0f + JumpLowest;
            }
            else
            {
                this.JumpForce = this.JumpLowest; //ダッシュ時以外のジャンプ力はデフォルトに統一
            }

            this.rigid2D.AddForce(transform.up * this.JumpForce); //ジャンプ力の適応
            this.JumpOnGround = false; //地面から離れたことを収納
        }

        //「十字右キーが押されている時」に右へ移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (this.rigid2D.velocity.x < this.PlayerLimit)
            {
                this.rigid2D.AddForce(transform.right * this.PlayerForce);
            }
        }

        //「十字左キーが押されている時」に左へ移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.rigid2D.velocity.x > this.PlayerLimit * -1)
                this.rigid2D.AddForce(transform.right * this.PlayerForce * -1);
        }

        //「Xキーが押されている時」にダッシュ
        if (Input.GetKey(KeyCode.X))
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