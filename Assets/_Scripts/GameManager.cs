using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {
	// 定数定義
	private const int MAX_LEVEL = 5;	// 最大プレイスレベル

	// データセーブ用キー
	private const string KEY_SCORE = "SCORE";	// スコア
	private const string KEY_LEVEL = "LEVEL";	// レベル
	private const string KEY_TIME = "TIME";		// 時間

	// オブジェクト参照
	public GameObject heartPrefab;		// ハートプレハブ
	public GameObject smokePrefab;		// 煙プレハブ
	public GameObject kusudamaPrefab;	// くす玉プレハブ
	public GameObject canvasGame;		// ゲームキャンバス
	public GameObject textScore;		// スコアテキスト
	public GameObject [] imagePlace;		// ゆっくりプレイス
	public GameObject [] tapYukkuri;        // タップするゆっくり
	public int yukkuriNumber = 0; //ゆっくりの種類
	public AudioClip levelUpSE;			// 効果音：レベルアップ
	public AudioClip clearSE;			// 効果音：クリア

	// メンバ変数
	private int score = 0;			// 現在のスコア
	private int nextScore = 10;		// レベルアップまでに必要なスコア
	public int placeLevel = 0;	// プレイスのレベル

	private int[] nextScoreTable = new int[] {20,30,40,50,99,999};
									// レベルアップ値
	private AudioSource audioSource;// オーディオソース

	public Text placeText;//プレイスの状態テキスト
	public string [] placeString;//プレイスの状態テキストのストリング
	public ChangeYukkuriButton cYB;//ゆっくりきりかえボタン
	public FadeController fadeController;

	// Use this for initialization
	void Start () {
		// オーディオソース取得
		audioSource = this.gameObject.GetComponent<AudioSource> ();
		// 初期設定
		score = PlayerPrefs.GetInt (KEY_SCORE, 0);
		placeLevel = PlayerPrefs.GetInt (KEY_LEVEL, 0);
		placeText.text = "ゆっくりプレイス：" + placeString[placeLevel];
		nextScore = nextScoreTable [placeLevel];
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

		if (score < nextScore) {
			score += getScore;

			// レベルアップ値を超えないよう制限
			if (score > nextScore) {
				score = nextScore;
			}

			placeLevelUp ();
			RefreshScoreText ();

			// ゲームクリア判定
			if ((score == nextScore) && (placeLevel == MAX_LEVEL)) {
				ClearEffect ();
			}
		}
		SaveGameData ();
	}

	// スコアテキスト更新
	void RefreshScoreText () {
		textScore.GetComponent<Text> ().text = 
			"ゆっくり度：" + score + " / " + nextScore;
	}

	// プレイスのレベル管理
	void placeLevelUp () {
		if (score >= nextScore) {
			if (placeLevel < MAX_LEVEL) {
				fadeController.isFadeOut = true;
				audioSource.PlayOneShot(levelUpSE);
				Invoke("FadeIn", 1.5f);
			}
		}
	}

	void FadeIn() {
		imagePlace[placeLevel].SetActive(false);//今のプレイスを消す
		placeLevel++;
		imagePlace[placeLevel].SetActive(true);//次のプレイスを出す
		score = 0;
		nextScore = nextScoreTable[placeLevel];
		placeText.text = "ゆっくりプレイス：" + placeString[placeLevel];
		cYB.BackYukkuri();
		fadeController.isFadeIn = true;
    }

	// レベルアップ時の演出
	void placeLevelUpEffect () {
		GameObject smoke = (GameObject)Instantiate (smokePrefab);
		smoke.transform.SetParent (canvasGame.transform, false);
		smoke.transform.SetSiblingIndex (2);
		audioSource.PlayOneShot (levelUpSE);
		Destroy (smoke, 0.5f);		
	}

	// プレイスが最後まで育った時の演出
	void ClearEffect () {
		GameObject kusudama = (GameObject)Instantiate(kusudamaPrefab);
		kusudama.transform.SetParent (canvasGame.transform, false);
		audioSource.PlayOneShot (clearSE);
	}

	// ゲームデータをセーブ
	void SaveGameData () {
		PlayerPrefs.SetInt (KEY_SCORE, score);
		PlayerPrefs.SetInt (KEY_LEVEL, placeLevel);
		PlayerPrefs.Save ();
	}

	//ランダムで音声再生
	public void RandomizeSfx(params AudioClip[] clips) {
		var randomIndex = UnityEngine.Random.Range(0, clips.Length);
		audioSource.PlayOneShot(clips[randomIndex]);
	}
}
