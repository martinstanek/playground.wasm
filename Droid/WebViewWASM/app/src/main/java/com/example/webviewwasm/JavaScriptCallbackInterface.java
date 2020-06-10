package com.example.webviewwasm;

import android.webkit.JavascriptInterface;

public class JavaScriptCallbackInterface {

    private MainActivity mainActivity;

    public JavaScriptCallbackInterface(MainActivity fragment) {
        this.mainActivity = fragment;
    }

    @JavascriptInterface
    public void logMessage(String message) {

        mainActivity.onCallBack(message);
    }
}