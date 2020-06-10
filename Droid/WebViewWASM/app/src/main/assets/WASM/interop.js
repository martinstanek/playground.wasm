function callWasm(name)
{
    document.getElementById("output").value = "calling wasm ...";
    document.getElementById("output").value = sayHelloWasm(name);
    
    //webkit.messageHandlers.callBack.postMessage(document.getElementById("output").value);
    //window.JSInterface.logMessage(document.getElementById("output").value);
}

function sayHelloWasm(name)
{
  const sayHello = Module.mono_bind_static_method("[example] Example:SayHello");

  return sayHello(name);
}
