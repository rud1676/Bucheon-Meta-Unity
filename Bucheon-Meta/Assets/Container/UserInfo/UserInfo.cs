using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Container
{
    public class UserInfo : MonoBehaviour
    {
        public static UserInfo Instance;

        public string DeviceUniqueIdentifier;

        // 회원가입 임시 데이터
        public string userId = "";
        public string emailAdress = "";
        public string nickName = "";
        public string tempNickName = "";
        public string snsId = "";
        public string provider = "";
        public int avataType = 0;
        public string deviceToken;

        // server real data;
        public UserInfoResult userInfoResult;
        // 유저가 획득한 마일리지
        public List<UserHistoryPoint> userHistoryPoint;

        public MyRanking myrank;
        // 랭킹 정보 리스트
        public List<RankingPerson> userRankPerson;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DeviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
            }
        }

        public async void AddCleanPoint(int trashSeq, string location, bool tAttack)
        {
            int totalPoint = await ApiServer.AddPoint(trashSeq, location, tAttack ? "Y" : "N");
            if (totalPoint != -1)
            {
                userInfoResult.cleanPoint = totalPoint;
                UIManager.GetView<INGAMEView>().ChangeTrashPointView(trashSeq, totalPoint); //UI questView 업데이트
            }
        }

        public async Task AsyncUpserInfo()
        {
            UserInfo.Instance.userInfoResult = await ApiServer.GetUserInfo(UserInfo.Instance.userId);
        }

        public async void UpdateUserInfo()
        {
            LoadingManager.Instance.Show();
            await ApiServer.UpdateUserInfo();
            LoadingManager.Instance.Hide();
        }

        public bool IsLinkClenCity()
        {
            if (string.IsNullOrEmpty(UserInfo.Instance.userInfoResult.cleantownUid))
            {
                return false;
            }
            return true;
        }

        public async Task<List<UserHistoryPoint>> GetTrashPointHistory(string month)
        {
            LoadingManager.Instance.Show();
            var historyList = await ApiServer.GetPointHistory(month);
            userHistoryPoint = MyUtils.ToList(historyList);
            LoadingManager.Instance.Hide();
            return userHistoryPoint;
        }

        public async Task<List<CampaignRe>> GetCampaignHistorys(string month)
        {
            LoadingManager.Instance.Show();
            var historyList = await ApiServer.GetCampaignHistory(month);
            LoadingManager.Instance.Hide();
            return historyList;
        }
        public async Task<List<RankingPerson>> GetRankingDatas(string date)
        {
            LoadingManager.Instance.Show();
            var historyList = await ApiServer.GetRankings(date);
            userRankPerson = MyUtils.ToList(historyList);
            LoadingManager.Instance.Hide();
            return userRankPerson;
        }
        public async Task<MyRanking> GetMyRank(string date)
        {
            LoadingManager.Instance.Show();
            var myRanking = await ApiServer.GetMyRank(date);
            LoadingManager.Instance.Hide();
            return myRanking;
        }
    }
}