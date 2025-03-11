using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // ��ǥ: �Ѿ��� ���� �߻��ϰ� �ʹ�.

    // �ʿ� �Ӽ�:
    // - �Ѿ� ������
    public GameObject BulletPrefab;
    // - �ѱ�
    public GameObject Muzzle;

    
    // �ʿ� ���:
    // - �߻��ϴ�.
    private void Update()
    {
        // 1. �߻� ��ư�� ������
        if(Input.GetButtonDown("Fire1"))
        {
            // 2. ���������κ��� �Ѿ��� �����.
            GameObject bullet = Instantiate(BulletPrefab); // �ν��Ͻ�ȭ
            // Instantiate�� ���ӿ�����Ʈ�� '����'�ؼ� ���� ���� ����� �ִ´�.

            // 3. �Ѿ� ��ġ�� �ѱ��� ��ġ�� �������ش�.
            bullet.transform.position = Muzzle.transform.position;
        }
    }


}
