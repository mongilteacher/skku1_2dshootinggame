using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ��ǥ: ���� ��� �̵��ϰ� �ʹ�.

    // �ʿ� �Ӽ�
    // - �̵� �ӵ�
    public float Speed = 5;

    // ���
    // - ��� ���� �̵�
    private void Move()
    {
        // ���� ����
        // 1. ������ ���Ѵ�.
        Vector2 dir = Vector2.up;

        // 2. �̵��Ѵ�.
        transform.Translate(dir * Speed * Time.deltaTime);
    }


    void Update()
    {
        Move();
    }
}
