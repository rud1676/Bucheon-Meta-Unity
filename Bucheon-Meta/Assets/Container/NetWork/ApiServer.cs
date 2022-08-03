using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestHTTP;
using UnityEngine;

namespace Container
{
    /// <summary>
    /// 부천시 api
    /// </summary>
    public class ApiServer
    {

        public const string BASE_URL = "https://cleantown.bucheon.go.kr";

        // 사용자 아디이 체크
        private const string USER_CHECK = BASE_URL + "/admin/metaBus/rest/check/userId/{userId}";

        // provider , sns 체크
        private const string PROVIDER_CHECK = BASE_URL + "/admin/metaBus/rest/check/{provider}/{snsId}";

        // sns id 체크
        private const string SNSID_CHECK = BASE_URL + "/admin/metaBus/rest/check/snsId/{snsId}";

        // 닉네임 체크
        private const string NICKNAME_CHECK = BASE_URL + "/admin/metaBus/rest/check/nickname/{nickname}";

        // email 체크
        private const string EMAIL_CHECK = BASE_URL + "/admin/metaBus/rest/check/email/{email}";

        // 사용자 정보
        private const string USER_INFO = BASE_URL + "/admin/metaBus/rest/get/userId/{userId}";

        // 사용자 조회 provider snsId
        private const string USER_INFO_PROVIDER = BASE_URL + "/admin/metaBus/rest/get/{provider}/{snsId}";

        // 사용자 조회 snsID
        private const string USER_INFO_SNSID = BASE_URL + "/admin/metaBus/rest/get/snsId/{snsId}";

        // 사용자 조회 nickName
        private const string USER_INFO_NICKNAME = BASE_URL + "/admin/metaBus/rest/get/nickname/{nickname}";

        // 사용자 조회 email
        private const string USER_INFO_EMAIL = BASE_URL + "/admin/metaBus/rest/get/email/{email}";

        // 회원 가입
        private const string USER_JOIN = BASE_URL + "/admin/metaBus/rest/put/user/new";

        // 정보 수정
        private const string USER_UPDATE = BASE_URL + "/admin/metaBus/rest/put/userId/{useId}";

        // 쓰레기 종류 조회
        private const string TRASH_INFO = BASE_URL + "/admin/metaBus/rest/get/trashType";

        // 마일리지 등록
        private const string ADD_POINT = BASE_URL + "/admin/metaBus/rest/put/point/{userId}";

        // 투표값 가져오기 * 현재 BoardSeq 5,6,7,8 만 가능합니다
        private const string BOARD_SEQ = BASE_URL + "/admin/metaBus/board/rest/get/{boardSeq}";

        // 투표값 등록하기 * 현재 BoardSeq 5,6,7,8 만 가능합니다
        private const string ADD_BOARD_SEQ = BASE_URL + "/admin/metaBus/board/rest/put/{boardSeq}/{type}";

        // 불편접수 수거 팝업 이미지 URL
        public const string POPUP_IMAGE1_URL = BASE_URL + "/admin/metaBus/board/rest/get/popup/image1";

        // 줍기 팝업 이미지 URL
        public const string POPUP_IMAGE2_URL = BASE_URL + "/admin/metaBus/board/rest/get/popup/image2";

        // 수거 이미지 URL
        public const string POPUP_IMAGE3_URL = BASE_URL + "/admin/metaBus/board/rest/get/popup/image3";

        // 쓰레기 종류 조회 1
        private const string TRASH_TYPE = BASE_URL + "/admin/metaBus/rest/get/trashType";
        // 쓰레기 줍었던 히스토리 조회

        private const string GET_POINT_HISTORY = BASE_URL + "/admin/metaBus/rest/get/act/pointHistory/{userId}/{month}";

        //랭킹 조회
        private const string GET_RANKING_USER = BASE_URL + "/admin/metaBus/rest/get/ranking/all/{month}";

        //깨끗한 마을 랭킹 조회
        private const string CLEANVILAGE_MONTH_RANKING = BASE_URL + "/admin/metaBus/rest/get/campaignRanking/all/{month}";
        // 메타버스 캐릭터 포인트 순위 조회
        private const string METABUS_MONTH_RANKING = BASE_URL + "/admin/metaBus/rest/get/myRanking/{userId}/{month}";
        // 깨끗한 마을 캐릭터 포인트 순위 조회
        private const string CLEANVILAGE_USER_MONTH_RANKING = BASE_URL + "/admin/metaBus/rest/get/myCampaignRanking/{userId}/{month}";

