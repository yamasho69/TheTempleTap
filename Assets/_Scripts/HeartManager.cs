using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class HeartManager : MonoBehaviour
{
	// オブジェクト参照
	private GameObject gameManager; // ゲームマネージャー

	public Sprite[] heartPicture = new Sprite[3]; //ハートの絵

	public enum HEART_KIND {  // オーブの種類を定義
		PINK,
		SILVER,
		GOLD,
	}

	private HEART_KIND heartKind;   // オーブの種類

	// Use this for initialization
	void Start() {
		gameManager = GameObject.Find("GameManager");
	}


	// オーブが飛ぶ
	public void FlyHeart() {
		RectTransform rect = GetComponent<RectTransform>();

		// オーブの軌跡設定
		Vector3[] path = {
			new Vector3(rect.localPosition.x * 4.0f, 300f, 0f),
			new Vector3(0f, 250f, 0f),
		};

		// DOTweenを使ったアニメ作成
		rect.DOLocalPath(path, 0.5f, PathType.CatmullRom)
			.SetEase(Ease.OutQuad)
			.OnComplete(AddheartPoint);
		// 同時にサイズも変更 
		rect.DOScale(
			new Vector3(0.5f, 0.5f, 0f),
			0.5f
		);
	}

	// オーブアニメ終了後にポイント加算処理をする
	void AddheartPoint() {
		switch (heartKind) {
			case HEART_KIND.PINK:
				gameManager.GetComponent<GameManager>().GetHeart(1);
				break;
			case HEART_KIND.SILVER:
				gameManager.GetComponent<GameManager>().GetHeart(5);
				break;
			case HEART_KIND.GOLD:
				gameManager.GetComponent<GameManager>().GetHeart(10);
				break;
		}

		Destroy(this.gameObject);
	}

	// オーブの種類を設定
	public void SetKind(HEART_KIND kind) {
		heartKind = kind;

		switch (heartKind) {
			case HEART_KIND.PINK:
				GetComponent<Image>().sprite = heartPicture[0];
				break;
			case HEART_KIND.SILVER:
				GetComponent<Image>().sprite = heartPicture[1];
				break;
			case HEART_KIND.GOLD:
				GetComponent<Image>().sprite = heartPicture[2];
				break;
		}
	}
}
