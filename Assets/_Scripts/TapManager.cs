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

	// �I�u�W�F�N�g�Q��
	public GameObject gameManager;  // �Q�[���}�l�[�W���[

	public void TapYukkuri() {
		gameManager.GetComponent<GameManager>().CreateNewHeart();
		gameManager.GetComponent<GameManager>().RandomizeSfx(voices);
	}
}
