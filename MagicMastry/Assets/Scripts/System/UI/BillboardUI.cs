using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour {
    public NameDisplay nameDisplay;
    public BarGaugeControler gauge;
    private Camera targetCamera;
    private GameObject parent;
    void Start()
    {
    }
    // Update is called once per frame
    void Update ()
    {
        targetCamera = Camera.main;
        if (parent)
        {
            this.GetComponent<Canvas>().enabled = 8 < (this.transform.position - parent.transform.position).magnitude;
            this.transform.position = parent.transform.position + new Vector3(0, 2, 0);
        }
        BillboardRotate();
    }

    //ビルボードのための回転（カメラのほうを見る
    void BillboardRotate()
    {
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.back, targetCamera.transform.rotation * Vector3.up);
    }

    //名前表示の関数
    public void setValue(float max, float value)
    {
        gauge.setValue(max, value);
    }

    //ゲージセットの関数
    //最大値と現在値からゲージの値を変更
    public void setName(string _name)
    {
        nameDisplay.setName(_name);
    }

    //親オブジェクトのセット
    //セットしたオブジェクトに追随する
    public void setParent(GameObject obj)
    {
        parent = obj;
    }
}
