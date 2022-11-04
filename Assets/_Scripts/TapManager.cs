using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class TapManager : MonoBehaviour
{
	public AudioClip [] voices;
	public ParticleSystem pinkLikeEffect;
	public ParticleSystem silverLikeEffect;
	public ParticleSystem goldLikeEffect;

	// オブジェクト参照
	public GameObject gameManager;  // ゲームマネージャー
	SoundManager soundManager;

    private void Start() {
		GameObject gameObject = GameObject.FindGameObjectWithTag("SoundManager");
		soundManager = gameObject.GetComponent<SoundManager>();
	}

    public void TapYukkuri() {
		gameManager.GetComponent<GameManager>().CreateHeart();
		soundManager.RandomizeSfx(voices);

		//https://tech.pjin.jp/blog/2021/03/31/unity_howto_random/
		int a = UnityEngine.Random.Range(0, 10);
		if (a > 3) {
			pinkLikeEffect.Play();
			gameManager.GetComponent<GameManager>().GetHeart(1);
		}else if (a > 0) {
			silverLikeEffect.Play();
			gameManager.GetComponent<GameManager>().GetHeart(3);
		} else {
			goldLikeEffect.Play();
			gameManager.GetComponent<GameManager>().GetHeart(5);
		}
	}
}
