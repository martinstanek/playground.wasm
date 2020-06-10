package com.example.webviewwasm;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.TargetApi;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.webkit.JavascriptInterface;
import android.webkit.WebResourceError;
import android.webkit.WebResourceRequest;
import android.webkit.WebResourceResponse;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.EditText;

import com.google.webviewlocalserver.WebViewLocalServer;

import java.io.File;
import java.io.IOException;

public class MainActivity extends AppCompatActivity {

    private WebViewLocalServer.AssetHostingDetails ahd;
    private WebViewLocalServer localServer;
    private WebView webView;
    private EditText edFromWasm;
    private EditText edToWasm;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_main);

        webView = findViewById(R.id.webView);
        edToWasm = findViewById(R.id.edToWasm);
        edFromWasm = findViewById(R.id.edFromWasm);

        JavaScriptCallbackInterface jsInterface = new JavaScriptCallbackInterface(this);

        localServer = new WebViewLocalServer(this);
        ahd = localServer.hostAssets("WASM");

        webView.getSettings().setJavaScriptEnabled(true);
        webView.addJavascriptInterface(jsInterface, "JSInterface");

        webView.setWebViewClient(new WebViewClient() {
            @Override
            public WebResourceResponse shouldInterceptRequest(WebView view, WebResourceRequest request) {

                return localServer.shouldInterceptRequest(request);
            }
        });

        Button button = findViewById(R.id.button);
        button.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {

                webView.loadUrl(ahd.getHttpsPrefix().buildUpon().appendPath("index.html").build().toString());
            }
        });

        Button buttonSend = findViewById(R.id.buttonSend);
        buttonSend.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {

                webView.evaluateJavascript("callWasmFromJava('" + edToWasm.getText().toString() + "');", null);
            }
        });
    }

    public void onCallBack(String message) {

        edFromWasm.setText(message);
    }
}