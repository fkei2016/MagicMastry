using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockControler : MonoBehaviour {
    [SerializeField]
    private Image hand;
    // Use this for initialization
    void Start()
    {
    }

    //０～１までの数字を指定して円ゲージの値を変更
    public void set(float value)
    {
        value = Mathf.Clamp(value, 0.0f, 1.0f);
        hand.transform.rotation = Quaternion.Euler(new Vector3(0,0,360 * value));
    }

    //最大値と現在値から円ゲージの値を変更
    public void setValue(float max, float value)
    {
        value = value / max;
        set(value);
    }
}
