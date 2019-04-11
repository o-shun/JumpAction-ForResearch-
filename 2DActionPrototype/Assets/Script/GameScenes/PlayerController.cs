using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector2 PlayerVec; //プレイヤーのベクトルを収納する
    Vector2 PlayerVelo; //プレイヤーの速度を収納する

   　Rigidbody2D rigid2D; //コンポーネント「Rigidbody2D」を収納する

    //X軸
    public float PlayerLimit = 0.0f; //プレイヤーの最高速度を収納
    float MoveLimit = 5.0f; //歩行時の最高速度
    float DashLimit = 10.0f;　//ダッシュ時の最高速度
    float ForcePower = 10.0f; // 慣性のかかり具合(大きければすぐ最高速に)

    //Y軸
    float JumpForce = 1000.0f; //ジャンプ力を収納する
    bool JumpKeyDown = false; //ジャンプボタンが押されたかを判断する
    private bool isJump = false; //ジャンプが開始したかを判断する(テスト用)
    private float timeCnt = 0.0f; //ジャンプを開始してからの時間経過を収納する(テスト用)
    private float stopTime = 0.0f; //ジャンプの頂点に到達する時間を収納する(テスト用)
    Vector2 ForecastPos = new Vector2(0, 0); //ジャンプ後の予測座標を収納する

    //ジョイコンのスティックの入力検出
    float JoyconHor; //水平方向の検出を収納
    //float JoyconVer; //垂直方向の検出を収納。念のため記載int

    void Start()
    {
        //コンポーネント「Rigidbody2D」取得
        this.rigid2D = GetComponent<Rigidbody2D>();
        //オブジェクト「Player」取得
    }

    void Update()
    {
        //ジョイコンのスティック入力がされているかと、その方向を取得する。
        this.JoyconHor = Input.GetAxis("Horizontal1") * -1; //水平方向。-１…左移動、1…右移動
        //this.JoyconVer = Input.GetAxis("Vertical1"); //垂直方向。念のため記載

        //ジャンプ中じゃない時にできること
        if (this.rigid2D.velocity.y == 0)
        {
            //ジョイコンの←のボタンで、最高速度を上げダッシュができる。押されてない時は最高速度を下げる。
            if (Input.GetKey(KeyCode.Joystick1Button0))
            {
                this.PlayerLimit = this.DashLimit; //ダッシュ時の最高速度を代入
            }
            else
            {
                this.PlayerLimit = this.MoveLimit; //歩行時の最高速度を代入
            }

            //ジョイコン↓ボタンでジャンプをする 
            if (Input.GetKeyDown(KeyCode.Joystick1Button1) && JumpKeyDown == false)
            {
                PlayerVelo.y = (PlayerVec.y / rigid2D.mass) * Time.fixedDeltaTime; //ジャンプのベクトルから初速度を計算(ジャンプが頂点に到達した時と仮定)
                var t = PlayerVelo.y / (-Physics2D.gravity.y * rigid2D.gravityScale); //ジャンプが頂点に到達した時までに要する時間を計算する
                stopTime = t;

                //ジャンプが頂点に到達した時の座標を計算し表示する
                var y = transform.position.y + (PlayerVelo.y * t) - 0.5f * (-Physics2D.gravity.y * rigid2D.gravityScale) * Mathf.Pow(t, 2.0f);
                var x = transform.position.x + (PlayerVelo.x * t);
                ForecastPos = new Vector2(x, y);
                Debug.Log(t.ToString("F2") + "秒後の座標は" + ForecastPos.ToString("F5"));
                Debug.Log(transform.position.x);

                JumpKeyDown = true; //ジャンプ開始
                isJump = true; //ジャンプボタンが押された
                timeCnt = 0.0f; //ジャンプが開始された時点でタイマー開始

                //ジャンプ時の最高速度を代入(ダッシュ時)
                if ((int)this.rigid2D.velocity.x < -5.0f || (int)this.rigid2D.velocity.x > 5.0f)
                {
                    this.PlayerLimit = Mathf.Abs(rigid2D.velocity.x);
                }
            }

            //中断コマンド(ジャンプボタン＋ダッシュボタン＋マイナス(orプラス))
            if (Input.GetKey(KeyCode.Joystick1Button0) && Input.GetKey(KeyCode.Joystick1Button1) && Input.GetKey(KeyCode.Joystick1Button8))
                SceneManager.LoadScene("LevelSelect");
        }

        //ジャンプが始まった場合(テスト用にジャンプが頂点到達したら一時停止する)
        if (isJump)
        {
            //タイマー進行
            timeCnt += Time.deltaTime;
            //想定時間に到達した時点で「Debug.Break()」
            if (timeCnt >= stopTime)
            {
                isJump = false;
                Debug.Log("Y軸での予測との誤差：" + (ForecastPos.y - transform.position.y).ToString("F5"));
                //Debug.Break(); //テスト用画面停止
            }
        }
    }

    //FixedUpdate() → 秒間に呼ばれる回数が一定のUpdate()。Rigidbodyの更新はここでやるのが良い。GetKeyはダメ。
    void FixedUpdate()
    {
        if(this.JoyconHor != -1)
            this.PlayerVelo.x = this.PlayerLimit * this.JoyconHor; //ジョイコンの向きを判定

        //ジャンプキーが押された時、Y軸方向に「AddForce」
        if (JumpKeyDown)
        {
            this.rigid2D.AddForce(Vector2.up * this.PlayerVec.y);
            JumpKeyDown = false;
        }

        this.PlayerVec.y = this.JumpForce; //ダッシュ時以外はジャンプ力を統一

        //ダッシュ時に速度が大きいほどでジャンプ力を強くする
        if ((int)this.rigid2D.velocity.x < -5.0f || (int)this.rigid2D.velocity.x > 5.0f)
        {
            var number = (Mathf.Round(Mathf.Abs(this.rigid2D.velocity.x) * 100)) / 100;
            this.PlayerVec.y = Mathf.FloorToInt(this.JumpForce + ((int)number - 5.0f) * 50.0f);
        }

        //ジョイコンの指定した方向に力を加える
        //「moveVector - this.rigid2D.velocity」で、最高速度に近づくたび、かける力を弱くする。「this.ForcePower」で効率の調整。
        this.rigid2D.AddForce(transform.right * this.ForcePower * (this.PlayerVelo - this.rigid2D.velocity));

    }
}