using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSCR : BehaviorTreeNodeSCR
{
    // データの中継地点.
    [SerializeField]
    TerminalSCR m_terminalSCR;
 
    // 自分自身.
    GameObject m_myself;












    /*---------------------------------------------------------------------------------*/
    // 移動速度.
    /*---------------------------------------------------------------------------------*/
    // 移動速度.
    private float m_moveSpeed = 5.0f;

    /**
    * @brief 移動速度を設定する
    * @param float(移動速度)
    * @author shinji
    * @date 12/18
    */
    public void SetMoveSpeed(float speed)
    {
        m_moveSpeed = speed;
    }
    /*---------------------------------------------------------------------------------*/
    // PointSCR.
    /*---------------------------------------------------------------------------------*/
    // 現在地のPointSCR.
    private PointSCR m_currentPointSCR;
    // 目的地のPointSCR.
    private PointSCR m_destinationPointSCR;

    /**
    * @brief 現在地のPointSCRを設定する
    * @param PointSCR(現在地)
    * @author shinji
    * @date 12/18
    */
    private void SetCurrentPointSCR(PointSCR scr)
    {
        // 1. 現在地のPointSCRを変更する.
        m_currentPointSCR = scr;
    }
    /*---------------------------------------------------------------------------------*/
    // 目的地.
    /*---------------------------------------------------------------------------------*/
    // 目的地の座標.
    Vector3 m_destination;

    /**
    * @brief 目的地の座標を設定する
    * @param Vector3(目的地)
    * @author shinji
    * @date 12/18
    */
    private void SetDestination(Vector3 pos)
    {
        m_destination = pos;
    }
    /*---------------------------------------------------------------------------------*/
    // PointSCRの格納庫.
    /*---------------------------------------------------------------------------------*/
    public WayPointStorageSCR m_wayPointStorageSCR;
    /*---------------------------------------------------------------------------------*/
    // 各場所に関する番号＋α.
    /*---------------------------------------------------------------------------------*/
    // 現在地を表す番号.
    PointSCR.position m_currentPointNumber;
    // 目的地を表す番号.
    PointSCR.position m_destinationPointNumber;
    // 中間地点を表す番号.
    PointSCR.position m_midelePointNumber;

    // 中間地点に到着したか否か.
    bool m_arrivedAtMiddlePoint;



    [SerializeField]
    protected Animator m_animator;






    /*---------------------------------------------------------------------------------*/
    // 初期設定.
    /*---------------------------------------------------------------------------------*/

    public override void Setup()
    {
        // 0.変数を初期化する.
        m_arrivedAtMiddlePoint = false;

        m_myself = m_terminalSCR.GetMyself();

        // 1. 現在地を取得する＆設定する.
        m_currentPointSCR = GetNearestPointSCR();

        // 2.現在地を表す番号を取得する.
        int childNumber;
        childNumber = GetNearestPointSCRChildNumber(m_currentPointSCR);
        m_currentPointNumber = (PointSCR.position)childNumber;
        if (m_currentPointNumber >= PointSCR.position.MiddleCentor) m_currentPointNumber += 1;

        // 3.目的地を取得する＆設定する.
        m_destinationPointSCR = m_currentPointSCR.GetDestinationPointSCR();

        // 4.目的地を表す番号を取得する.
        int childCount;
        childCount = m_destinationPointSCR.gameObject.transform.childCount;
        m_destinationPointNumber = (PointSCR.position)Random.Range(0, childCount);
 
        // 5.中間地点を表す番号を取得する.
        m_midelePointNumber = GetMidelePointNumber(m_currentPointSCR, m_destinationPointSCR);

        // 6.目的地の座標を設定する.
        PointSCR.position pos = m_currentPointNumber;
        if (pos > PointSCR.position.MiddleCentor) pos -= 1;
        Transform transform = m_currentPointSCR.gameObject.transform;
        Vector3 position = transform.GetChild((int)pos).position;
        m_destination = position;


        /*----------------------------------*/
        // 移動開始.
        m_animator.SetBool("IsWalking", true);
        /*----------------------------------*/


    }

    /**
    * @brief 一番近いPointSCRを取得する
    * @return PointSCR(一番近いPointSCR)
    * @author shinji
    * @date 12/18
    */
    private PointSCR GetNearestPointSCR()
    {
        // 0.一時的な変数を作成する.
        Vector3 posA, posB;
        float disA, disB;
        int num = 0;

        // 1.リストを取得する.
        List<PointSCR> pointSCRList;
        pointSCRList = m_wayPointStorageSCR.GetPointSCRList();

        // 2.現在地を取得する.
        Vector3 currentPos = m_myself.transform.position;

        // 3.仮で最短を格納する.
        posA = currentPos - pointSCRList[0].gameObject.transform.position;
        disA = GetDistance(posA.x, posA.z);

        // 4.最短を取得する.
        for (int i = 0; i < pointSCRList.Count; i++)
        {
            posB = currentPos - pointSCRList[i].gameObject.transform.position;
            disB = GetDistance(posB.x, posB.z);

            if (disA > disB)
            {
                posA = posB;
                disA = disB;
                num = i;
            }
        }
        return pointSCRList[num];
    }
    /**
    * @brief 一番近いPointSCRの番号を取得する
    * @return PointSCR(一番近いPointSCR)
    * @author shinji
    * @date 12/18
    */
    private int GetNearestPointSCRChildNumber(PointSCR scr)
    {
        // 0.一時的な変数を作成する.
        Vector3 posA, posB;
        float disA, disB;
        int num = 0;
        Transform transform;
        transform = scr.gameObject.transform;

        // 1.現在地を取得する.
        Vector3 currentPos = m_myself.transform.position;


        // 2.仮で最短を格納する.
        posA = currentPos - transform.GetChild(0).gameObject.transform.position;
        disA = GetDistance(posA.x, posA.z);

        // 3.最短を取得する.
        for (int i = 0; i < transform.childCount; i++)
        {
            posB = currentPos - transform.GetChild(i).gameObject.transform.position;
            disB = GetDistance(posB.x, posB.z);

            if (disA > disB)
            {
                posA = posB;
                disA = disB;
                num = i;
            }
        }
        return num;
    }
    /**
    * @brief 中間地点を取得する
    * @param float(x), float(z)
    * @return float(距離)
    * @author shinji
    * @date 12/18
    */
    private float GetDistance(float x, float z)
    {
        return Mathf.Sqrt(x * x + z * z);
    }

    /**
    * @brief 中間地点を取得する
    * @return PointSCR.position(中間地点)
    * @author shinji
    * @date 12/18
    */
    private PointSCR.position GetMidelePointNumber(PointSCR startPoint, PointSCR endPoint)
    {
        Vector3 startPos;
        startPos = startPoint.gameObject.transform.GetChild((int)m_currentPointNumber).position;
        Vector3 endPos;
        endPos = endPoint.gameObject.transform.GetChild((int)m_destinationPointNumber).position;

        return GetPositionRelationship(startPos, endPos);
    }
    /**
    * @brief 位置関係を取得する
    * @return Vector3(現在地),Vector3(目的地), 
    * @author shinji
    * @date 12/18
    */
    private PointSCR.position GetPositionRelationship(Vector3 currentPos, Vector3 targetPos)
    {
        PointSCR.position pos;

        // 位置関係を計算する.
        Vector3 distance;
        distance = targetPos - currentPos;

        // 目的地が右側にある場合.
        if (distance.x > 0)
            if (distance.z > 0)
                pos = PointSCR.position.TopRight;
            else
                pos = PointSCR.position.BottomRight;
        // 目的地が左側にある場合.
        else
            if (distance.z > 0)
            pos = PointSCR.position.TopLeft;
        else
            pos = PointSCR.position.BottomLeft;

        return pos;
    }








    /*---------------------------------------------------------------------------------*/
    // 更新.
    /*---------------------------------------------------------------------------------*/
    public override void DoAction()
    {
        // 1.到着を確認する.
        bool arrived = CheckArrival(m_destination);

        // 2.もし、到着した場合.
        if (arrived)
        {
            bool hoge;
            hoge = SetNextPointNumber();
            if (hoge == true)
                Notify();

            return;
        }

        // 3.目的地に向かって移動する.
        GoTowardToDestination();
    }


    // 許容範囲.
    float m_tolerance = 1.0f;
    // 
    /**
    * @brief 到着を確認する
    * @return Vector3(目的地)
    * @return true:到着した
    * @author shinji
    * @date 12/18
    */
    private bool CheckArrival(Vector3 destinationPosition)
    {
        // 1.現在地を取得する.
        Vector3 currentPosition;
        currentPosition = m_myself.transform.position;

        // 2.1.二点間(現在地と目的地)の距離を求める(Vector3).
        Vector3 distance_Vector3;
        distance_Vector3 = destinationPosition - currentPosition;
        // 2.2.二点間(現在地と目的地)の距離を求める(float).
        float distance_float;
        distance_float = Mathf.Sqrt((distance_Vector3.x * distance_Vector3.x) + (distance_Vector3.z * distance_Vector3.z));

        // 3.二点間の距離が一定以内の場合.
        if (distance_float < m_tolerance)
        {
            // 到着している.
            return true;
        }
        // 到着していない.
        return false;
    }

    /**
    * @brief 目的地へ向かって進む
    * @author shinji
    * @date 12/18
    */
    private void GoTowardToDestination()
    {
        // 1.目的地を見る.
        m_myself.transform.LookAt(m_destination);

        // 2.現在地を取得する.
        Vector3 currentPosition;
        currentPosition = m_myself.transform.position;

        // 3.移動距離を取得する.
        Vector3 moveDistance;
        moveDistance = m_myself.transform.forward * m_moveSpeed * Time.deltaTime;

        // 4.移動する.
        m_myself.transform.position = currentPosition + moveDistance;
    }
    /**
    * @brief 次の位置を表す番号を設定する
    * @author shinji
    * @date 12/18
    */
    private bool SetNextPointNumber()
    {
        PointSCR.position pos;

        // 1.水平移動できるか確認する.
        pos = CheckIfYouCanHorizontalMove(m_currentPointNumber, m_midelePointNumber);
        if (pos != m_currentPointNumber)
        {
            // 1.1.現在地の番号を更新する.
            m_currentPointNumber = pos;

            // 1.2.目的地の番号を更新する.
            if (pos >= PointSCR.position.MiddleCentor) pos -= 1;
            m_destinationPointNumber = pos;

            // 1.3.目的地を更新する.
            m_destination = m_currentPointSCR.gameObject.transform.GetChild((int)pos).position;       
            return false;
        }

        // 2.垂直移動ができるか確認する.
        pos = CheckIfYouCanVerticalMove(m_currentPointNumber, m_midelePointNumber);
        if (pos != m_currentPointNumber)
        {
            // 2.1.現在地の番号を更新する.
            m_currentPointNumber = pos;

            // 2.2.目的地の番号を更新する.
            if (pos >= PointSCR.position.MiddleCentor) pos -= 1;
            m_destinationPointNumber = pos;

            // 2.3.目的地を更新する.
            m_destination = m_currentPointSCR.gameObject.transform.GetChild((int)pos).position;
            return false;
        }

        // 3.移動しなかった場合.
        if (!m_arrivedAtMiddlePoint)
        {
            // 3.1.中間地点を反転する.
            InvertTheMidelePointNumber();

            // 3.2.スクリプトを切り替える.
            m_currentPointSCR = m_destinationPointSCR;

            // 3.3.現在地を変更する.
            m_currentPointNumber = m_midelePointNumber;

            // 3.4.目的地を変更する.
            m_midelePointNumber = m_destinationPointNumber;

            // 3.5.現在地の番号を更新する.
            pos = m_currentPointNumber;
            if (pos >= PointSCR.position.MiddleCentor) pos -= 1;

            // 3.6.目的地を更新する.
            m_destination = m_currentPointSCR.gameObject.transform.GetChild((int)pos).position;          
        }
        else
        {
            // 1.目的地を切り替える.
            if (m_midelePointNumber != m_destinationPointNumber)
                m_midelePointNumber = m_destinationPointNumber;
            // 2.到着した.
            else
            {
                /*----------------------------------*/
                // 移動開始.
                m_animator.SetBool("IsWalking", false);
                /*----------------------------------*/
                return true;
            }
        }
        // ここに来るのは、ありえない！！
        return false;
    }


    // 水平移動できるか確認する.
    private PointSCR.position CheckIfYouCanHorizontalMove(PointSCR.position current, PointSCR.position target)
    {
        if (current == PointSCR.position.MiddleLeft || current == PointSCR.position.MiddleRight)
            return current;
        
        // 水平の位置を取得する.
        int currentHorizontal = (int)current % 3;
        int targetHorizontal = (int)target % 3;

        // 目的地の番号を保持する.
        PointSCR.position targrtNum;
        targrtNum = current;

        // 移動距離を取得する.
        int distance;
        distance = targetHorizontal - currentHorizontal;

        if (distance > 0 && currentHorizontal != 2)
            targrtNum += 1;
        if (distance < 0 && currentHorizontal != 0)
            targrtNum -= 1;

        // 移動先が[4]の場合→移動させない.
        if (targrtNum == PointSCR.position.MiddleCentor)
            return current;

        return targrtNum;

    }

    // 垂直移動ができるか確認する.
    private PointSCR.position CheckIfYouCanVerticalMove(PointSCR.position current, PointSCR.position target)
    {
        if (current == PointSCR.position.TopCentor || current == PointSCR.position.BottomCentor)
            return current;

        // 垂直の位置を取得する.
        int currentVertical = (int)current / 3;
        int targetVertical = (int)target / 3;

        // 目的地の番号を保持する.
        PointSCR.position targrtNum;
        targrtNum = current;

        // 移動距離を取得する.
        int distance;
        distance = targetVertical - currentVertical;

        if (distance > 0)
            targrtNum += 3;
        if (distance < 0)
            targrtNum -= 3;

        // 移動先が[4]の場合→移動させない.
        if (targrtNum == PointSCR.position.MiddleCentor)
            return current;

        return targrtNum;
    }

    // 中間地点を反転する.
    private void InvertTheMidelePointNumber()
    {
        m_midelePointNumber -= 8;
        m_midelePointNumber = (PointSCR.position)Mathf.Abs((int)m_midelePointNumber);

        m_arrivedAtMiddlePoint = true;
    }
}