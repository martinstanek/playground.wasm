import UIKit
import WebKit;
import Swifter;

class ViewController: UIViewController {

    @IBOutlet var webViewContainer: UIView!
    @IBOutlet var tbxInput: UITextField!
    @IBOutlet var tbxOutput: UITextField!
    
    var webView: WKWebView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        initWebView()
        loadWebView()
    }
    
    func loadWebView() {
        
        let myRequest = URLRequest(url: URL(string: "http://127.0.0.1:8888")!)
        webView.load(myRequest)
    }
    
    func initWebView() {
        
        let contentController = WKUserContentController()
        contentController.add(self, name: "callBack")
               
        let config = WKWebViewConfiguration()
        config.userContentController = contentController
        
        webView = WKWebView(frame: webViewContainer.bounds, configuration: config)
        webView.translatesAutoresizingMaskIntoConstraints = false
        
        webViewContainer.addSubview(webView)
        
        webView.leadingAnchor.constraint(equalTo: webViewContainer.leadingAnchor, constant: 0).isActive = true
        webView.trailingAnchor.constraint(equalTo: webViewContainer.trailingAnchor, constant: 0).isActive = true
        webView.topAnchor.constraint(equalTo: webViewContainer.topAnchor, constant: 0).isActive = true
        webView.bottomAnchor.constraint(equalTo: webViewContainer.bottomAnchor, constant: 0).isActive = true
    }
    
    func onCallBack(message: String) {
        
        tbxOutput.text = message
    }
    
    @IBAction func btnSend(_ sender: Any) {
        
        self.webView.evaluateJavaScript("callWasm('\(tbxInput.text!)')", completionHandler: nil)
    }
    
    @IBAction func btnReload(_ sender: Any) {

        let myRequest = URLRequest(url: URL(string: "http://127.0.0.1:8888")!)
        webView.load(myRequest)
    }
}

extension ViewController : WKScriptMessageHandler {
    
    func userContentController(_ userContentController: WKUserContentController, didReceive message: WKScriptMessage) {
    
        if message.name == "callBack", let strValue = message.body as? String {
        
            onCallBack(message: strValue)
        }
    }
}
