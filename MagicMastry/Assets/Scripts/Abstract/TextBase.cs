using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBase : MonoBehaviour {

    protected Text text; //自身のテキスト
    protected GameManager gmScript; //ゲーム管理スクリプト

    // Use this for initialization
    public virtual void Start () {
        text = this.GetComponent<Text>();
        gmScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }
}
