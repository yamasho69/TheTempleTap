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
	// �I�u�W�F�N�g�Q��
	private GameObject gameManager; // �Q�[���}�l�[�W���[

	public Sprite[] heartPicture = new Sprite[3]; //�n�[�g�̊G

	public enum HEART_KIND {  // �I�[�u�̎�ނ��`
		PINK,
		SILVER,
		GOLD,
	}

	private HEART_KIND heartKind;   // �I�[�u�̎��

	// Use this for initialization
	void Start() {
		gameManager = GameObject.Find("GameManager");
	}


	// �I�[�u�����
	public void FlyHeart() {
		RectTransform rect = GetComponent<RectTransform>();

		// �I�[�u�̋O�Րݒ�
		Vector3[] path = {
			new Vector3(rect.localPosition.x * 4.0f, 300f, 0f),
			new Vector3(0f, 250f, 0f),
		};

		// DOTween���g�����A�j���쐬
		rect.DOLocalPath(path, 0.5f, PathType.CatmullRom)
			.SetEase(Ease.OutQuad)
			.OnComplete(AddheartPoint);
		// �����ɃT�C�Y���ύX 
		rect.DOScale(
			new Vector3(0.5f, 0.5f, 0f),
			0.5f
		);
	}

	// �I�[�u�A�j���I����Ƀ|�C���g���Z����������
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

	// �I�[�u�̎�ނ�ݒ�
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
