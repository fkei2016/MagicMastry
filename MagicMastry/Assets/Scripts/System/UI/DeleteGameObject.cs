using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGameObject : MonoBehaviour {

    [SerializeField]
    GameObject deleteObject; //消去オブジェクト

	public void DestroyObject() {
        Destroy(deleteObject);
    }

}
