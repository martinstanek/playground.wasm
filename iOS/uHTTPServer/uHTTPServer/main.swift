import Foundation
import Swifter;

print("Hello, World!")

let server = HttpServer()
server["managed/:path"] = shareFilesFromDirectory("/Users/martinstanek/Desktop/WebKitWASM/WebKitWASM/WASM/managed")
server["/:path"] = shareFilesFromDirectory("/Users/martinstanek/Desktop/WebKitWASM/WebKitWASM/WASM/")
server["/"] = shareFile("/Users/martinstanek/Desktop/WebKitWASM/WebKitWASM/WASM/index.html")
try! server.start(8888)

RunLoop.main.run()
