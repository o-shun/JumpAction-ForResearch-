using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    float JumpForce = 500.0f; //ジャンプのベクトルを収納する関数
    float MoveForce = 10.0f; //横移動のベクトルを収納する関数
    float MoveLimit = 5.0f; //横移動時のベクトル上限を収納する関数
    float DashForce = 20.0f; //ダッシュ時のベクトルを収納する関数
    float DashLimit = 10.0f; //ダッシュ時のベクトル上限を収納する関数

    float PlayerForce = 0.0f; //プレイヤーのX軸ベクトルを収納する関数
    float PlayerLimit = 0.0f; //プレイヤーのX軸ベクトル上限を収納する関数

    void Start()
    {
        //コンポーネント「Rigidbody2D」取得
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //「Zキーが押された時」にジャンプ
        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.rigid2D.AddForce(transform.up * this.JumpForce);
        }

        //「十字右キーが押されている時」に右へ移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if(this.rigid2D.velocity.x < PlayerLimit)
                this.rigid2D.AddForce(transform.right * this.PlayerForce);
        }

        //「十字左キーが押されている時」に左へ移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.rigid2D.velocity.x > PlayerLimit * -1)
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
}