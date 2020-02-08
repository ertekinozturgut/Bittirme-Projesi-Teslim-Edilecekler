package com.example.ertekin.raspberrypidepootomasyon;

import android.os.StrictMode;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.TextView;
import android.widget.Toast;

import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.BasicResponseHandler;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;
import org.json.JSONArray;
import org.json.JSONObject;
import org.w3c.dom.Text;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Depo extends AppCompatActivity {

    TextView txtid;
    TextView txtad;
    TextView txtadres;
    TextView txtdurum;
    TextView txtbaslama;
    TextView txtbitis;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);
        setContentView(R.layout.activity_depo);

        txtid = (TextView) findViewById(R.id.txtdid);
        txtad = (TextView) findViewById(R.id.txtad);
        txtadres = (TextView) findViewById(R.id.txtdadres);
        txtdurum = (TextView) findViewById(R.id.txtddurum);
        txtbaslama = (TextView) findViewById(R.id.txtbaslama);
        txtbitis = (TextView) findViewById(R.id.txtbitis);
        try {
            String veri = depobilgileri();
            //Toast.makeText(getApplicationContext(),""+veriler,Toast.LENGTH_LONG).show();
            String[] veriler = veri.split(",");
            String did = veriler[0].substring(1, veriler[0].length() - 1);
            String dad = veriler[1].substring(1, veriler[1].length() - 1);
            String dadres = veriler[2].substring(1, veriler[2].length() - 1);
            String ddurum = veriler[3].substring(1, veriler[3].length() - 1);
            String dbaslangic = veriler[4].substring(1, veriler[4].length() - 1);
            String dbitis = veriler[5].substring(1, veriler[5].length() - 1);

            txtid.setText("" + did);
            txtad.setText("" + dad);
            txtadres.setText("" + dadres);
            txtbaslama.setText("" + dbaslangic);
            txtbitis.setText("" + dbitis);
            txtdurum.setText("" + ddurum);

        }
        catch (Exception ex)
        {
            Toast.makeText(getApplicationContext(),""+ex,Toast.LENGTH_LONG).show();
        }

    }
    public String depobilgileri()
    {
        String sonuc="";
        try {
            HttpParams httpParams = new BasicHttpParams();
            HttpConnectionParams.setConnectionTimeout(httpParams, 100);
            HttpConnectionParams.setSoTimeout(httpParams, 100);
            HttpParams p = new BasicHttpParams();
            p.setParameter("user", "1");
            HttpClient httpclient = new DefaultHttpClient(p);
            String url = "http://ertechinsoftware.com/wp-content/DepoBilgileri.php?user=1&format=json";
            HttpPost httppost = new HttpPost(url);
            try {
                Log.i(getClass().getSimpleName(), "send  task - start");
                List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(
                        2);
                nameValuePairs.add(new BasicNameValuePair("user", "1"));
                httppost.setEntity(new UrlEncodedFormEntity(nameValuePairs));
                ResponseHandler<String> responseHandler = new BasicResponseHandler();
                String responseBody = httpclient.execute(httppost, responseHandler);
                Log.i(getClass().getSimpleName(), "0");
                Log.i(getClass().getSimpleName(), ""+responseBody.substring(11,responseBody.lastIndexOf(']')-1));
                sonuc=responseBody.substring(11,responseBody.lastIndexOf(']')-1);
            } catch (ClientProtocolException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
        } catch (Throwable t) {
            Toast.makeText(this, "Request failed: " + t.toString(),
                    Toast.LENGTH_LONG).show();
        }return sonuc;
    }
}
