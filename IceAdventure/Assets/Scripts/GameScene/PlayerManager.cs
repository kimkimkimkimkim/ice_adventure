using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	//オブジェクト参照
	public GameObject gameManager; //ゲームマネージャー

	//グローバル変数
	public LayerMask blockLayer; //ブロックレイヤー

	//メンバ変数
	private Rigidbody2D rbody; //プレイヤー制御用のrigidbody2d
	private Vector3 nowPos; //プレイヤーの現在の位置
	private const float MOVE_SPEED = 3; //移動速度固定値
	private float moveSpeed; //プレイヤーの移動速度
	private float jumpPower = 400; //ジャンプの力
	private bool goJump = false; //ジャンプしたかどうか
	private bool canJump = false; //ブロックに設置しているかどうか

	public enum MOVE_DIR //移動方向定義
	{
		STOP,
		LEFT,
		RIGHT,
	};

	private MOVE_DIR moveDirection = MOVE_DIR.STOP; //移動方向

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		canJump = Physics2D.Linecast (transform.position - (transform.right * 0.1f),
			transform.position - (transform.up * 0.1f), blockLayer) ||
		Physics2D.Linecast (transform.position + (transform.right * 0.1f),
			transform.position - (transform.up * 0.1f), blockLayer);

		//何も押してないとき
		moveDirection = MOVE_DIR.STOP;

		//右方向キー
		if (Input.GetKey (KeyCode.RightArrow)) {
			moveDirection = MOVE_DIR.RIGHT;
		}

		//左方向キー
		if (Input.GetKey (KeyCode.LeftArrow)) {
			moveDirection = MOVE_DIR.LEFT;
		}

		//上方向キー
		if (Input.GetKey (KeyCode.UpArrow)) {
			if (canJump) {
				goJump = true;
			}
		}

	}

	void FixedUpdate(){
		//移動方向で処理を分岐
		switch (moveDirection) {
		case MOVE_DIR.STOP: //停止
			moveSpeed = 0;
			break;
		case MOVE_DIR.LEFT: //左に移動
			moveSpeed = MOVE_SPEED * -1;
			transform.localScale = new Vector2 (-0.15f, 0.15f);
			break;
		case MOVE_DIR.RIGHT: //右に移動
			moveSpeed = MOVE_SPEED;
			transform.localScale = new Vector2 (0.15f, 0.15f);
			break;
		}

		rbody.velocity = new Vector2 (moveSpeed, rbody.velocity.y);

		//ジャンプ処理
		if (goJump) {
			rbody.AddForce (Vector2.up * jumpPower);
			goJump = false;
		}
	}



	void OnCollisionStay2D(Collision2D col){
		canJump = true;
	}

	void OnCollisionExit2D(Collision2D col){
		canJump = false;
	}

	//衝突処理
	void OnTriggerEnter2D(Collider2D col){

		//プレイ中でなければ衝突判定は行わない
		if (gameManager.GetComponent<GameManager> ().gameMode != GameManager.GAME_MODE.PLAY) {
			return;
		}

		if(col.gameObject.tag == "Trap"){
			gameManager.GetComponent<GameManager> ().GameOver ();
			DestroyPlayer ();
		}

		if (col.gameObject.tag == "Goal") {
			gameManager.GetComponent<GameManager> ().GameClear ();
		}

		if (col.gameObject.tag == "Snow") {
			col.gameObject.GetComponent<SnowManager> ().GetSnow ();
		}
	}

	//プレイヤーオブジェクト削除処理
	void DestroyPlayer(){
		Destroy (this.gameObject);
	}
}
