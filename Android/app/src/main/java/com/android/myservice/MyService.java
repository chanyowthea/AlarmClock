package com.android.myservice;

import android.app.Service;
import android.content.Intent;
import android.media.MediaPlayer;
import android.os.Handler;
import android.os.IBinder;
import android.os.Message;
import android.os.SystemClock;
import android.util.Log;
import android.widget.TextView;
import android.widget.Toast;

import java.text.DecimalFormat;
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
        player.setLooping(false);

        initTimer();
        // 参数：0，延时0秒后执行;1000，每隔1秒执行1次task。
        mTimer.schedule(mTimerTask, 0, 1000);
    }

    Handler mHandler = new Handler() {
        @Override
        public void handleMessage(Message msg) {
            switch (msg.what) {
                case WHAT:
//                    Toast.makeText(getInstance(), "结束", Toast.LENGTH_SHORT).show();
                    long sRecLen = (long) msg.obj;
                    //毫秒换成00:00:00格式的方式，自己写的。
//                    mTimerTv.setText(TimeTools.getCountTimeByLong(sRecLen));
                    if (sRecLen <= 0) {
                        mTimer.cancel();
                        curTime = 0;
                        player.start();
//                        Toast.makeText(getInstance(), "结束", Toast.LENGTH_SHORT).show();
                    }
                    break;
            }
        }
    };


    @Override
    public void onDestroy() {
        Toast.makeText(this, "My Service Stoped", Toast.LENGTH_LONG).show();
        Log.i(TAG, "onDestroy");
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

    private static final long MAX_TIME = 3000;
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
//                ServiceDemo.getInstance().SetTimeView("464646");
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


    public void CreateTimer() {
        Toast.makeText(this, "CreateTimer", Toast.LENGTH_LONG).show();
        destroyTimer();
        initTimer();
        isPause = false;
        mTimer.schedule(mTimerTask, 0, 1000);


//        final Handler startTimehandler = new Handler() {
//            public void handleMessage(android.os.Message msg) {
//                ServiceDemo.getInstance().SetTimeView((String) msg.obj);
//            }
//        };
//        new Timer("开机计时器").scheduleAtFixedRate(new TimerTask() {
//            @Override
//            public void run() {
//                // SystemClock.elapsedRealTime is milliseconds
//                // time is seconds
//                int time = (int) ((SystemClock.elapsedRealtime() - baseTimer) / 1000);
//                String hh = new DecimalFormat("00").format(time / 3600);
//                String mm = new DecimalFormat("00").format(time % 3600 / 60);
//                String ss = new DecimalFormat("00").format(time % 60);
//                String timeFormat = new String(hh + ":" + mm + ":" + ss);
//                Message msg = new Message();
//                msg.obj = timeFormat;
//
//                if (time == 3) {
//                    player.start();
//                    msg.obj = "play audio! ";
////                    startTimehandler.sendMessage(msg);
//                } else {
////                    startTimehandler.sendMessage(msg);
//                }
//            }
//
//        }, 0, 1000L);
    }

    public void StopAudio() {
        Toast.makeText(this, "StopAudio", Toast.LENGTH_LONG).show();
        player.stop();
        curTime = 0;
        isPause = false;
        mTimer.cancel();
    }
}