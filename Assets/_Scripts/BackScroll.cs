using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class BackScroll : MonoBehaviour {
	public float speed;
	[Header("�f�b�h���C��")]public�@float deadLine;
	[Header("���X�|�[�����C��")]public float respawnLine;
	RectTransform rectTransform;

    private void Start() {
		rectTransform = GetComponent<RectTransform>();    
    }

	//�������ɗ����
    void FixedUpdate() {
			rectTransform.Translate(0, speed, 0);
			if (rectTransform.anchoredPosition.y > deadLine) {
				rectTransform.anchoredPosition = new Vector3(0, respawnLine, 0);
			}
		}
	}
