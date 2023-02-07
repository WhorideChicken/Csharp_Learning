using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    //PC -> ����Ű
    private Vector3 inputVec; // �Է¹���
    private float movePower = 50; // �̵��ӵ�
    private float jumpPower = 5; // ���� �� 
    private bool isGround; //�ٴ� üũ 

    Rigidbody rigid;

    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    //3D��ǥ��� ������ �̵��� x,z������
    void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.z = Input.GetAxis("Vertical");

        bool isJump = Input.GetButtonDown("Jump");

        if(isJump)
        {
            // ForceMode.Impulse : �������� ���� �ִ°�
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    
    
    }

    //���� ����� FixedUpdate���� �Ͼ����
    private void FixedUpdate()
    {
        //ForceMode.Force �������� ���� ����, �⺻���̱� ������ ���� ����
        rigid.AddForce(inputVec * movePower, ForceMode.Force);
    }
}
