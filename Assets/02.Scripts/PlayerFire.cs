using UnityEngine;

public enum FireMode
{
    Auto,
    Mannual
}

public class PlayerFire : MonoBehaviour
{
    // ��ǥ: �Ѿ��� ���� �߻��ϰ� �ʹ�.

    // �ʿ� �Ӽ�:
    // - �Ѿ� ������
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    
    // - �ѱ���
    public GameObject[] Muzzles;
    public GameObject[] SubMuzzles;

    // - ��Ÿ�� / ��Ÿ�̸�
    public float Cooltime  = 0.6f;
    public float Cooltimer = 0f;

    // - ���(�ڵ�, ����)
    public FireMode FireMode = FireMode.Mannual;


    
    // �ʿ� ���:
    // - �߻��ϴ�.
    private void Update()
    {
        Cooltimer -= Time.deltaTime;

        // Ű �Է� �˻�
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            FireMode = FireMode.Auto;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            FireMode = FireMode.Mannual;
        }

        // ��Ÿ���� ���� �ȵ����� ����
        if(Cooltimer > 0 )
        {
            return;
        }

        // �ڵ� ��� �̰ų� "Fire1" ��ư�� �ԷµǸ�..
        if (FireMode == FireMode.Auto || Input.GetButtonDown("Fire1"))
        {
            foreach (GameObject muzzle in Muzzles)
            {
                GameObject bullet = Instantiate(BulletPrefab); // �ν��Ͻ�ȭ

                bullet.transform.position = muzzle.transform.position;
            }

            foreach (GameObject subMuzzle in SubMuzzles)
            {
                GameObject subBullet = Instantiate(SubBulletPrefab); // �ν��Ͻ�ȭ

                subBullet.transform.position = subMuzzle.transform.position;
            }

            Cooltimer = Cooltime;
        }
    }


}
