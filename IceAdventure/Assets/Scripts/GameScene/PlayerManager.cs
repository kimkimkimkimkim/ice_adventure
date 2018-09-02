using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	//メンバ変数
	private Rigidbody2D rbody; //プレイヤー制御用のrigidbody2d
	private Vector3 nowPos; //プレイヤーの現在の位置
	private float speed = 15; //プレイヤーの移動速度
	private float jumpPower = 400; //ジャンプの力

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		nowPos = GetComponent<RectTransform> ().localPosition;

		//右方向キー
		if (Input.GetKey (KeyCode.RightArrow)) {
			GetComponent<RectTransform> ().localPosition = new Vector3 (nowPos.x + speed,nowPos.y,nowPos.z);
		}

		//左方向キー
		if (Input.GetKey (KeyCode.LeftArrow)) {
			GetComponent<RectTransform> ().localPosition = new Vector3 (nowPos.x - speed,nowPos.y,nowPos.z);
		}

		//上方向キー
		if (Input.GetKey (KeyCode.UpArrow)) {
			rbody.AddForce (Vector2.up * jumpPower);
		}

	}
}
