using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_FireBall : Magic_Attack

{

	// Use this for initialization
	void Start () {
		
	}

    public override void CastSpell()
    {
        Debug.Log("Fire ball was invoked!");
    }
}
