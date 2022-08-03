package social

import android.content.Context
import android.util.Log
import com.google.gson.Gson
import com.kakao.sdk.common.KakaoSdk
import com.kakao.sdk.user.UserApiClient
import com.unity3d.player.UnityPlayer
import org.json.JSONObject

open class KaKaoTock (private var context: Context) {
    private val TAG = "KaKaoTock"
    private val KaKaoHashKey = "8c572b4faf4e7933a09995984c375466"

    fun InitKaKaoSdk() {
        KakaoSdk.init(context, KaKaoHashKey) // 해시키
    }

    fun LogoutKaKaoTalk(){
        UserApiClient.instance.logout{

        };
    }

    fun LoginKaKaoTalk(){
        if(UserApiClient.instance.isKakaoTalkLoginAvailable(context)){
            // 카톡으로 로그인
            UserApiClient.instance.loginWithKakaoTalk(context){token, error ->
                if(error != null){
                    // 로그인 실패
                    Log.i(TAG, "LoginKaKaoTalk err: ${error.message}")
                }
                else if (token != null){
                    Log.i(TAG, "LoginKaKaoTalk: ${token.accessToken}")
                    requestMe()
                }
            }
        } else {
            // 카카오계정으로 로그인
            UserApiClient.instance.loginWithKakaoAccount(context){token, error ->
                if(error != null){
                    // 로그인 실패
                    Log.i(TAG, "LoginKaKaoTalk err: ${error.message}")
                }
                else if (token != null){
                    Log.i(TAG, "LoginKaKaoTalk: ${token.accessToken}")
                    requestMe()
                }
            }
        }
    }

    fun requestMe(){
        UserApiClient.instance.me { user, error ->
            if (error != null) {
                Log.e(TAG, "사용자 정보 요청 실패", error)
                UnityPlayer.UnitySendMessage("Android", "CallbackKaKao", null);
            }
            else if (user != null) {
                Log.i(TAG, "사용자 정보 요청 성공" +
                        "\n회원번호: ${user.id}" +
                        "\n이메일: ${user.kakaoAccount?.email}" +
                        "\n닉네임: ${user.kakaoAccount?.profile?.nickname}" +
                        "\n프로필사진: ${user.kakaoAccount?.profile?.thumbnailImageUrl}")
                var gson = Gson();
                var str = gson.toJson(user);
                UnityPlayer.UnitySendMessage("Android", "CallbackKaKao", str);
            }
        }
    }

}