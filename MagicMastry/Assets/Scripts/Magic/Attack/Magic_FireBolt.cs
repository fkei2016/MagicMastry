using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_FireBolt : Magic_Attack
{
    public GameObject bullet;

	// Use this for initialization
	void Start () {
		
	}

    public override void CastSpell()
    {
        GameObject fire = GameObject.Instantiate(bullet) as GameObject;
        fire.transform.position = this.gameObject.transform.position;
        fire.transform.rotation = this.gameObject.transform.rotation;

        Debug.Log("Fire bolt was invoked!");
    }
}
