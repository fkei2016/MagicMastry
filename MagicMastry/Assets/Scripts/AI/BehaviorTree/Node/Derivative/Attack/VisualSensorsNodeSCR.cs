using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブザーバーパターン用の初期設定.
[RequireComponent(typeof(BehaviorTree_ObseverSCR))]
[RequireComponent(typeof(BehaviorTree_SubjectSCR))]

public class VisualSensorsNodeSCR : BehaviorTreeNodeSCR
{
    [SerializeField]
    List<GameObject> m_targets = new List<GameObject>();
    // 視野角.
    float m_viewingAngle = 90.0f;

    [SerializeField]
    SphereCollider m_sphereCollider;

    // rayの長さ.
    float m_maxRayDistance;

    /**
     * @brief 初期化
     * @author shinji
     * @date 10/27
     */
    public override void Setup()
    {
        m_maxRayDistance = m_sphereCollider.radius;
    }

    /**
     * @brief 実行する
     * @author shinji
     * @date 10/27
     */
    public override void DoAction()
    {
        Search();
    }


    private void Search()
    {
        foreach (GameObject target in m_targets)
        {
            // 1.rayを発射して衝突を確認する.
            bool hit = false;
            hit = FlyTheRayToTheTarget(target);

            if (hit)
            {
                // プレイヤー用のグローバル変数に追加する.
                m_terminalSCR.SetTarget(target);
                //m_targets.Remove(target);

                m_subjectSCR.Notify();
                return;
            }
        }

        m_subjectSCR.Notify(-1);
    }


    private Vector3 GetDistance(GameObject obj)
    {
        // 1.座標を取得する.
        Vector3 posA = gameObject.transform.position;
        Vector3 posB = obj.transform.position;

        // 2.距離の差を求める.
        Vector3 distance = posB - posA;

        return distance;
    }

    private bool FlyTheRayToTheTarget(GameObject target)
    {
        // 現在地.
        Vector3 currentPos;
        currentPos = this.gameObject.transform.position;
        // 距離.
        Vector3 distance;
        // 角度.
        float angle;





        // 1.二点間の距離を取得する.
        distance = GetDistance(target);

        // 2.二点間の角度を取得する.        
        angle = Vector3.Angle(distance, transform.forward);
        if(angle > m_viewingAngle)
            return false;

        // 3.reyを発射する.
        Ray ray = new Ray(currentPos, distance);
        Physics.SphereCast(ray, 100.0f, m_maxRayDistance);

        // デバック用の表示.
        Debug.DrawRay(ray.origin, ray.direction * m_maxRayDistance, Color.red);

        // 4.reyの衝突を確認する.
        RaycastHit raycastHit;
        bool hit = Physics.Raycast(ray, out raycastHit, m_maxRayDistance);
        // 衝突していない場合.
        if (!hit)
            return false;
        // 衝突したオブジェクトのタグが同じではない場合
        if (raycastHit.collider.gameObject.tag != target.tag)
            return false;

        return true;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "character")
            m_targets.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        m_targets.Remove(other.gameObject);
    }



    /*------------------------------------------------------*/
    // プレイヤー内のグローバル変数
    /*------------------------------------------------------*/
    [SerializeField]
    TerminalSCR m_terminalSCR;
    /*------------------------------------------------------*/
}