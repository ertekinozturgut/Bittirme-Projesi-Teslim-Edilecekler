package com.example.ertekin.raspberrypidepootomasyon;

import android.graphics.Color;
import android.os.StrictMode;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;


import com.github.mikephil.charting.charts.BarChart;
import com.github.mikephil.charting.charts.PieChart;
import com.github.mikephil.charting.data.BarData;
import com.github.mikephil.charting.data.BarDataSet;
import com.github.mikephil.charting.data.BarEntry;
import com.github.mikephil.charting.data.BarData.*;
import com.github.mikephil.charting.data.PieData;
import com.github.mikephil.charting.data.PieDataSet;
import com.github.mikephil.charting.data.PieEntry;
import com.github.mikephil.charting.formatter.IndexAxisValueFormatter;
import com.github.mikephil.charting.interfaces.datasets.IBarDataSet;

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

import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class Analiz extends AppCompatActivity {
   // List<PieEntry> entries1 = new ArrayList<>();

    ImageView imggaz;
    ImageView imgpir;
    TextView txttarih;
    EditText edttumveri;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_analiz);
        BarChart chart=(BarChart) findViewById(R.id.chart);
        txttarih=(TextView) findViewById(R.id.txtverisaati);
        imggaz=(ImageView) findViewById(R.id.imggaz);
        imgpir=(ImageView) findViewById(R.id.imgpir);
        edttumveri=(EditText) findViewById(R.id.edttumveri);
        edttumveri.setEnabled(false);

       ArrayList<String> labels=new ArrayList<>();
            labels.add("Sıcaklık");
            labels.add("");
            labels.add("Nem");
            labels.add("");
            labels.add("Gaz");
            labels.add("");

            try {
                String veri=sensorbilgileri();
                String[] veriler=veri.split(",");
                int sicaklik=Integer.parseInt(veriler[2].substring(1,veriler[2].length()-1));
                int nem=Integer.parseInt(veriler[3].substring(1,veriler[3].length()-1));
                int gaz=Integer.parseInt(veriler[4].substring(1,veriler[4].length()-1));
                String tarih=veriler[5].substring(1,veriler[5].length()-1);
                int veriid=Integer.parseInt(veriler[0].substring(1,veriler[0].length()-1));
                int hareket=Integer.parseInt(veriler[1].substring(1,veriler[1].length()-1));

                if(hareket==1)
                {
                    imgpir.setImageResource(R.drawable.warning);
                }
                if(hareket==0)
                {
                    imgpir.setImageResource(R.drawable.tick);
                }
                if (gaz<380)
                {
                    imggaz.setImageResource(R.drawable.tick);
                }
                if (gaz>=380)
                {
                    imggaz.setImageResource(R.drawable.warning);
                }
                txttarih.setText("Verinin Tarihi :\n"+tarih);
                edttumveri.setText("Veri ID: "+veriid+"\nSıcaklık: "+sicaklik+"\nNem: "+nem+"\nGaz: "+gaz+"\nHareket"+
                hareket+"\n");
                List<BarEntry> entries = new ArrayList<>();
                entries.add(new BarEntry(0f, sicaklik));
                entries.add(new BarEntry(2f, nem));
                entries.add(new BarEntry(4f, gaz));

        // gap of 2f


        BarDataSet set = new BarDataSet(entries,"");
        BarData data = new BarData(set);
        chart.getXAxis().setValueFormatter(new IndexAxisValueFormatter(labels));

        set.setColors(new int[] {Color.parseColor("#00B500"),Color.parseColor("#FFED00"),Color.parseColor("#FF0000"),Color.parseColor("#0047AB")});

        data.setBarWidth(0.9f); // set custom bar width
        chart.setData(data);
        chart.setFitBars(true); // make the x-axis fit exactly all bars
        chart.invalidate(); // refresh

    }
    catch (Exception ex)
    {
        Toast.makeText(getApplicationContext(),""+ex,Toast.LENGTH_LONG).show();
    }

    }
    public String sensorbilgileri()
    {
        String sonuc="";
        try {
            // http://androidarabia.net/quran4android/phpserver/connecttoserver.php

            // Log.i(getClass().getSimpleName(), "send  task - start");
            HttpParams httpParams = new BasicHttpParams();
            HttpConnectionParams.setConnectionTimeout(httpParams,
                    100);
            HttpConnectionParams.setSoTimeout(httpParams, 100);
            //
            HttpParams p = new BasicHttpParams();
            // p.setParameter("name", pvo.getName());
            p.setParameter("user", "1");

            // Instantiate an HttpClient
            HttpClient httpclient = new DefaultHttpClient(p);
            String url = "http://ertechinsoftware.com/wp-content/SensorVerileri.php?user=1&format=json";

            HttpPost httppost = new HttpPost(url);

            // Instantiate a GET HTTP method
            try {
                Log.i(getClass().getSimpleName(), "send  task - start");
                //
                List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(
                        2);
                nameValuePairs.add(new BasicNameValuePair("user", "1"));
                Log.i(getClass().getSimpleName(), "1");
                httppost.setEntity(new UrlEncodedFormEntity(nameValuePairs));
                Log.i(getClass().getSimpleName(), "2");
                ResponseHandler<String> responseHandler = new BasicResponseHandler();
                Log.i(getClass().getSimpleName(), "3");
                String responseBody = httpclient.execute(httppost,
                        responseHandler);
                Log.i(getClass().getSimpleName(), "0");
                Log.i(getClass().getSimpleName(), ""+responseBody.substring(11,responseBody.lastIndexOf(']')-1));
                sonuc=responseBody.substring(11,responseBody.lastIndexOf(']')-1);
                /*
                // Parse
               //JSONObject json = new JSONObject(responseBody);
         //       JSONArray json=new JSONArray(responseBody);
              //  JSONArray jArray = json.getJSONArray("posts");
                JSONObject json = new JSONObject(responseBody);


                JSONArray jArray = json.getJSONArray("posts");
                Log.i(getClass().getSimpleName(), "2");

                ArrayList<HashMap<String, String>> mylist =
                        new ArrayList<HashMap<String, String>>();

             //   JSONArray jsonArray = new JSONArray(responseBody);
            //    JSONObject jsonObjWoa = jsonArray.getJSONObject(0);
            //    JSONArray jsonArrayWoa = jsonObjWoa.getJSONArray("posts");
           //     for (int i = 0; i < jsonArrayWoa.length(); ++i) {
                   // listWoa.add(jsonArrayWoa.getString(i));

                for (int i = 0; i < jArray.length(); i++) {
                    HashMap<String, String> map = new HashMap<String, String>();
                    Log.i(getClass().getSimpleName(), "3");

                    JSONObject e = jArray.getJSONObject(i);
                    Log.i(getClass().getSimpleName(), "3+1");
                    //   JSONObject e = jsonArrayWoa.getJSONObject(i);
                    String s = e.getString("post");
                    JSONObject jObject = new JSONObject(s);
                    Log.i(getClass().getSimpleName(), "4");


                 //   map.put("Sicaklik",jObject.getString("Sicaklik"));
                 //   map.put("Nem",jObject.getString("Nem"));

                   map.put("Depo_Id", String.valueOf(jObject.getInt("Depo_Id")));
                    map.put("Depo_Adi", jObject.getString("Depo_Adi"));
                    map.put("Depo_Adresi", jObject.getString("Depo_Adresi"));
                    map.put("Depo_Durumu", String.valueOf(jObject.getString("Depo_Durumu")));
                  map.put("Depo_Baslama_Saati", jObject.getString("Depo_Baslama_Saati"));
                    map.put("Depo_Bitis_Saati", jObject.getString("Depo_Bitis_Saati"));

                    mylist.add(map);
                }
                Toast.makeText(this, responseBody, Toast.LENGTH_LONG).show();*/

            } catch (ClientProtocolException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            } catch (IOException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
            // Log.i(getClass().getSimpleName(), "send  task - end");

        } catch (Throwable t) {
            Toast.makeText(this, "Request failed: " + t.toString(),
                    Toast.LENGTH_LONG).show();
        }
        return sonuc;
    }
}
