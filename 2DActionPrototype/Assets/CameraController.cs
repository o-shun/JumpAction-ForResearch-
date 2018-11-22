using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        //カメラのX軸の位置を「Player」に合わせる
        transform.position = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
