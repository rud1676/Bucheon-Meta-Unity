using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Container
{
	public class AppData : MonoBehaviour
	{
		[System.Serializable]
		public class KakaoAccount
		{
			public string ageRange;
			public bool ageRangeNeedsAgreement;
			public string gender;
			public bool genderNeedsAgreement;
		}

		[System.Serializable]
		public class KakaoAccountInfo
		{
			public string connectedAt;
			public int id;
			public KakaoAccount kakaoAccount;
		}
	}
}