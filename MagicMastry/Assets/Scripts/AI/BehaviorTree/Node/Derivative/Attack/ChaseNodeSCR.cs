using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブザーバーパターン用の初期設定.
[RequireComponent(typeof(BehaviorTree_ObseverSCR))]
[RequireComponent(typeof(BehaviorTree_SubjectSCR))]

public class ChaseNodeSCR : BehaviorTreeNodeSCR
{
    [SerializeField, Tooltip("追跡者")]
    GameObject m_chaser;
    [SerializeField, Tooltip("逃亡者")]
    GameObject m_target;
    /*------------------------------------------------------*/
    // プレイヤー内のグローバル変数
    /*------------------------------------------------------*/
    [SerializeField]
    TerminalSCR m_terminalSCR;
    /*------------------------------------------------------*/


    // 移動速度"
    float m_speed = 5.0f;

    /**
    * @brief Startと酷似する関数
    * @author shinji
    * @date 10/30
    */
    public override void Setup()
    {
        // プレイヤー用のグローバル変数から取得する.
        m_target = m_terminalSCR.GetTarget();
    }

    /**
     * @brief Updateと酷似する関数
     * @author shinji
     * @date 10/30
     */
    public override void DoAction()
    {
        // ターゲットがnullの場合は終了する.
        if (m_target == null)
        {          
            m_subjectSCR.Notify(-1);
            return;
        }

        // 1.ターゲットの座標を取得する.
        Vector3 targetPosition;
        targetPosition = m_target.transform.position;

        // 2.一定距離以内に入った場合.
        float distance = Vector3.Distance(targetPosition, gameObject.transform.position);
        if (distance < 3.0f)
        {
            m_subjectSCR.Notify();
            //m_target = null;
            return;
        }

        // 3.ターゲットの方向を向く.
        m_chaser.transform.LookAt(targetPosition);

        // 4.ターゲットの方向に移動する.
        Vector3 deltaDistance;
        deltaDistance = m_chaser.transform.forward * m_speed * Time.deltaTime;
        m_chaser.transform.position += deltaDistance;
    }
}