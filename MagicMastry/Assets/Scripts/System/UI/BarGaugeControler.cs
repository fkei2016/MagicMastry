using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarGaugeControler : MonoBehaviour
{
    private Image gauge;
    // Use this for initialization
    void Start()
    {
        gauge = GetComponent<Image>();
    }

    //０～１までの数字を指定して円ゲージの値を変更
    public void set(float value)
    {
        value = Mathf.Clamp(value, 0.0f, 1.0f);
        gauge.fillAmount = value;
        gauge.color = ValueToColor(value);
    }

    //最大値と現在値から円ゲージの値を変更
    public void setValue(float max, float value)
    {
        value = value / max;
        set(value);
    }

    Color ValueToColor(float value)
    {
        float r = 1 - (Mathf.Clamp(value, 0.5f, 1.0f) * 2.0f - 1.0f);
        float g = Mathf.Clamp(value * 2.0f, 0.0f, 1.0f);
        return new Color(r,g,0);
    }
}
