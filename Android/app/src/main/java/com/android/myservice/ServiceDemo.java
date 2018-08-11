package com.android.myservice;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.TextView;

public class ServiceDemo extends Activity {
    private static final String TAG = "ServiceDemo";
    public TextView timerView;
    private static ServiceDemo instance;

    public static ServiceDemo getInstance() {
        return instance;
    }

    public void setTimeView(String s) {
        if (null != timerView) {
            timerView.setText(s);
        }
    }

    @Override
    protected void onDestroy() {
        stopService(new Intent(this, MyService.class));
        super.onDestroy();
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        instance = this;
        startService(new Intent(this, MyService.class));
        setContentView(R.layout.main);
        timerView = (TextView) this.findViewById(R.id.timeView);
    }

    public void onClickStart(View src) {
        Log.i(TAG, "onClick: starting service");
//        MyService.getInstance().CreateTimer();
    }

    public void onClickStop(View src) {
        Log.i(TAG, "onClick: stopping service");
        MyService.getInstance().StopAudio();
    }


    void showGlobalDialog()
    {
        final AlertDialog dialog = new AlertDialog.Builder(this).setMessage("test activity2  dialog").create();
        dialog.getWindow().setType(WindowManager.LayoutParams.TYPE_SYSTEM_ALERT);

        final WindowManager.LayoutParams alertParams = new WindowManager.LayoutParams();
        alertParams.type = WindowManager.LayoutParams.TYPE_SYSTEM_ALERT;
        alertParams.flags = WindowManager.LayoutParams.FLAG_NOT_TOUCH_MODAL | WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE;
        alertParams.width = WindowManager.LayoutParams.MATCH_PARENT;
        alertParams.height = 300;
        alertParams.format = PixelFormat.RGBA_8888;

        final WindowManager wm = (WindowManager) getSystemService(Context.WINDOW_SERVICE);

        final TextView view = new TextView(getApplicationContext());

        view.setBackgroundColor(Color.GREEN);
        view.setText("test2");
        view.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                wm.removeView(v);
            }
        });

        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                Log.i(getPackageName(), "显示 activity2");
                wm.addView(view, alertParams);
                dialog.show();
            }
        }, 7000);
    }

    public void showDialog()
    {
        View view = View.inflate(this, R.layout.update_manager_dialog, null);
        AlertDialog.Builder b = new AlertDialog.Builder(this);
        b.setView(view);
        final AlertDialog d = b.create();
//        d.getWindow().setType(WindowManager.LayoutParams.TYPE_SYSTEM_DIALOG); //系统中关机对话框就是这个属性
        d.getWindow().setType(WindowManager.LayoutParams.TYPE_SYSTEM_ALERT);  //窗口可以获得焦点，响应操作
        //d.getWindow().setType(WindowManager.LayoutParams.TYPE_SYSTEM_OVERLAY);  //窗口不可以获得焦点，点击时响应窗口后面的界面点击事件
        d.show();

        Button snoozeBtn = (Button) view.findViewById(R.id.dialog_btn_snooze);
        Button closeBtn = (Button) view.findViewById(R.id.dialog_btn_close);

        snoozeBtn.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                MyService.getInstance().StopAudio();
                d.dismiss();
                Log.i(TAG, "showDialog onClick: snooze button");
            }
        });

        closeBtn.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                MyService.getInstance().StopAudio();
                d.dismiss();
                Log.i(TAG, "showDialog onClick: close button");
            }
        });
    }
}