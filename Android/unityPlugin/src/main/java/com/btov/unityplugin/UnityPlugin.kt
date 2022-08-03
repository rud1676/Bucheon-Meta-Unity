package com.btov.unityplugin

import android.util.Log
import com.navercorp.nid.NaverIdLoginSDK.initialize
import com.navercorp.nid.NaverIdLoginSDK.authenticate
import social.KaKaoTock
import com.btov.unityplugin.UnityPlugin
import com.navercorp.nid.oauth.OAuthLoginCallback
import com.navercorp.nid.oauth.NidOAuthLogin
import com.navercorp.nid.profile.NidProfileCallback
import com.navercorp.nid.profile.data.NidProfileResponse
import android.widget.Toast
import androidx.core.os.HandlerCompat.postDelayed
import com.unity3d.player.UnityPlayer
import com.google.firebase.messaging.FirebaseMessaging
import com.google.android.gms.tasks.OnCompleteListener
import com.btov.unityplugin.MainActivity
import com.google.gson.Gson
import com.kakao.sdk.user.UserApiClient
import com.navercorp.nid.NaverIdLoginSDK
import kotlinx.coroutines.delay
import java.util.logging.Handler

class UnityPlugin {
    private var kaKaoTock: KaKaoTock? = null
    fun Init() {
        Log.d(TAG, "UnityPlugin Init")
        kaKaoTock = KaKaoTock(_context!!)
        kaKaoTock!!.InitKaKaoSdk()
        NaverIdLoginSDK.initialize(_context!!, NaverClientId, NaverClientSecret, NaverClientName)
        refreshToken();
    }

    fun LoginKaKao() {
        Log.d(TAG, "UnityPlugin LoginKaKaoLogin: ")
        kaKaoTock!!.LoginKaKaoTalk()
    }

    fun LogoutKaKao(){
        kaKaoTock!!.LogoutKaKaoTalk()
    }

    fun LoginNaver() {
        Log.d(TAG, "UnityPlugin LoginNaver: ")
        NaverIdLoginSDK.logout()
        showLoginNaver()
    }

    fun showLoginNaver()
    {
        NaverIdLoginSDK.authenticate(_context!!, object : OAuthLoginCallback {
            override fun onSuccess() {
                NidOAuthLogin().callProfileApi(object :
                    NidProfileCallback<NidProfileResponse> {
                    override fun onSuccess(response: NidProfileResponse) {
                        Log.i(TAG, "사용자 정보 요청 성공" +
                                "\n회원번호: ${response.profile?.id}" +
                                "\n이메일: ${response.profile?.email}")
                        var gson = Gson();
                        var str = gson.toJson(response.profile);
                        UnityPlayer.UnitySendMessage("Android", "CallbackNaver", str);
                    }

                    override fun onFailure(httpStatus: Int, message: String) {
                        val errorCode = NaverIdLoginSDK.getLastErrorCode().code
                        val errorDescription = NaverIdLoginSDK.getLastErrorDescription()
                        Log.i(TAG, "에러" +
                                "\n에러코드: $errorCode" +
                                "\n에러내용: $errorDescription")
                        ShowToast("에러" +
                                "\n에러코드: $errorCode" +
                                "\n에러내용: $errorDescription")
                    }

                    override fun onError(errorCode: Int, message: String) {
                        onFailure(errorCode, message)
                        ShowToast("에러" +
                                "\n에러코드: $errorCode" +
                                "\n에러내용: $message")
                    }
                })
            }

            override fun onFailure(i: Int, s: String) {}
            override fun onError(i: Int, s: String) {}
        })
    }

    fun ShowToast(text: String?) {
        _context!!.runOnUiThread { Toast.makeText(_context, text, Toast.LENGTH_LONG).show() }
    }

    private fun unitySendMessage(objectName: String, methodName: String, param: String) {
        UnityPlayer.UnitySendMessage("Android", "Callback", param)
    }

    private fun refreshToken() {
        FirebaseMessaging.getInstance().token
            .addOnCompleteListener(OnCompleteListener { task ->
                if (!task.isSuccessful) {
                    Log.w("FCM", "Fetching FCM registration token failed", task.exception)
                    return@OnCompleteListener
                }
                // Get new FCM registration token
                val token = task.result
                // Log and toast
                Log.d("FCM", token!!)
                UnityPlayer.UnitySendMessage("Android", "FCMToken", token)
            })
    }

    companion object {
        private const val TAG = "unity UnityPlugin"
        private var _instance: UnityPlugin? = null
        private var _context: MainActivity? = null
        private const val NaverClientId = "kyIXDhF5W3CNElirHP65"
        private const val NaverClientSecret = "VCTXCzm3QY"
        private const val NaverClientName = "부천시 깨끗한마을 메타버스"
        @JvmStatic
        fun instance(): UnityPlugin? {
            if (_instance == null) {
                _instance = UnityPlugin()
                _context = UnityPlayer.currentActivity as MainActivity
            }
            return _instance
        }
    }
}