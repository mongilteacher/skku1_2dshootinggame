using UnityEngine;
 

public class Enemy : MonoBehaviour
{
    public float Speed = 5f;
    public int Health = 100;
    public int Damage = 40;

    // �������Ӹ��� �ڵ����� ȣ��Ǵ� �Լ�
    private void Update()
    {
        Vector2 dir = Vector2.down;
        transform.Translate(dir * Speed * Time.deltaTime);
    }


    // �浹 �̺�Ʈ �Լ�
    // - Trigger �̺�Ʈ    : ���� ������ ����������, �浹 �̺�Ʈ�� �ްڴ�.
    // - Collision �̺�Ʈ  : ���� ���굵 �ϰ�, �浹 �̺�Ʈ�� �ްڴ�.

    // �浹 ����, �浹 ��, �浹 ��

    // �ٸ� �ݶ��̴��� �浹�� �Ͼ���� �ڵ����� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D other)   // Stay(�浹��), Exit(�浹��)
    {
        // �Ѿ˰� �浹:
        if(other.CompareTag("Bullet"))
        {
            // ���װ�
            Destroy(other.gameObject);

            // ������
            Bullet bullet = other.GetComponent<Bullet>();
            Health -= bullet.Damage;

            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        // �÷��̾�� �浹:
        if(other.CompareTag("Player"))
        {
            // ������
            Destroy(this.gameObject);

            // �÷��̾� ü���� 0 �����϶��� ���δ�.
            Player player = other.GetComponent<Player>(); // ���� ������Ʈ�� ������Ʈ�� �����´�.
            
            player.Health -= Damage;
            if(player.Health <= 0)
            {
                Destroy(player.gameObject);
            }
        }

    }
}
