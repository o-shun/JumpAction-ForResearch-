using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEventer : MonoBehaviour
{
    public GameObject MoneyPrefab; //ゴール時に落下物を収納する
    public float DropPos = 0.0f; //落下物をランダム生成する位置の中心を収納する
    float span = 0.25f; //Prefabが生産されるスパンを設定
    float delta = 0.0f; //経過時間を収納

    void Start()
    {

    }

    void Update()
    {
        //Playerの終了位置が収納されたら生成開始
        if ((int)DropPos != 0)
        {
            //時間経過をさせる
            this.delta += Time.deltaTime;

            //経過時間がスパンを越した時Prefabを生成
            if (this.delta > this.span)
            {
                this.delta = 0.0f; //経過時間を初期化
                GameObject go = Instantiate(MoneyPrefab) as GameObject; //Prefabを生成
                int px = Random.Range((int)DropPos - 6, (int)DropPos + 7); //落下位置をランダム決定
                go.transform.position = new Vector2(px, 6); //落下位置をPrefabに収納
            }
        }
    }
}
