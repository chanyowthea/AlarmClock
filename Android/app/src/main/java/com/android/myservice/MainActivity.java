package com.android.myservice;
import com.unity3d.player.UnityPlayerActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

public class MainActivity extends Activity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Intent intent = new Intent(this, ServiceDemo.class);
        this.startActivity(intent);
    }

    public void PlayAudio(String audioName)
    {
        MyService.getInstance().CreateTimer();
    }

    public void StopAudio()
    {
        MyService.getInstance().StopAudio();
    }
}