        //게시판 조회
        private const string BOARD_VOTE_INFO = BASE_URL + "/admin/metaBus/board/rest/get/{type}";
        //게시판 투표
        private const string BOARD_VOTE = BASE_URL + "/admin/metaBus/board/rest/put/vote/{boardSeq}";

        // 깨끗한 마을 연동
        private const string LINK_TOWN = BASE_URL + "/admin/metaBus/rest/put/link";

        // 깨끗한 마을 연동 해제
        private const string UNLINK_TOWN = BASE_URL + "/admin/metaBus/rest/put/unlink";

        // 깨끗한 마을 내 활동 내역 리스트
        private const string CampaignHistory = BASE_URL + "/admin/metaBus/rest/get/act/campaignHistory/{userId}/{month}";   //URL error
        private const string CampaignHistoryTest = BASE_URL + "/admin/metaBus/rest/get/act/campaignHistory2";   //this is test URL

        // 깨끗한 마을 전체랭킹 순위 100 위까지
        private const string CampaignRanking = BASE_URL + "/admin/metaBus/rest/get/campaignRanking/all/{month}";

        private static async Task<string> requestCommon(HTTPMethods method, string api, string body = null, bool isArray = false)
        {
            // LoadingManager.Instance.ShowLoading();
            string fullUrl = api;

            Debug.Log("requestCommon fullUrl : " + fullUrl);

            HTTPRequest request = new HTTPRequest(new System.Uri(fullUrl), method);
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            // body 가 있을 경우
            if (!string.IsNullOrEmpty(body))
            {
                Debug.Log(body);
                request.RawData = System.Text.Encoding.UTF8.GetBytes(body.ToString());
            }

            try
            {
                await request.GetAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                return null;
            }

            if (request.Response.IsSuccess)
            {
                if (isArray)
                {
                    return JsonHelper.fixJson(request.Response.DataAsText);
                }
                else
                {
                    return request.Response.DataAsText;
                }
            }
            else
            {
                // 에러 처리 및 에러 팝업
                Debug.Log(request.Response.DataAsText);
                return null;
            }
        }

        public static async Task<bool> UserCheck(string userId)
        {
            var api = USER_CHECK.Replace("{userId}", userId);

            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData UserCheck : " + resultData);
            if (resultData != null)
            {
                UserCheckResult userCheckResult = JsonUtility.FromJson<UserCheckResult>(resultData);
                return userCheckResult.cnt > 0;
            }
            return false;
        }

        public static async Task<bool> IsExistNickName(string nickname)
        {
            var api = NICKNAME_CHECK.Replace("{nickname}", nickname);

            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData IsExistNickName : " + resultData);
            if (resultData != null)
            {
                UserCheckResult userCheckResult = JsonUtility.FromJson<UserCheckResult>(resultData);
                return userCheckResult.cnt > 0;
            }
            return false;
        }

        public static async Task<UserCheckResult> IsEmailExist(string email)
        {
            var api = EMAIL_CHECK.Replace("{email}", email);

            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData IsEmailExist : " + resultData);
            if (resultData != null)
            {
                UserCheckResult userCheckResult = JsonUtility.FromJson<UserCheckResult>(resultData);
                return userCheckResult;
            }
            return null;
        }

        public static async Task<UserInfoResult> GetUserInfo(string userId)
        {
            var api = USER_INFO.Replace("{userId}", userId);

            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData GetUserInfo : " + resultData);
            if (resultData != null)
            {
                UserInfoResult loginResult = JsonUtility.FromJson<UserInfoResult>(resultData);

                return loginResult;
            }
            return null;
        }
        public static async Task<UserInfoResult> GetUserInfoByEmail(string email)
        {

            var api = USER_INFO_EMAIL.Replace("{email}", email);

            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData GetUserInfoByEmail : " + resultData);
            if (resultData != null)
            {
                UserInfoResult loginResult = JsonUtility.FromJson<UserInfoResult>(resultData);

                return loginResult;
            }
            return null;
        }
        public static async Task<UserInfoResult> GetUserInfoByNickName(string nickname)
        {
            var api = USER_INFO_NICKNAME.Replace("{nickname}", nickname);

            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData GetUserInfoByNickName : " + resultData);
            if (resultData != null)
            {
                UserInfoResult loginResult = JsonUtility.FromJson<UserInfoResult>(resultData);

                return loginResult;
            }
            return null;
        }

