using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 5f;

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
        if(other.CompareTag("Bullet"))
        {
            // Destory: ���� ������Ʈ�� �ı�!

            // ���װ�
            Destroy(other.gameObject);

            // ������
            Destroy(this.gameObject);
        }
    }
}
