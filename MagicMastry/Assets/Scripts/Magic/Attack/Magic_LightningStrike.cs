using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_LightningStrike : Magic_Attack
{

	// Use this for initialization
	void Start () {
		
	}

    public override void CastSpell()
    {
        Debug.Log("Lightning strike was invoked!");
    }
}
