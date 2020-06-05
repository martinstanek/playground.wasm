function callWasm(name)
{
    document.getElementById("output").value = "calling wasm ...";
    document.getElementById("output").value = sayHelloWasm(name);
}

function sayHelloWasm(name)
{
  const sayHello = Module.mono_bind_static_method("[example] Example:SayHello");

  return sayHello(name);
}
