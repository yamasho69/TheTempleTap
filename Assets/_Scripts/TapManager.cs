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

	// オブジェクト参照
	public GameObject gameManager;  // ゲームマネージャー

	public void TapYukkuri() {
		gameManager.GetComponent<GameManager>().CreateNewHeart();
		gameManager.GetComponent<GameManager>().RandomizeSfx(voices);
	}
}
