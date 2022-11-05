using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {
	// 定数定義
	private const int MAX_LEVEL = 5;	// 最大プレイスレベル

	// データセーブ用キー
	//private const string KEY_SCORE = "SCORE";	// スコア
	//private const string KEY_LEVEL = "LEVEL";   // レベル

	//bool値をセーブデータとして扱う　https://smartgames.hatenablog.com/entry/2016/08/22/010140
	//private const string KEY_CLEAR = "CLEAR";   //クリア済か

	// オブジェクト参照
	public GameObject heartPrefab;		// ハートプレハブ
	public GameObject lastText;	// ラストテキスト
	public GameObject textScore;		// スコアテキスト
	public GameObject [] imagePlace;		// ゆっくりプレイス
	public GameObject [] tapYukkuri;        // タップするゆっくり
	public int yukkuriNumber = 0; //ゆっくりの種類
	public AudioClip [] levelUpSE;			// 効果音：レベルアップ
	public AudioClip clearSE;           // 効果音：クリア
	public GameObject[] levelUpText;//レベルアップ時のテキスト
	public GameObject particleBox; //パーティクルのフォルダ。レベルアップ時に非表示にする。

	// メンバ変数
	[NonSerialized]public int score = 0;			// 現在のスコア
	private int nextScore = 10;		// レベルアップまでに必要なスコア
	public int placeLevel = 0;  // プレイスのレベル
	public bool isClear;
	public AudioClip bgm;

	private int[] nextScoreTable = new int[] {20,30,40,50,99,100};
	// レベルアップ値
	//private AudioSource audioSource;// オーディオソース
	SoundManager soundManager;

	public Text placeText;//プレイスの状態テキスト
	public string [] placeString;//プレイスの状態テキストのストリング
	public ChangeYukkuriButton cYB;//ゆっくりきりかえボタン
	public FadeController fadeController;

	// Use this for initialization
	void Start () {
		// オーディオソース取得
		GameObject gameObject = GameObject.FindGameObjectWithTag("SoundManager");
		soundManager = gameObject.GetComponent<SoundManager> ();
		soundManager.PlayBgm(bgm);
		// 初期設定
		//score = PlayerPrefs.GetInt (KEY_SCORE, 0);
		//placeLevel = PlayerPrefs.GetInt (KEY_LEVEL, 0);
		//int clearChack = PlayerPrefs.GetInt(KEY_CLEAR);

		score = ES3.Load<int>("SCORE", defaultValue: 0);
		placeLevel = ES3.Load<int>("LEVEL", defaultValue: 0);
		isClear = ES3.Load<bool>("CLEAR", defaultValue:false);


		/*if (clearChack == 1) {
			isClear = true;
        }*/
		if(isClear) {//クリア済の場合
			ClearEffect();
		} else {
			nextScore = nextScoreTable[placeLevel];
		}
		placeText.text = "ゆっくりプレイス：" + placeString[placeLevel];
		imagePlace[placeLevel].SetActive(true);
		cYB.BackYukkuri();//後ろのゆっくりを表示
		RefreshScoreText ();
	}

	// ハート生成
	public void CreateHeart () {
		tapYukkuri[yukkuriNumber].GetComponent<Animator> ().SetTrigger ("isGetScore");
	}

	// ハート入手
	public void GetHeart (int getScore) {

		//クリア済の場合はこの処理だけしてリターン
        if (isClear) {
			score += getScore;
			RefreshScoreText();
			SaveGameData();
			return;
		}


		if (score < nextScore) {
			score += getScore;

			// レベルアップ値を超えないよう制限
			if (score > nextScore) {
				score = nextScore;
			}
			placeLevelUp();
			RefreshScoreText();

			// ゲームクリア判定
			if ((score == nextScore) && (placeLevel == MAX_LEVEL)&&!isClear) {//クリアしてないも条件に追加
				ClearEffect ();
            }
		}
		SaveGameData ();
	}

	// スコアテキスト更新
	void RefreshScoreText () {
		if (!isClear) {
			textScore.GetComponent<Text>().text =
				"ゆっくり度：" + score + " / " + nextScore;
        } else {
			textScore.GetComponent<Text>().text =
				"ゆっくり度：" + score + " / ∞";
		}
	}

	// プレイスのレベル管理
	void placeLevelUp () {
		if (score >= nextScore) {
			if (placeLevel < MAX_LEVEL) {
				particleBox.SetActive(false);
				fadeController.isFadeOut = true;
				soundManager.PlaySe(levelUpSE[placeLevel]);
				levelUpText[placeLevel].SetActive(true);
				Invoke("FadeIn", 3.8f);
			}
		}
	}

	void FadeIn() {
		imagePlace[placeLevel].SetActive(false);//今のプレイスを消す
		placeLevel++;
		imagePlace[placeLevel].SetActive(true);//次のプレイスを出す
		score = 0;
		nextScore = nextScoreTable[placeLevel];
		RefreshScoreText();
		placeText.text = "ゆっくりプレイス：" + placeString[placeLevel];
		cYB.BackYukkuri();
		levelUpText[placeLevel - 1].SetActive(false);
		fadeController.isFadeIn = true;
		particleBox.SetActive(true);
		SaveGameData();//レベルアップ時にセーブしないと、レベルアップ直後にタイトル画面に抜けた時にバグる
	}

	// プレイスが最後まで育った時の演出
	void ClearEffect () {
		lastText.SetActive(true);
		soundManager.PlaySe(clearSE);
		nextScore = nextScoreTable[placeLevel];
		RefreshScoreText();
		isClear = true;
	}

	// ゲームデータをセーブ
	void SaveGameData () {
		/*
		PlayerPrefs.SetInt (KEY_SCORE, score);
		PlayerPrefs.SetInt (KEY_LEVEL, placeLevel);
        if (!isClear) {//クリア済なら1、未クリアなら0をキーに保存
			PlayerPrefs.SetInt(KEY_CLEAR, 0);
        } else { PlayerPrefs.SetInt(KEY_CLEAR, 1); }

		PlayerPrefs.Save ();*/

		ES3.Save<int>("SCORE", score);
		ES3.Save<int>("LEVEL", placeLevel);
		ES3.Save<bool>("CLEAR", isClear);
	}
}
