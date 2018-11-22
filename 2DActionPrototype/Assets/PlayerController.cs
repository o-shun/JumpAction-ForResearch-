using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float JumpForce = 500.0f; //ジャンプのベクトルを収納する関数
    float MoveForce = 0.1f; //横移動のベクトルを収納する関数

    void Start()
    {
        //コンポーネント「Rigidbody2D」取得
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //「Zキーが押された時」にジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.rigid2D.AddForce(transform.up * this.JumpForce);
        }

        //「十字右キーが押されている時」に右へ移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(this.MoveForce, 0, 0);
        }

        //「十字左キーが押されている時」に左へ移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(this.MoveForce * -1, 0, 0);
        }
    }
}