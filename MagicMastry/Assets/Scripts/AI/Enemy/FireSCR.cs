using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSCR : BehaviorTreeNodeSCR
{
    // データの中継地点.
    [SerializeField]
    TerminalSCR m_terminalSCR;

    // 追跡対象.
    GameObject m_targetObject;

    // 視界.
    float m_sensorRange = 50.0f;

    // 自分自身.
    GameObject m_myself;

    public override void Setup()
    {
        // 1.オブジェクトを取得する.
        {
            if (m_targetObject == null)
                m_targetObject = m_terminalSCR.GetTarget();
            if (m_myself == null)
                m_myself = m_terminalSCR.GetMyself();
        }

        // 2.座標を取得する.
        Vector3 currentPos;
        Vector3 targetPos;
        {
            currentPos = m_myself.transform.position;
            targetPos = m_targetObject.transform.position;
        }

        // 3.距離を取得する.
        Vector3 distance;
        float range;
        {
            distance = targetPos - currentPos;
            range = Mathf.Sqrt(distance.x * distance.x + distance.z + distance.z);
        }

        // 4.視界に居る場合→攻撃する.
        if (range < m_sensorRange)
        {
            Fire();
        }
        else
        {
            // 5.終了する.
            Notify(-1);
        }
    }

    private void Fire()
    {
        // 1.相手を見る.
        m_myself.transform.LookAt(m_targetObject.transform.position);

        // 2.座標を設定する.
        Vector3 startPos;
        startPos = m_myself.transform.position;
        Vector3 endPos;
        endPos = m_targetObject.transform.position;

        // 3.レイを飛ばす.
        Ray ray;
        ray = new Ray(startPos, endPos - startPos);

        // 4.衝突を確認する.
        RaycastHit hit = new RaycastHit();
        if (!Physics.Raycast(ray, out hit))
            Notify(-1);

        // 5.ターゲットと衝突する場合.
        if (hit.collider.gameObject == m_targetObject)
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1.0f);


            /*---------------------------*/
            // 攻撃する.
            m_enemyAttackSCR.DoAction();
            /*---------------------------*/



            Notify();
        }
        // 6.ターゲットと衝突しない場合.
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 1.0f);
            Notify(-1);
        }       
    }

    [SerializeField]
    private EnemyAttackSCR m_enemyAttackSCR;


}