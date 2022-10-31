using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class ChangeYukkuriButton : MonoBehaviour
{
    // �I�u�W�F�N�g�Q��
    public GameObject gameManager;// �Q�[���}�l�[�W���[
    public GameObject[] yukkuris;//����������� 
    public string [] yukkuriText;//�{�^���̕���
    public Text buttonText;
    public Sprite[] yukkuriButtons;
    public Image buttonImg;
    private GameManager gm;
    void Start()
    {
         gm = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        
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

        /*
        //0�͂܂肳
        if (gm.yukkuriNumber == 0) {
            
        }
        //1�͂ꂢ��
        else if (gm.yukkuriNumber == 1) {

        }
        //2�͂܂肿��
        else if (gm.yukkuriNumber == 2) {

        }
        //3�͂ꂢ�݂�
        else if (gm.yukkuriNumber == 3) {

        }
        //4�͂݂��
        else if (gm.yukkuriNumber == 4) {

        }
        //5�͂킳�ꂢ�݂�
        else {

        }*/
    }
}
