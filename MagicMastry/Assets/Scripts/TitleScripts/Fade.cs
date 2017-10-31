using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    [SerializeField]
    private Image image;

    bool flag = true;

    Color alpha = new Color(0, 0, 0, 0.01f);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(image.color.a <= 0)
        {
            flag = true;
        }
        if(image.color.a >= 1.0f)
        {
            flag = false;
        }


        if (flag)
        {
            image.color += alpha;
        }
        else
        {
            image.color -= alpha;
        }

    }
}
