using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // MonoBehaviour: ���� ���� �̺�Ʈ �Լ��� �ڵ����� ȣ�����ִ� ���
    // Component: ���� ������Ʈ�� �߰��� �� �ִ� ���� ���� ���

    // ���� ��ǥ: Ű���� �Է¿� ���� �÷��̾ �̵���Ű�� �ʹ�.

    public float Speed = 3f;

    public float MinX, MaxX;
    public float MinY, MaxY;


    void Update()
    {

        // ���� ������Ʈ�� transform�̶�� ������Ʈ�� ������ �����Ƿ�
        // transform�� ���� ������ �� �ֵ��� ��������.

        // transform.Translate -> �̵��ϴٶ�� ������ �Ű� ������ '�ӵ�'�� �޴´�.
        // �ӵ�: ���� * �ӷ�



        // ����   : ũ��� ���� 
        // �������� 100


        // �ʴ� 3M(unit)��ŭ ���� ��������!
        // transform.Translate(Vector2.up * 3f * Time.deltaTime);
        // Vector2 d = Vector2.up;
        // Time.deltaTime: ������ �� �ð� ������ �ǹ��Ѵ�.
        // 4�������� Time.deltaTime: 1/4 = 0.25��     
        // 3*0.25 + 3 * 0.25 + 3 * 0.25 + 3 * 0.25 = 3


        // 2�������� Time.deltaTime: 1/2 = 0.5��
        // 3*0.5 + 3*0.5 = 3

        // 30�������� Time.deltaTime: 1/30 = 0.03��
        // 60�������� Time.deltaTime: 1/60 = 0.016��


        SpeedCheck();

        Move();
    }


    private void Move()
    {
        // Ű����, ���콺, ��ġ, ���̽�ƽ �� �ܺο��� ������
        // �Է� �ҽ��� ������� 'Input' Ŭ������ ���� ������ �� �ִ�.
        //float h = Input.GetAxis("Horizontal"); // ���� Ű : -1f ~ 1f
        float h = Input.GetAxisRaw("Horizontal"); // ���� Ű : -1, 0, 1

        //float v = Input.GetAxis("Vertical");  // ���� Ű: -1f ~ 1f
        float v = Input.GetAxisRaw("Vertical");  // ���� Ű: -1, 0, 1

        // ���� �����
        Vector2 direction = new Vector2(h, v);
        // ���ͷκ��� ���⸸ �������� ����: ����ȭ
        direction = direction.normalized;
        direction.Normalize();


        // transform.Translate(direction * Speed * Time.deltaTime);

        // 1. ���ο� ��ġ = ���� ��ġ + ���� * �ӷ� * �ð�
        Vector3 newPosition = transform.position + (Vector3)(direction * Speed) * Time.deltaTime;

        // 2. Math.Clamp(���簪, �ּҰ�, �ִ밪)
        newPosition.y = Math.Clamp(newPosition.y, MinY, MaxY);

        // 3. �Ѿ�� �ݴ��
        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        // 4. ��ġ ����
        transform.position = newPosition;
    }

    private void SpeedCheck()
    {
        // 5. ���ǵ� ����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // �����ѹ��� �ص� �Ǵ� ����: -1, 0, 1
            Speed += Math.Min(10, Speed + 1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Speed = Math.Max(1, Speed - 1);
        }
    }
}
