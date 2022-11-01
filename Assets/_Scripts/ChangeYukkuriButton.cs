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
    // オブジェクト参照
    public GameObject gameManager;// ゲームマネージャー
    public GameObject[] yukkuris;//ゆっくり入れる 
    public string [] yukkuriText;//ボタンの文字
    public Text buttonText;
    public Sprite[] yukkuriButtons;
    public Image buttonImg;
    private GameManager gm;
    public GameObject[] backYukkuris; //後ろのゆっくり入れる。
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
                backYukkuris[backYukkuriNum].SetActive(false);//タップできるゆっくりは後ろに表示しない
                backYukkuriNum++;
                continue;
            }
            backYukkuris[backYukkuriNum].SetActive(true);//表示されていないゆっくりを表示
            backYukkuriNum++;
        }
    }
}
