using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySCR : PlayerBase
{
    public override void Awake()
    {
        base.Awake();

        WeakMagicList.LoadMagic(magicData.magic1, 1, this);
    }

    public override void Update()
    {
        base.Update();
    }
}