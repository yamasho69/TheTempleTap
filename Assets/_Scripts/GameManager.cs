using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {
	// 定数定義
	private const int MAX_HEART = 30;		// ハート最大数
	//private const int RESPAWN_TIME = 5;// オーブが発生する秒数
	private const int MAX_LEVEL = 5;	// 最大プレイスレベル

	// データセーブ用キー
	private const string KEY_SCORE = "SCORE";	// スコア
	private const string KEY_LEVEL = "LEVEL";	// レベル
	private const string KEY_HEART = "HEART";		// ハート数
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

	//public AudioClip getScoreSE;		// 効果音：スコアゲット
	public AudioClip levelUpSE;			// 効果音：レベルアップ
	public AudioClip clearSE;			// 効果音：クリア

	// メンバ変数
	private int score = 0;			// 現在のスコア
	private int nextScore = 10;		// レベルアップまでに必要なスコア

	private int currentHeart = 0;		// 現在のハート数

	public int placeLevel = 0;	// プレイスのレベル

	private DateTime lastDateTime;	// 前回ハートを生成した時間

	private int[] nextScoreTable = new int[] {20,30,40,50,99,999};
									// レベルアップ値
	private AudioSource audioSource;// オーディオソース

	private int numOfHeart;         // まとめて生成するハートの数

	public Text placeText;
	public string [] placeString;
	public ChangeYukkuriButton cYB;
	public ParticleSystem likeEffect;

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
		//imageplace.GetComponent<placeManager> ().SetplacePicture (placeLevel);
		//imageplace.GetComponent<placeManager> ().SetplaceScale (score, nextScore);
		cYB.BackYukkuri();//後ろのゆっくりを表示
		RefreshScoreText ();
	}
	
	// Update is called once per frame
	void Update () {
		// まとめて生成するオーブがあれば生成
		while (numOfHeart > 0) {
			Invoke ("CreateNewHeart", 0.1f * numOfHeart);
			numOfHeart--;
		}
	}

	// バックグラウンドへの移行時と復帰時（アプリ起動時も含む）に呼び出される
	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			// アプリがバックグラウンドへ移行
		} else {
			// バックグラウンドから復帰
			// 時間の復元
			string time = PlayerPrefs.GetString(KEY_TIME, "");
			if (time == "") {
				lastDateTime = DateTime.UtcNow;
			} else {
				long temp = Convert.ToInt64 (time);
				lastDateTime = DateTime.FromBinary (temp);
			}

			numOfHeart = 0;
			// 時間におるオーブ自動生成
			/*TimeSpan timeSpan = DateTime.UtcNow - lastDateTime;
			if (timeSpan >= TimeSpan.FromSeconds (RESPAWN_TIME)) {
				while (timeSpan > TimeSpan.FromSeconds (RESPAWN_TIME)) {
					if (numOfOrb < MAX_ORB) {
						numOfOrb++;
					}
					timeSpan -= TimeSpan.FromSeconds (RESPAWN_TIME);
				}
			}*/
		}
	}

	// 新しいオーブの生成(タップしたときに呼び出される)
	public void CreateNewHeart () {
		lastDateTime = DateTime.UtcNow;
		if (currentHeart >= MAX_HEART) {
			return;
		}
		CreateHeart ();
		currentHeart++;

		SaveGameData ();
	}

	// ハート生成
	public void CreateHeart () {
		/*
		GameObject orb = (GameObject)Instantiate (heartPrefab);
		orb.transform.SetParent (canvasGame.transform, false);
		orb.transform.localPosition = new Vector3 (
			UnityEngine.Random.Range (-100.0f, 100.0f),
			UnityEngine.Random.Range (-300.0f, -450.0f),
			0f);
		
		// ハートの種類を設定
		int kind = UnityEngine.Random.Range(0, placeLevel + 1);
		switch (kind) {
		case 0:
			orb.GetComponent<HeartManager> ().SetKind (HeartManager.HEART_KIND.PINK);
			break;
		case 1:
			orb.GetComponent<HeartManager> ().SetKind (HeartManager.HEART_KIND.SILVER);
			break;
		case 2:
			orb.GetComponent<HeartManager> ().SetKind (HeartManager.HEART_KIND.GOLD);
			break;
		}

		orb.GetComponent<HeartManager> ().FlyHeart ();*/

		//audioSource.PlayOneShot (getScoreSE);
		// ゆっくりアニメ再生
		//AnimatorStateInfo stateInfo = 
		//tapYukkuri[yukkuriNumber].GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);
		/*if (stateInfo.fullPathHash == Animator.StringToHash ("Base Layer.get@ImageMarisa")) {
			// すでに再生中なら先頭から
			tapYukkuri[yukkuriNumber].GetComponent<Animator> ().Play (stateInfo.fullPathHash, 0, 0.0f);
		} else {*/
		likeEffect.Play();
		tapYukkuri[yukkuriNumber].GetComponent<Animator> ().SetTrigger ("isGetScore");
		//}
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
		currentHeart--;
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
				imagePlace[placeLevel].SetActive(false);//今のプレイスを消す
				placeLevel++;
				imagePlace[placeLevel].SetActive(true);//次のプレイスを出す
				score = 0;
				placeLevelUpEffect ();
				nextScore = nextScoreTable [placeLevel];
				placeText.text = "ゆっくりプレイス：" + placeString[placeLevel];
				cYB.BackYukkuri();
			}
		}
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
		PlayerPrefs.SetInt (KEY_HEART, currentHeart);
		PlayerPrefs.SetString (KEY_TIME, lastDateTime.ToBinary ().ToString ());
		PlayerPrefs.Save ();
	}

	//ランダムで音声再生
	public void RandomizeSfx(params AudioClip[] clips) {
		var randomIndex = UnityEngine.Random.Range(0, clips.Length);
		audioSource.PlayOneShot(clips[randomIndex]);
	}
}
