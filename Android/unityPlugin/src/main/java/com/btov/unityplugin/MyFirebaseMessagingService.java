package com.btov.unityplugin;

import android.util.Log;

import com.google.firebase.messaging.FirebaseMessagingService;
import com.google.firebase.messaging.RemoteMessage;
import com.unity3d.player.UnityPlayer;

public class MyFirebaseMessagingService extends FirebaseMessagingService {
    private static final String TAG = "MyFirebaseMsg";

    @Override
    public void onNewToken(String token) {
        super.onNewToken(token);
        Log.d("NEW_TOKEN",token);
        UnityPlayer.UnitySendMessage("Android", "RefreshNewToken", token);
    }

    @Override
    public void onMessageReceived(RemoteMessage remoteMessage) {
        // TODO(developer): Handle FCM messages here.
        Log.d(TAG, "From: " + remoteMessage.getFrom());

        // Check if message contains a notification payload.
        if (remoteMessage.getNotification() != null) {
            Log.d(TAG, "Message Notification Body: " + remoteMessage.getNotification().getBody());
        }
    }
}
