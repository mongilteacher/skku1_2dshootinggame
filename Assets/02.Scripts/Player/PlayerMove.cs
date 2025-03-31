using System;
using UnityEngine;

public class PlayerMove : PlayerComponent
{
    // MonoBehaviour: 여러 가지 이벤트 함수를 자동으로 호출해주는 기능
    // Component: 게임 오브젝트에 추가할 수 있는 여러 가지 기능

    // 최종 목표: 키보드 입력에 따라 플레이어를 이동시키고 싶다.

    public float MinX, MaxX;
    public float MinY, MaxY;

    private GameObject _target = null;
    private int UNDER_LINE;

    public Animator MyAnimator;

    public DynamicJoystick Joystick;

    public PlayerMove()
    {
        UNDER_LINE = 4;
    }

    private const float THRESHOLD = 0.01f;
    private const float DETACTION_RANGE = 6;

    // Start 보다 먼저 호출되며 프리팹이 인스턴스화 된 직후 호출
    protected override void Awake()
    {
        base.Awake();
        
        MyAnimator = GetComponent<Animator>();
        
#if !UNITY_ANDROID 
        Destroy(Joystick.gamObject);
#endif
    }
    
    private void Update()
    {
        SpeedCheck();

        if (_player.PlayMode == PlayMode.Auto)
        {
            AutoMove();
        }
        else
        {
            ManualMove();
        }
    }

    private void AutoMove()
    {
        // 가장 가까운 적을 찾아서 _target에 저장
        FindClosestTarget();

        // 적이 없다면 아무고또 안한다.
        if (_target == null) return;
        
        // _target을 이용해서 이동 코드 작성
        Vector2 direction = _target.transform.position - transform.position;
        float distance = direction.magnitude;
        
        // 달달달거리지 않게
        if (Mathf.Abs(direction.x) <= THRESHOLD)
        {
            direction.x = 0;
        }
        
        PlayAnimation(direction);

        // 멀면 앞으로 가까우면 뒤로
        if (distance > DETACTION_RANGE)
        {
            direction.y = 1;
        }
        else if (transform.position.y < -UNDER_LINE)
        {
            direction.y = 0;
        }
        else
        {
            direction.y = -1;
        }

        
        
        
        direction.Normalize();
        
        
        // 1. 새로운 위치 = 현재 위치 + 방향 * 속력 * 시간
        Vector3 newPosition = transform.position + (Vector3)(direction * _player.MoveSpeed) * Time.deltaTime;

        // 2. Math.Clamp(현재값, 최소값, 최대값)
        newPosition.y = Math.Clamp(newPosition.y, MinY, MaxY);

        // 3. 넘어가면 반대로
        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        // 4. 위치 갱신
        transform.position = newPosition;
    }

    // 가장 가까운 적을 찾는다.
    private void FindClosestTarget()
    {
        // 이미 타겟이 있으면 아무것도 안한다.
        if (_target != null) return;
        
        // 모든 적을 찾는다.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        // 가장 나와 거리가 짧은 적 찾기
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            // 거리가 저장한것보다 짧으면
            float targetDistance = Vector2.Distance(transform.position, enemy.transform.position);
            if (targetDistance < distance)
            {
                // 타겟을 갱신한다.
                distance = targetDistance;
                _target = enemy;
            }
        
        }
    }
    
    
    private void ManualMove()
    {
        float h = Input.GetAxisRaw("Horizontal"); // 수평 키 : -1, 0, 1
        float v = Input.GetAxisRaw("Vertical");  // 수직 키: -1, 0, 1

#if UNITY_ANDROID 
        h = Joystick.Horizontal;
        v = Joystick.Vertical;
#endif
        
        Vector2 direction = new Vector2(h, v);
        direction = direction.normalized;
        direction.Normalize();

        PlayAnimation(direction);


        // 1. 새로운 위치 = 현재 위치 + 방향 * 속력 * 시간
        Vector3 newPosition = transform.position + (Vector3)(direction * _player.MoveSpeed) * Time.deltaTime;

        // 2. Math.Clamp(현재값, 최소값, 최대값)
        newPosition.y = Math.Clamp(newPosition.y, MinY, MaxY);

        // 3. 넘어가면 반대로
        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        // 4. 위치 갱신
        transform.position = newPosition;
    }

    private void SpeedCheck()
    {
        // 5. 스피드 수정
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 매직넘버로 해도 되는 숫자: -1, 0, 1
            //_player.MoveSpeed += Math.Min(10, _player.MoveSpeed + 1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //_player.MoveSpeed = Math.Max(1, _player.MoveSpeed - 1);
        }
    }


    private void PlayAnimation(Vector2 direction)
    {
        // 1. 직접 애니메이터에게 특정 애니메이션 클립(상태)을 실행하라고 명령
        /*if (direction.x == 0)
        {
            MyAnimator.Play("Player_Idle");
        }
        else if (direction.x < 0)
        {
            MyAnimator.Play("Player_Left");
        }
        else if (direction.x > 0)
        {
            MyAnimator.Play("Player_Right");
        }*/
        
        // 2. 상태 전이
        // -0.03 -> (int)-0.03 -> -1
        MyAnimator.SetInteger("x", direction.x < 0 ? -1 : direction.x > 0 ? 1 : 0);
    }
}
