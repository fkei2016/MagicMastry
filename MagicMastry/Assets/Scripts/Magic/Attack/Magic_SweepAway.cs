using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_SweepAway : Magic_Attack
{

	// Use this for initialization
	void Start () {
		
	}

    public override void CastSpell()
    {
        Debug.Log("Sweep away was invoked!");
    }
}
