package com.example.ertekin.raspberrypidepootomasyon;

import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.format.Formatter;
import android.util.Log;
import android.view.View;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.ImageView;
import android.widget.Toast;

import java.io.IOException;
import java.net.Inet4Address;
import java.util.Timer;
import java.util.TimerTask;

public class MainActivity extends AppCompatActivity {

    ImageView imgdepo;
    ImageView imganaliz;
    Intent depogecis;
    Intent analizgecis;
    Intent arabagecis;
    ImageView imgaraba;
    WebView browser;
    Runnable runnable;
    Handler handler;
    int sayac=0;

    Timer myTimer=new Timer();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        browser = new WebView(this);
        browser.getSettings().setJavaScriptEnabled(true);
        depogecis = new Intent(this, Depo.class);
        analizgecis = new Intent(this, Analiz.class);
        arabagecis = new Intent(this, ArabaVerileri.class);
        imgaraba = (ImageView) findViewById(R.id.imgarac);
        imgdepo = (ImageView) findViewById(R.id.imgdepo);
        imganaliz = (ImageView) findViewById(R.id.imganalizler);
        // Context context=this;
        imgaraba.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(arabagecis);
            }
        });
        imganaliz.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(analizgecis);
            }
        });
        imgdepo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(depogecis);
            }
        });
      /*  ConnectivityManager cm = (ConnectivityManager)context.getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetwork = cm.getActiveNetworkInfo();
        WifiManager wm = (WifiManager)context.getSystemService(Context.WIFI_SERVICE);

        WifiInfo connectionInfo = wm.getConnectionInfo();
        int ipAddress = connectionInfo.getIpAddress();
        String ipString = Formatter.formatIpAddress(ipAddress);*/
        //Toast.makeText(getApplicationContext(),""+ipString,Toast.LENGTH_LONG).show();
        browser.setWebViewClient(new WebViewClient());
        browser.loadUrl("http://192.168.1.130");
        for(int i=0;i<1000;i++)
        {
            browser.loadUrl("http://192.168.1.130");
            for(int a = 0; a<1000;a++)
            {

            }
        }

    }
}
