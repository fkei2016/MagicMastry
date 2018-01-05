using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class EnemyAttackSCR : EnemyActionSCR
{
    public override void DoAction()
    {
        // ネットワークの確認.
        if (!m_playerBase.CheckActionable()) return;

        // 攻撃する.
        if (m_playerBase.magicData.magic1.waitTime > 0) return;
        m_playerBase.magicData.magic1.action.Invoke();
        m_animator.SetTrigger("Attack");
    }
}