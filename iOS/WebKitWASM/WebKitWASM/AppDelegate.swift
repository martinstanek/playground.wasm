import UIKit
import Swifter;

@UIApplicationMain
class AppDelegate: UIResponder, UIApplicationDelegate {

    let server = HttpServer()
    
    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        
        server["managed/:path"] = shareFilesFromDirectory(Bundle.main.resourcePath! + "/WASM/managed")
        server["/:path"] = shareFilesFromDirectory(Bundle.main.resourcePath! + "/WASM/")
        server["/"] = shareFile(Bundle.main.resourcePath! + "/WASM/index.html")
        
        try! server.start(8888)
              
        return true
    }

    // MARK: UISceneSession Lifecycle

    func application(_ application: UIApplication, configurationForConnecting connectingSceneSession: UISceneSession, options: UIScene.ConnectionOptions) -> UISceneConfiguration {
        return UISceneConfiguration(name: "Default Configuration", sessionRole: connectingSceneSession.role)
    }

    func application(_ application: UIApplication, didDiscardSceneSessions sceneSessions: Set<UISceneSession>) {
    }
}
