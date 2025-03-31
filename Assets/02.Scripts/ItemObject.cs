using System;
using UnityEngine;
using Random = UnityEngine.Random;


public enum ItemType
{
    HealthUp,
    AttackSpeedUp,
    MoveSpeedUp
}

public class ItemObject : MonoBehaviour
{
    public ItemType ItemType;

    public float MoveSpeed = 2f;
    
    public float Value;

    // 시간을 체크할 타이머
    private float _timer = 0f;

    private const float DELETE_TIME = 5f;
    private float _deleteTimer = 0f;

    // 둘 다 콜라이더가 무조건 있어야하고
    // 하나 이상이 리지드바디 있어야 한다.

    private GameObject _player;


    // 제일 상위 게임 오브젝트가 콜라이더와 리지드바디를 가지고 있는 상황
    // 1. 상위 게임 오브젝트에 여러 콜라이더가 있을 때 트리거 이벤트가 발생하면 어떤 콜라이더인지 판별 가능한가?
    // 2. 하위 게임 오브젝트들에 콜라이더가 있을시 상위 스크립트로 이벤트가 전달될 것인가?
    // 3. 하위 게임 오브젝트들에서 이벤트 가로채기 안된다.


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    private float _percent = 0f;

    private Vector2 _controlVector = Vector2.zero;
    
    private bool _isMoving = false;
    private float _duration = 0;
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position); // 3 
        if (distance < 3f)
        {
            _isMoving = true;
            _duration = distance / MoveSpeed; // 10/2 -> 5
        }

        if (_isMoving)
        {
            if (_controlVector == Vector2.zero)
            {
                _controlVector =  transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }

            _percent += Time.deltaTime / _duration;
            
            transform.position = Bezier(transform.position, _controlVector, _player.transform.position, _percent);
        }
        else
        {
            _deleteTimer += Time.deltaTime;
            if (_deleteTimer >= DELETE_TIME)
            {
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="start">시작점</param>
    /// <param name="center">제어점</param>
    /// <param name="end">목적점</param>
    /// <param name="t">시간(0~1(100%))</param>
    private Vector2 Bezier(Vector2 start, Vector2 center, Vector2 end, float t)
    {
        // p1: 새로운 위치1 = 시작점과 제어점 사이의 보간
        // p2: 새로운 위치2 = 제어점과 목적지 사이의 보간
        // 최종 위치 = p1과 p2 사이의 보간

        Vector2 p1 = Vector2.Lerp(start, center, t);
        Vector2 p2 = Vector2.Lerp(center, end, t);
        Vector2 final =  Vector2.Lerp(p1, p2, t);
        return final;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _timer += Time.deltaTime;

            if (_timer < 1f) return;

            // 효과 발동!
            Player player = other.GetComponent<Player>();

            switch (ItemType)
            {
                case ItemType.HealthUp:
                {
                    player.Health += (int)Value; // 프로퍼티, 메서드 처리
                    // 설계는 best가 없다. better가 있다.
                    break;
                }

                case ItemType.AttackSpeedUp:
                {
                    player.AttackCooltime += Value;
                    break;
                }

                case ItemType.MoveSpeedUp:
                {
                    //player.MoveSpeed += Value;
                    break;
                }
            }
            
            Destroy(gameObject);  
        }
    }
}
