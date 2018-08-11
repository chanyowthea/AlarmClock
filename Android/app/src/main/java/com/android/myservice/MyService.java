package com.android.myservice;

import android.app.Service;
import android.content.Intent;
import android.media.MediaPlayer;
import android.os.Handler;
import android.os.IBinder;
import android.os.Message;
import android.util.Log;
import android.widget.Toast;

import java.util.Timer;
import java.util.TimerTask;

public class MyService extends Service {
    private static final String TAG = "MyService";
    MediaPlayer player;
    private static final int WHAT = 101;

    private static MyService instance;

    public static MyService getInstance() {
        return instance;
    }

    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public void onCreate() {
        instance = this;
        Toast.makeText(this, "My Service created", Toast.LENGTH_LONG).show();
        Log.i(TAG, "onCreate");

        player = MediaPlayer.create(this, R.raw.braincandy);
        player.setLooping(true);

//        initTimer();
        // 参数：0，延时0秒后执行;1000，每隔1秒执行1次task。
//        mTimer.schedule(mTimerTask, 0, 1000);
    }

    Handler mHandler = new Handler() {
        @Override
        public void handleMessage(Message msg) {
            switch (msg.what) {
                case WHAT:
                    long sRecLen = (long) msg.obj;
                    ServiceDemo.getInstance().setTimeView("" + sRecLen);
                    if (sRecLen <= 0) {
                        mTimer.cancel();
                        curTime = 0;
                        player.start();
                        ServiceDemo.getInstance().showDialog();
                        Toast.makeText(getInstance(), "count down timer finished. start play audio! ", Toast.LENGTH_SHORT).show();
                    }
                    break;
            }
        }
    };


    @Override
    public void onDestroy() {
        Toast.makeText(this, "My Service Stoped", Toast.LENGTH_LONG).show();
        Log.i(TAG, "onDestroy");
        player.stop();
        player.release();
        destroyTimer();
        if (mHandler != null) {
            mHandler.removeMessages(WHAT);
            mHandler = null;
        }
    }

    @Override
    public void onStart(Intent intent, int startid) {
        Toast.makeText(this, "My Service Start", Toast.LENGTH_LONG).show();
        Log.i(TAG, "onStart");
    }

    private Timer mTimer;
    private TimerTask mTimerTask;

    private static long MAX_TIME = 7000;
    private long curTime = 0;
    private boolean isPause = false;

    public void initTimer() {
        mTimerTask = new TimerTask() {
            @Override
            public void run() {
                if (curTime == 0) {
                    curTime = MAX_TIME;
                } else {
                    curTime -= 1000;
                }
                Message message = new Message();
                message.what = WHAT;
                message.obj = curTime;
                mHandler.sendMessage(message);
            }
        };
        mTimer = new Timer();
    }

    public void destroyTimer() {
        if (mTimer != null) {
            mTimer.cancel();
            mTimer = null;
        }
        if (mTimerTask != null) {
            mTimerTask.cancel();
            mTimerTask = null;
        }
    }

    public void CreateTimer(String title, int type, int day, int hour, int minute, String audioName) {
        Toast.makeText(this, "CreateTimer", Toast.LENGTH_LONG).show();
        destroyTimer();
        initTimer();
        isPause = false;
        mTimer.schedule(mTimerTask, 0, 1000);
    }

    public void StopAudio() {
        Toast.makeText(this, "Clear", Toast.LENGTH_LONG).show();
        player.pause();
        player.seekTo(0);
        curTime = 0;
        isPause = false;
        mTimer.cancel();
    }
}