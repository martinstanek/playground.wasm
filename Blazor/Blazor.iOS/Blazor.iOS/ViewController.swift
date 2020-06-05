//
//  ViewController.swift
//  Blazor.iOS
//
//  Created by Martin Stanek on 24.05.20.
//  Copyright © 2020 AWiTEC. All rights reserved.
//

import UIKit
import WebKit;

class ViewController: UIViewController {

    @IBOutlet var webView: WKWebView!
    
    override func viewDidLoad() {
        
        super.viewDidLoad()
        
        let url = Bundle.main.url(forResource: "index", withExtension: "html", subdirectory: "")!
        webView.loadFileURL(url, allowingReadAccessTo: url)
        let request = URLRequest(url: url)
        webView.load(request)
    }
}
