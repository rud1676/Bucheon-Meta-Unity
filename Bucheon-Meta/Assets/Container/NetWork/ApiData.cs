using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Container
{
    [System.Serializable]
    public struct UserResult
    {
        public int id;
        public string mail;
    }

    [System.Serializable]
    public class UserCheckResult
    {
        public string provider;
        public int cnt;
    }

    [System.Serializable]
    public class UserInfoResult
    {
        public string userId;
        public string nickname;
        public string email;
        public int avatarType;
        public string provider;
        public int cleanPoint;
        public string deviceToken;
        public string cleantownUid; // 안에 스트링이 있다면 연동 된걸로 생각
    }
    [System.Serializable]
    public class JoinResult
    {
        public string result;
    }

    [System.Serializable]
    public class KakaoAccount
    {
        public string email;
        public bool emailNeedsAgreement;
        public bool isEmailValid;
        public bool isEmailVerified;
        public KakaoProfile profile;
        public bool profileNicknameNeedsAgreement;
    }

    [System.Serializable]
    public class KakaoProfile
    {
        public string nickname;
    }

    [System.Serializable]
    public class KakaoProperties
    {
        public string nickname;
    }

    [System.Serializable]
    public class KakaoAccountInfo
    {
        public string connectedAt;
        public int id;
        public KakaoAccount kakaoAccount;
        public KakaoProperties properties;
    }

    [System.Serializable]
    public class NaverAccount
    {
        public string email;
        public string id;
        public string nickname;
    }

    [System.Serializable]
    public class TrashInfo
    {
        public int trashSeq;
        public string trashName;
        public string trashAct;
        public int trashPoint;
    }

    [System.Serializable]
    public class TrashInfoUrl
    {
        public string result;
        public string url;
    }

    [System.Serializable]
    public class ResultAddPoint
    {
        public string result;
        public int point;
    }
    [System.Serializable]
    /// <summary>
    /// 랭킹페이지에 담을 데이터
    /// </summary>
    public class RankingPerson
    {
        public int rankNum;
        public string lastConnectAt;
        public string userId;
        public string nickname;
        public int sumPoint;

    }

    /// <summary>
    /// 해당 유저의 랭킹 정보 가져오기
    /// </summary>
    [System.Serializable]
    public class MyRanking
    {
        public string nickname;
        public int rankNum;
        public string userId;
        public string lastConnectAt;
        public int sumPoint;

    }

    [System.Serializable]
    public class UserHistoryPoint
    {
        public int pointSeq;
        public int point;
        public int accumulatedPoint;
        public int trashSeq;
        public string trashName;
        public int cleanPoint;
        public string processLocation;
        public string reason;
        public string regDt; // 수거 시간
    }

    [System.Serializable]
    public class BoardInfo
    {
        public int boardSeq;
        public string boardType;
        public string boardTypeName;
        public string boardTitle;
        public string boardContent;
        public int vote1;
        public int vote2;
        public int vote3;
        public int boardAttCnt;
        public List<string> boardAttList;
    }

    [System.Serializable]
    public class CampaignReResult
    {
        public string result;
        public List<CampaignRe> CampaignRes;
    }

    [System.Serializable]
    public class CampaignRe
    {
        public string campaignStatusName; // 내역 - 쓰레기 접수
        public string areaName; // 활동 지역 - 상동
        public string regDt; // 일시
        public string mileage; // 마일리지
        public string campaignProcessName; // 상태 - 접수
    }
}

