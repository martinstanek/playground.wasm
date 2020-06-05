import UIKit
import WebKit;

class ViewController: UIViewController {

    var webView: WKWebView!
    
    @IBOutlet var viewWebContainer: UIView!
    @IBOutlet var btnMagic: UIButton!
    @IBOutlet var txtMessage: UITextField!
    @IBOutlet var txtShared: UITextField!
    
    override func viewDidLoad() {
        
        super.viewDidLoad()
        
        initWebView()
        loadWebView()
        
        txtMessage.resignFirstResponder()
        txtShared.resignFirstResponder()
    }

    @IBAction func btnMagicTouch(_ sender: Any) {
        
        self.webView.evaluateJavaScript("sendMessage('\(txtMessage.text!)')", completionHandler: nil)
    }
    
    func onCallBack(message: String) {
        
        txtShared.text = message
    }
    
    func initWebView() {
        
        let contentController = WKUserContentController()
        contentController.add(self, name: "callBack")
               
        let config = WKWebViewConfiguration()
        config.userContentController = contentController
               
        webView = WKWebView(frame: viewWebContainer.bounds, configuration: config)
        webView.translatesAutoresizingMaskIntoConstraints = false
        
        viewWebContainer.addSubview(webView)
        
        webView.leadingAnchor.constraint(equalTo: viewWebContainer.leadingAnchor, constant: 0).isActive = true
        webView.trailingAnchor.constraint(equalTo: viewWebContainer.trailingAnchor, constant: 0).isActive = true
        webView.topAnchor.constraint(equalTo: viewWebContainer.topAnchor, constant: 0).isActive = true
        webView.bottomAnchor.constraint(equalTo: viewWebContainer.bottomAnchor, constant: 0).isActive = true
    }
    
    func loadWebView() {
        
        let url = Bundle.main.url(forResource: "index", withExtension: "html", subdirectory: "")!
        
        webView.loadFileURL(url, allowingReadAccessTo: url)
        
        let request = URLRequest(url: url)
        
        webView.load(request)
    }
}

extension ViewController : WKScriptMessageHandler {
    
    func userContentController(_ userContentController: WKUserContentController, didReceive message: WKScriptMessage) {
    
        if message.name == "callBack", let strValue = message.body as? String {
        
            onCallBack(message: strValue)
        }
    }
}