        public static async Task<bool> JoinUser()
        {
            JSONObject jSONObject = new JSONObject();
            jSONObject.AddField("userId", UserInfo.Instance.userId);
            jSONObject.AddField("nickname", UserInfo.Instance.nickName);
            jSONObject.AddField("email", UserInfo.Instance.emailAdress);
            jSONObject.AddField("provider", UserInfo.Instance.provider);
            jSONObject.AddField("snsId", UserInfo.Instance.snsId);
            jSONObject.AddField("avatarType", UserInfo.Instance.avataType);
            if (string.IsNullOrEmpty(UserInfo.Instance.deviceToken) == false)
            {
                jSONObject.AddField("deviceToken", UserInfo.Instance.deviceToken);
            }

            string resultData = await requestCommon(HTTPMethods.Post, USER_JOIN, jSONObject.ToString());
            Debug.Log("resultData : " + resultData);
            if (resultData != null)
            {
                JoinResult joinResult = JsonUtility.FromJson<JoinResult>(resultData);
                if (joinResult.result.Equals("success"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static async Task<TrashInfo[]> GetTrashInfo()
        {
            string resultData = await requestCommon(HTTPMethods.Get, TRASH_INFO);
            resultData = JsonHelper.fixJson(resultData);
            TrashInfo[] trashInfos = JsonHelper.FromJson<TrashInfo>(resultData);
            return trashInfos;
        }

        public static async Task<string> GetTrashInfoUrl(string api)
        {
            string resultData = await requestCommon(HTTPMethods.Get, api);
            TrashInfoUrl trashInfo = JsonUtility.FromJson<TrashInfoUrl>(resultData);
            return trashInfo.url;
        }

        // 유저 싱크 
        public static async Task<bool> UpdateUserInfo()
        {
            var api = USER_UPDATE.Replace("{userId}", UserInfo.Instance.userInfoResult.userId);
            JSONObject jSONObject = new JSONObject();
            jSONObject.AddField("userId", UserInfo.Instance.userInfoResult.userId);
            jSONObject.AddField("nickname", UserInfo.Instance.userInfoResult.nickname);
            jSONObject.AddField("avatarType", UserInfo.Instance.userInfoResult.avatarType);

            if (string.IsNullOrEmpty(UserInfo.Instance.deviceToken) == false)
            {
                jSONObject.AddField("deviceToken", UserInfo.Instance.deviceToken);
            }

            string resultData = await requestCommon(HTTPMethods.Post, api, jSONObject.ToString());
            Debug.Log("UpdateUserInfo resultData : " + resultData);
            if (resultData != null)
            {
                JoinResult joinResult = JsonUtility.FromJson<JoinResult>(resultData);
                if (joinResult.result.Equals("success"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        // 쓰레기 마일리지 획득
        public static async Task<int> AddPoint(int trashSeq, string location, string tAttack)
        {
            var api = ADD_POINT.Replace("{userId}", tAttack);
            JSONObject jSONObject = new JSONObject();
            jSONObject.AddField("userId", UserInfo.Instance.userInfoResult.userId);
            jSONObject.AddField("trashSeq", trashSeq);
            jSONObject.AddField("processLocation", location);

            string resultData = await requestCommon(HTTPMethods.Post, api, jSONObject.ToString());

            if (resultData != null)
            {
                ResultAddPoint resultAddPoint = JsonUtility.FromJson<ResultAddPoint>(resultData);
                return resultAddPoint.point; // 유저의 총합 포인트
            }
            return -1; // 에러
        }

        // 획득한 마일리지 히스토리
        public static async Task<UserHistoryPoint[]> GetPointHistory(string month)
        {
            var api = GET_POINT_HISTORY.Replace("{userId}", UserInfo.Instance.userInfoResult.userId);
            api = api.Replace("{month}", month);
            Debug.Log("In APISERVER: " + api);
            
            string resultData = await requestCommon(HTTPMethods.Get, api);
            resultData = JsonHelper.fixJson(resultData);
            UserHistoryPoint[] userHistoryPoints = JsonHelper.FromJson<UserHistoryPoint>(resultData);
            return userHistoryPoints;
        }

        public static async Task<List<CampaignRe>> GetCampaignHistory(string month)
        {

            var api = CampaignHistory.Replace("{userId}", UserInfo.Instance.userInfoResult.userId);  //실제 사용 주소
            api = api.Replace("{month}", month);
            string resultData = await requestCommon(HTTPMethods.Get, api); //테스트 주소를 일단 넣었음 실제 사용시 수정 필

            Debug.Log(resultData);
            if (resultData != null)
            {
                var result = JsonUtility.FromJson<CampaignReResult>(resultData);
                return result.CampaignRes;
            }
            return null;
        }

        // todo
        //랭킹 페이지 목록
        public static async Task<RankingPerson[]> GetRankings(string date)
        {
            var api = GET_RANKING_USER.Replace("{month}", date);
            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData GetRankings : " + resultData);
            resultData = JsonHelper.fixJson(resultData);
            RankingPerson[] userRankData = JsonHelper.FromJson<RankingPerson>(resultData);
            return userRankData;
        }

        //랭킹 페이지 목록
        public static async Task<RankingPerson[]> GetCleanVilageRankings(string date)
        {
            var api = CLEANVILAGE_MONTH_RANKING.Replace("{month}", date);
            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData GetCleanVilageRankings : " + resultData);
            resultData = JsonHelper.fixJson(resultData);
            RankingPerson[] userRankData = JsonHelper.FromJson<RankingPerson>(resultData);
            return userRankData;
        }



        public static async Task<MyRanking> GetMyRank(string date)
        {
            var api = METABUS_MONTH_RANKING.Replace("{userId}", UserInfo.Instance.userInfoResult.userId);
            api = api.Replace("{month}", date);
            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData GetUserRank : " + resultData);
            if (resultData != null)
            {
                MyRanking RankData = JsonUtility.FromJson<MyRanking>(resultData);

                return RankData;
            }
            return null;
        }


        public static async Task<MyRanking> GetMyRankCleanVilage(string date)
        {
            var api = CLEANVILAGE_USER_MONTH_RANKING.Replace("{userId}", UserInfo.Instance.userInfoResult.userId);
            api = api.Replace("{month}", date);
            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData GetUserRank : " + resultData);
            if (resultData != null)
            {
                MyRanking RankData = JsonUtility.FromJson<MyRanking>(resultData);

                return RankData;
            }
            return null;
        }


        //게시판 정보 받아오기

        public static async Task<BoardInfo> GetBoardVote(string type)
        {

            var api = BOARD_VOTE_INFO.Replace("{type}", type); ;

            string resultData = await requestCommon(HTTPMethods.Get, api);
            Debug.Log("resultData GetBoardVote : " + resultData);
            if (resultData != null)
            {
                BoardInfo boardResult = JsonUtility.FromJson<BoardInfo>(resultData);

                return boardResult;
            }
            return null;
        }

        /// <summary>
        /// 게시ㅏㅍㄴ 투표
        /// </summary>
        /// <param name="boardseq">게시판 고유번호</param>
        /// <param name="vote">A: 좋음, B: 중간 C: 나쁨</param>
        /// <returns></returns>
        public static async Task<bool> VoteBoard(int boardseq, string vote, string boardType)
        {
            var api = BOARD_VOTE.Replace("{boardSeq}", boardseq.ToString());

            JSONObject jSONObject = new JSONObject();
            jSONObject.AddField("userId", UserInfo.Instance.userInfoResult.userId);
            jSONObject.AddField("voteType", vote);
            jSONObject.AddField("boardType", boardType);
            jSONObject.AddField("boardSeq", boardseq);

            string resultData = await requestCommon(HTTPMethods.Post, api, jSONObject.ToString());
            Debug.Log("VoteBoard resultData : " + resultData);
            if (resultData != null)
            {
                JoinResult joinResult = JsonUtility.FromJson<JoinResult>(resultData);
                if (joinResult.result.Equals("success"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 깨끗한 마을 연동
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> TownLink(string cleanUID="")
        {
            JSONObject jSONObject = new JSONObject();
            jSONObject.AddField("userId", UserInfo.Instance.userInfoResult.userId);

            if(cleanUID == "")
            {
                return false;
            }
            else
            {
                jSONObject.AddField("cleantownUid", cleanUID);
            }

            string resultData = await requestCommon(HTTPMethods.Post, LINK_TOWN, jSONObject.ToString());
            Debug.Log("VoteBoard resultData : " + resultData);
            if (resultData != null)
            {
                JoinResult joinResult = JsonUtility.FromJson<JoinResult>(resultData);
                if (joinResult.result.Equals("success"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 깨끗한 마을 연동 해제
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> TownUnLink()
        {
            JSONObject jSONObject = new JSONObject();
            jSONObject.AddField("userId", UserInfo.Instance.userInfoResult.userId);

            string resultData = await requestCommon(HTTPMethods.Post, UNLINK_TOWN, jSONObject.ToString());
            Debug.Log("VoteBoard resultData : " + resultData);
            if (resultData != null)
            {
                JoinResult joinResult = JsonUtility.FromJson<JoinResult>(resultData);
                if (joinResult.result.Equals("success"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

    }
}

