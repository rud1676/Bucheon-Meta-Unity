using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Container
{
    public class TrashManager : MonoBehaviour
    {
        public static TrashManager Instance;

        [SerializeField]
        public List<TrashInfo> mTrashInfos;

        // 대형 폐기물 포인트
        public int BigTrashPoint = 3;
        // 전단지 포인트
        public int PrintTrashPoint = 2;
        // 그 외 쓰레기 포인트
        public int OtherTrashPoint = 1;

        // 불편 접수 Url
        public string BigTrashUrl;
        // 수거 팝업 Url
        public string PrintTrashUrl;
        // 줍기 팝업 url
        public string OtherTrashUrl;

        /// <summary>
        /// Trash Seq 1 대형폐기물
        /// Trash Seq 2 전단지
        /// Trash Seq 3 그 외 쓰레기
        /// </summary>

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        void Start()
        {
            getTrashInfo();
            getTrashUrlInfo();
        }

        private async void getTrashUrlInfo()
        {
            BigTrashUrl = await ApiServer.GetTrashInfoUrl(ApiServer.POPUP_IMAGE1_URL);
            PrintTrashUrl = await ApiServer.GetTrashInfoUrl(ApiServer.POPUP_IMAGE3_URL);
            OtherTrashUrl = await ApiServer.GetTrashInfoUrl(ApiServer.POPUP_IMAGE2_URL);
        }

        private async void getTrashInfo()
        {
            var trashInfos = await ApiServer.GetTrashInfo();
            mTrashInfos = MyUtils.ToList(trashInfos);

            var trash = mTrashInfos.Find((TrashInfo trashInfo) =>
            {
                return trashInfo.trashSeq == 1;
            });

            if (trash != null)
            {
                BigTrashPoint = trash.trashPoint;
            }

            trash = mTrashInfos.Find((TrashInfo trashInfo) =>
            {
                return trashInfo.trashSeq == 2;
            });

            if (trash != null)
            {
                PrintTrashPoint = trash.trashPoint;
            }

            trash = mTrashInfos.Find((TrashInfo trashInfo) =>
            {
                return trashInfo.trashSeq == 3;
            });

            if (trash != null)
            {
                OtherTrashPoint = trash.trashPoint;
            }
        }

    }
}