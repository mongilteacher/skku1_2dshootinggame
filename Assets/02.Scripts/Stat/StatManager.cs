using System;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    // 관리자: 추가, 삭제, 조회, 정렬, 
    // 레벨업, 조회
    public static StatManager Instance;

    // 데이터
    public List<StatDataSO> StatDataList;
    

    private List<Stat> _stats = new List<Stat>();
    public List<Stat> Stats => _stats;

    // 델리게이트: 함수를 참조하는 변수
    public delegate void OnDataChanged();
// 
    public OnDataChanged OnDataChangedCallback = null;
    

    
    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < (int)StatType.Count; ++i)
        {
            _stats.Add(new Stat((StatType)i, 1, StatDataList[i]));
        }
    }
    

    public bool TryLevelUp(StatType statType)
    {
        if (_stats[(int)statType].TryUpgrade())
        {
            // 야! 데이터가 변화할때마다 너가 등록한 함수를 호출해줄게!
            OnDataChangedCallback?.Invoke();
            return true;
        }

        return false;
    }
}
