using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SliderText : MonoBehaviour {

    [SerializeField]
    Slider slider;

    [SerializeField]
    string beforeText; //数字の前に表示するテキスト

    [SerializeField]
    string afterText; //数字の後に表示するテキスト

    Text text;

	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();

        UnityAction<float> action = new UnityAction<float>(ChangeText);

        //イベントの登録
        slider.onValueChanged.AddListener(action);

        //テキストの初期変更
        ChangeText(slider.value);

	}


    //テキストの変更
    void ChangeText(float value) {
        //テキストを常に変更
        text.text = beforeText + slider.value + afterText;
    }



}
