package com.example.webviewwasm;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.TargetApi;
import android.os.Bundle;
import android.webkit.WebResourceError;
import android.webkit.WebResourceRequest;
import android.webkit.WebResourceResponse;
import android.webkit.WebView;
import android.webkit.WebViewClient;

import com.google.webviewlocalserver.WebViewLocalServer;

import java.io.File;
import java.io.IOException;

public class MainActivity extends AppCompatActivity {

    private WebView webView;
    private WebViewLocalServer localServer;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_main);

        localServer = new WebViewLocalServer(this);

        WebViewLocalServer.AssetHostingDetails ahd = localServer.hostAssets("WASM");

        webView = findViewById(R.id.webView);
        webView.getSettings().setJavaScriptEnabled(true);

        webView.setWebViewClient(new WebViewClient() {
          @Override
            public WebResourceResponse shouldInterceptRequest(WebView view, WebResourceRequest request) {
              return localServer.shouldInterceptRequest(request);
          }
        });

        webView.loadUrl(ahd.getHttpsPrefix().buildUpon().appendPath("index.html").build().toString());
    }
}