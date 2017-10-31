using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1.0f)]
    private float volume = 0.5f;
    [SerializeField]
    private string bgmName;

    // Use this for initialization
    void Start()
    {
        AudioManager.Instance.ChangeVolumeBGM(volume);
        AudioManager.Instance.PlayBGM(bgmName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}