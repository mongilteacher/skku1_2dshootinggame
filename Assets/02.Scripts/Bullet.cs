using UnityEngine;

public enum BulletType
{
    Main,
    Sub
}

public class Bullet : MonoBehaviour
{
    // ��ǥ: ���� ��� �̵��ϰ� �ʹ�.

    // �ʿ� �Ӽ�
    // - �̵� �ӵ�
    public float Speed = 5;

    public BulletType BulletType;

    public int Damage;

    private void Start()
    {
        // �Ѿ� Ÿ�Կ� ���� ������� �ٸ����Ѵ�.
        switch(BulletType)
        {
            case BulletType.Main:
                Damage = 100;
                break;

            case BulletType.Sub:
                Damage = 60;
                break;
        }
    }


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
