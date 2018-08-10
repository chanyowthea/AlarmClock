package com.android.myservice;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;

import com.unity3d.player.UnityPlayerActivity;

public class ServiceDemo extends Activity
{
    private static final String TAG = "ServiceDemo";
    public TextView timerView;

    private static ServiceDemo instance;
    public static ServiceDemo getInstance() {
        return instance;
    }

    public void SetTimeView(String s)
    {
        if (null != timerView) {
            timerView.setText(s);
        }
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        startService(new Intent(this, MyService.class));
        instance = this;
        setContentView(R.layout.main);
        timerView = (TextView) this.findViewById(R.id.timerView);
    }

    public void onClickStart(View src) {
        Log.i(TAG, "onClick: starting service");
        MyService.getInstance().CreateTimer();
    }

    public void onClickStop(View src) {
        Log.i(TAG, "onClick: stopping service");
        MyService.getInstance().StopAudio();
    }

    @Override
    protected void onDestroy() {
        stopService(new Intent(this, MyService.class));
        super.onDestroy();
    }
}