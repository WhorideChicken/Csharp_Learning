using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    //PC -> 방향키
    private Vector3 inputVec; // 입력방향
    private float movePower = 50; // 이동속도
    private float jumpPower = 5; // 점프 힘 
    private bool isGround; //바닥 체크 

    Rigidbody rigid;

    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    //3D좌표계기 때문에 이동은 x,z축으로
    void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.z = Input.GetAxis("Vertical");

        bool isJump = Input.GetButtonDown("Jump");

        if(isJump)
        {
            // ForceMode.Impulse : 순간적인 힘을 주는것
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    
    
    }

    //물리 계산은 FixedUpdate에서 일어나도록
    private void FixedUpdate()
    {
        //ForceMode.Force 지속적인 힘을 가함, 기본값이기 때문에 생략 가능
        rigid.AddForce(inputVec * movePower, ForceMode.Force);
    }
}
