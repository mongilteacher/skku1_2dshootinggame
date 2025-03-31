using UnityEngine;

public class Attendance
{
    // 출석 데이터
    public readonly AttendanceDataSO Data;
    
    // 보상 받기 유무
    private bool _rewarded;
    public bool IsRewarded => _rewarded;

    // 생성자(데이터와 보상 유무를 받아온다.)
    public Attendance(AttendanceDataSO data, bool rewarded)
    {
        Data = data;
        _rewarded = rewarded;
    }

    
    public void SetRewarded(bool rewarded)
    {
        
        _rewarded = rewarded;
    }
}
