using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIWriter : MonoBehaviour 
{
    GameObject player;
    GameObject text;

	void Start () 
    {
        //Find関数で各オブジェクトの呼び出し
        this.player = GameObject.Find("Player");
        this.text = GameObject.Find("Text");
	}

	void Update () 
    {
        //lengthに「Player」の現在の横移動ベクトルを収納し、テキストで更新表示
        float length = this.player.GetComponent<Rigidbody2D>().velocity.x;
        this.text.GetComponent<Text>().text = length.ToString("F3");
    }
}
