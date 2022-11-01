using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public  class ChangeYukkuriButton : MonoBehaviour
{
    // �I�u�W�F�N�g�Q��
    public GameObject gameManager;// �Q�[���}�l�[�W���[
    public GameObject[] yukkuris;//����������� 
    public string [] yukkuriText;//�{�^���̕���
    public Text buttonText;
    public Sprite[] yukkuriButtons;
    public Image buttonImg;
    private GameManager gm;
    public GameObject[] backYukkuris; //���̂����������B
    void Start()
    {
         gm = gameManager.GetComponent<GameManager>();
    }

    public void OnClick() {
        if(gm.placeLevel>gm.yukkuriNumber)
            gm.yukkuriNumber++;
        else {
            gm.yukkuriNumber = 0;
        }
        ChangeYukkuri();
        Debug.Log(gm.yukkuriNumber);
    }

    void ChangeYukkuri(){
        for(int i = 0; i < yukkuris.Length; i++) {
            yukkuris[i].SetActive(false);
        }
        yukkuris[gm.yukkuriNumber].SetActive(true);
        buttonText.text = yukkuriText[gm.yukkuriNumber];
        buttonImg.sprite = yukkuriButtons[gm.yukkuriNumber];
        BackYukkuri();
    }

    public  void BackYukkuri() {
        int backYukkuriNum = 0;
        while(gm.placeLevel >= backYukkuriNum) {
            if(gm.yukkuriNumber == backYukkuriNum) {
                backYukkuris[backYukkuriNum].SetActive(false);//�^�b�v�ł���������͌��ɕ\�����Ȃ�
                backYukkuriNum++;
                continue;
            }
            backYukkuris[backYukkuriNum].SetActive(true);//�\������Ă��Ȃ���������\��
            backYukkuriNum++;
        }
    }
}
