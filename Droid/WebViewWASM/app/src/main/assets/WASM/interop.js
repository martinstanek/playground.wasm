function callWasm(name)
{
    document.getElementById("output").value = "calling wasm from JS ...";
    document.getElementById("output").value = sayHelloWasm(name);
    
    window.JSInterface.logMessage(document.getElementById("output").value);
}

function callWasmFromJava(name)
{
    document.getElementById("output").value = "calling wasm from Java...";
    document.getElementById("output").value = sayHelloWasm(name);
}

function sayHelloWasm(name)
{
  const sayHello = Module.mono_bind_static_method("[example] Example:SayHello");

  return sayHello(name);
}
