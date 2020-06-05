package com.example.webviewjs;

import android.util.Log;
import android.webkit.JavascriptInterface;

import androidx.fragment.app.Fragment;

public class JavaScriptInterface {

    private FirstFragment fragment;

    public JavaScriptInterface(FirstFragment fragment) {
        this.fragment = fragment;
    }

    @JavascriptInterface
    public void logMessage(String message) {

        fragment.onWebViewCallBack(message);
    }
}