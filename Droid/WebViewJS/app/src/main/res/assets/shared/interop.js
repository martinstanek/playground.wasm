function sendMessage(message) {
    document.getElementById("swift").value = message;
}

function callBack(message) {
    try {
        webkit.messageHandlers.callBack.postMessage(message);
    } catch(err) {
        document.getElementById("error").innerHTML = err;
    }
}
