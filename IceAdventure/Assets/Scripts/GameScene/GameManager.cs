using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//オブジェクト参照
	public GameObject textGameOver; //ゲームオーバーテキスト
	public GameObject textClear; //クリアテキスト

	public enum GAME_MODE //ゲーム状態定義
	{
		PLAY, //プレイ中
		CLEAR, //クリア
	};

	public GAME_MODE gameMode = GAME_MODE.PLAY; //ゲーム状態

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameOver(){
		textGameOver.SetActive (true);
	}

	//ゲームクリア処理
	public void GameClear(){
		gameMode = GAME_MODE.CLEAR;
		textClear.SetActive (true);
	}
}
