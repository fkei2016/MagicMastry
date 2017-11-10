using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMagus : MonoBehaviour
{

    private const int maximumSlotNum = 4;

    [SerializeField]
    public MagicScript[] magic;

    public CooldownGaugeManager ui;
    public BarGaugeControler ui2;
    public ClockControler ui3;


    private Animator anim;

    private float maximumCoolTime;
    private float coolTime;
    // Use this for initialization
    void Start()
    {

        Debug.Log("s");
        maximumCoolTime = 15.0f;
        coolTime = 0;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsDead", Input.GetKey("q"));

        if (Input.GetKeyDown("a") && coolTime <= 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Cast"))
        {
            anim.SetTrigger("Attack");
            coolTime = maximumCoolTime;
        }
        anim.SetBool("IsWalking", Input.GetKey("w"));

        if(anim.GetBool("IsWalking") && anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            this.transform.position = this.transform.position + new Vector3(0, 0, -0.017f);
        }


        ui.GaugeUp.setValue(maximumCoolTime, maximumCoolTime - coolTime);
        ui2.setValue(maximumCoolTime, coolTime);
        ui3.setValue(maximumCoolTime, coolTime);

        coolTime -= 0.1f;

        coolTime = Mathf.Clamp(coolTime, 0, maximumCoolTime);

    }
}
