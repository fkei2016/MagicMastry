using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

    public enum GameScene
    {
        Title,
        Select,
        MagicSelect,
        Play,
        Result
    }

    [SerializeField]
    private GameScene nextScene;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GoToScene();
        }
    }

    // Use this for initialization
    void GoToScene() {

        SceneManager.LoadScene(nextScene.ToString());
    }
}
