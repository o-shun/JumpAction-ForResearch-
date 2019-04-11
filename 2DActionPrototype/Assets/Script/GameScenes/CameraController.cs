//カメラを動かす為のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //オブジェクト「Player」を収納
    public GameObject player;

    void Start()
    {
        //初期位置を収納
        transform.position = new Vector3(0.7f, 0.0f, -15.0f);
    }

    void FixedUpdate()
    {
        //必要画面内しか映さない様に、制限をつける
        if (player.transform.position.x >= 0.7f && player.transform.position.x <= 19f)
        {
            //カメラのX軸の位置を「Player」に合わせる
            transform.position = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }
}