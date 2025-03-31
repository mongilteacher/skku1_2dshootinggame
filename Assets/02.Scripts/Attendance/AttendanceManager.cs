using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    // 관리자(한명<싱글톤>): 추가, 삭제, 조회, 정렬

    public static AttendanceManager Instance;

    // 기획자가 세팅한 출석 데이터들
    [SerializeField]
    private List<AttendanceDataSO> _soDatas;

    // (출석 데이터로부터 만들어진) '출석' 객체들
    private List<Attendance> _attendances; // = null
    
    // 출석 검증 데이터
    private DateTime _lastLoginDateTime = new DateTime();   // 마지막으로 로그인한 날짜
    private int _attendanceCount = 0;                       // 현재까지 출석 횟수

    // 데이터 바뀔때마다 호출되는 콜백
    public Action OnDataChanged;
    
    // [조회]
    public List<Attendance> Attendances => _attendances;


    
    private void Awake()
    {
        Instance = this;
        
        // [추가]
        // 게임이 시작될 때 출석 객체들을 만들어야한다. (기획자가 세팅한 만큼)
        _attendances = new List<Attendance>(_soDatas.Count); // 용량(capacity)을 지정해서 미리 필요한 만큼 받아두자
        foreach (AttendanceDataSO data in _soDatas)
        {
            Attendance attendance = new Attendance(data, false);
            _attendances.Add(attendance);
        }
        
        OnDataChanged?.Invoke();

        AttendanceCheck();
    }

    private void AttendanceCheck()
    {
        DateTime today = DateTime.Today;
        if (today > _lastLoginDateTime) // 오늘이 마지막으로 로그인한 날짜보다 크다면(하루 이상 지났다면) 
        {
            // 데이터 갱신
            _lastLoginDateTime = today;
            _attendanceCount += 1;
        }
    }
    
    
    
    // [보상 받기]
    // 최태온강사님.보상주세요(출석)
    public bool TryGetReward(Attendance attendance)
    {
        // 조건 1. 이미 보상을 받았다면 실패
        if (attendance.IsRewarded)
        {
            return false;
        }
        
        // 조건 2. 실제 그만큼 출석을 했는가?
        if (_attendanceCount < attendance.Data.Day)
        {
            return false;
        }
        
        CurrencyManager.Instance.Add(attendance.Data.RewardCurrencyType, attendance.Data.RewardAmount);
        attendance.SetRewarded(true);
        
        OnDataChanged?.Invoke();

        return true;
    }
    
    
    
}
