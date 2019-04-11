using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCheck : MonoBehaviour 
{
    //テスト用

    //プレイヤーのY軸位置を取得する
    public float BestYpos = 0.0f;

	void Start ()
    {
		
	}
	
	void Update () 
    {
        //取得済みのY軸位置より、プレイヤーの現在位置が高かった時に「BestYpos」を更新
        if (BestYpos < transform.position.y)
            BestYpos = transform.position.y;
	}
}
