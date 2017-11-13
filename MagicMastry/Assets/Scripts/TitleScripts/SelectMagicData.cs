using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMagicData : MonoBehaviour {

    public Sprite sprite; //その魔法の画像
    public string magicName; //魔法名 
    public float waitTime; //再使用時間
    public int damage; //ダメージ量
    public int saveID; //選択時にセーブされる値
    [MultilineAttribute(3)]
    public string comment; //コメント

    // Use this for initialization
    void Start () {
        //自身のイメージを適応させる
        this.GetComponent<Image>().sprite = sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
