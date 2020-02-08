package com.example.ertekin.raspberrypidepootomasyon;

import android.graphics.Color;
import android.os.StrictMode;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.TextView;
import android.widget.Toast;

import com.github.mikephil.charting.charts.PieChart;
import com.github.mikephil.charting.data.Entry;
import com.github.mikephil.charting.data.PieData;
import com.github.mikephil.charting.data.PieDataSet;
import com.github.mikephil.charting.data.PieEntry;

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
import java.util.List;

public class ArabaVerileri extends AppCompatActivity {

    List<PieEntry> entries1 = new ArrayList<>(); // Grafiğimizin veri kaynağı olacak liste
    TextView txtarabaad;
    TextView txttarih;
    TextView txtveriid;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);
        setContentView(R.layout.activity_araba_verileri);
        PieChart chartaraba = (PieChart) findViewById(R.id.chartaraba);// Grafiğimiz
        txtveriid=(TextView) findViewById(R.id.txtveriid);
        txttarih=(TextView) findViewById(R.id.txttarih);
        txtarabaad=(TextView) findViewById(R.id.txtarabaad);
        try{
        String veri = arababilgileri();//web servis aracılığıyla ulaştığımız veriler
        Toast.makeText(getApplicationContext(),""+veri,Toast.LENGTH_LONG).show();
        String[] veriler = veri.split(",");//verileri diziye attık
        String vid = veriler[0].substring(1, veriler[0].length() - 1);
        String varaba = veriler[1].substring(1, veriler[1].length() - 1);
        String vsicaklik = veriler[2].substring(1, veriler[2].length() - 1);
        String vnem= veriler[3].substring(1, veriler[3].length() - 1);
        String vtarih = veriler[4].substring(1, veriler[4].length() - 1);
        txtarabaad.setText(txtarabaad.getText()+""+varaba);
        txttarih.setText(txttarih.getText()+""+vtarih);
        txtveriid.setText(txtveriid.getText()+""+vid);
        entries1.add(new PieEntry(Integer.parseInt(vsicaklik), "Sıcaklık"));//Grafik kaynağımıza sıcaklık ve nem
        entries1.add(new PieEntry(Integer.parseInt(vnem), "Nem"));//değerlerini ekledik

        PieDataSet set = new PieDataSet(entries1, "Araba Verileri");//ve grafiğin kaynağını olarak listemizi belirttik
        set.setColors(new int[]{Color.parseColor("#e53935"), Color.parseColor("#03a9f4"), Color.parseColor("#FF0000"), Color.parseColor("#0047AB")});
        set.setValueTextColor(Color.WHITE);//Ardından gerekli ayarlamaları yaptık
        set.setValueTextSize(13);
        chartaraba.setHoleColor(Color.argb(0,255,255,255));
        PieData data = new PieData(set);

        chartaraba.setData(data);
        chartaraba.invalidate(); // refresh
    }
    catch (Exception ex)
    {
        Toast.makeText(getApplicationContext(),""+ex,Toast.LENGTH_LONG).show();
    }
    }

    public String  arababilgileri() {
        String sonuc = "";
        try {
            HttpParams httpParams = new BasicHttpParams();
            HttpConnectionParams.setConnectionTimeout(httpParams,
                    100);
            HttpConnectionParams.setSoTimeout(httpParams, 100);
            HttpParams p = new BasicHttpParams();
            p.setParameter("user", "1");
            HttpClient httpclient = new DefaultHttpClient(p);
            String url = "http://ertechinsoftware.com/wp-content/ArabaVerileri.php?user=1&format=json";
            HttpPost httppost = new HttpPost(url);
            try {
                Log.i(getClass().getSimpleName(), "send  task - start");
                //
                List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(
                        2);
                nameValuePairs.add(new BasicNameValuePair("user", "1"));
                httppost.setEntity(new UrlEncodedFormEntity(nameValuePairs));
                ResponseHandler<String> responseHandler = new BasicResponseHandler();
                String responseBody = httpclient.execute(httppost,
                        responseHandler);
                Log.i(getClass().getSimpleName(), "0");
                Log.i(getClass().getSimpleName(), "" + responseBody.substring(11, responseBody.lastIndexOf(']') - 1));
                sonuc = responseBody.substring(11, responseBody.lastIndexOf(']') - 1);
            } catch (ClientProtocolException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            } catch (IOException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
        } catch (Throwable t) {
            Toast.makeText(this, "Request failed: " + t.toString(),
                    Toast.LENGTH_LONG).show();
        }
        return sonuc;
    }
}