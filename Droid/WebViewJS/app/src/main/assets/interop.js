function sendMessage(message) {
    document.getElementById("java").value = message;
}

function callBack(message) {
    try {
        window.JSInterface.logMessage(document.getElementById("message").value);
    } catch(err) {
        document.getElementById("error").innerHTML = err;
    }
}