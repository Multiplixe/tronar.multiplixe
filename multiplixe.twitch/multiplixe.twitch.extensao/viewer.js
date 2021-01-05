var token = "";
var tuid = "";
var channelId = "";
var clientId = "";
var _ebs = "https://api.multiplyx.me/restrito/twitch";
var ebs = "http://localhost:63623/restrito/twitch";
var pingTimeout = 0;
var timePing = 0
var alreadyInitialized = false;
var giveBackHeader = {};
var timeoutContextExecuting = 0;
var isContextExecuting = false;
var timeoutClock = 0;
var twitch = window.Twitch.ext;
var seconds = 0;
var finishedCurrentPing = false;
var timeoutShowTitleExtension = 1000 * 30;
var timeoutHiddenTitleExtension = 1000 * 60 * 5;
var timeoutAd = 0;

var startPause = startPause;
var pauseCounter = 0;

var requests =
{
    init: () => { send('GET', '/ping', proccessInitSuccessResponse, proccessInitErrorResponse) },
    ping: () => { send('POST', '/ping', proccessPingSuccessResponse, proccessPingErrorResponse, proccessPingCompleteResponse) },
};

function send(type, method, proccessSuccess, proccessError, proccessComplete, params) {

    var options = {
        headers: {
            Authorization: 'Bearer ' + token,
            'ping-pause': btoa(pauseCounter),
            'empresa-id' : '5F22E669-8CF2-4702-A828-B32E832A6BA6'
        }
    };

    Object.keys(giveBackHeader).forEach((item) => {
        options.headers[item] = giveBackHeader[item];
    });

    proccessComplete = proccessComplete || (() => { });
    params = params || [];

    var querystring = params.join('/');

    $.ajax({
        content_type: 'application/json',
        type: type,
        url: ebs + method + '/' + querystring,
        success: proccessSuccess,
        error: proccessError,
        complete: proccessComplete,
        headers: options.headers
    });
}

twitch.onAuthorized(function (auth) {

    log("token mudou");
    log("isLinked", twitch.viewer.isLinked);

    if (!twitch.viewer.isLinked) {
        twitch.actions.requestIdShare();
    }
    else {
        setValues(auth);

        if (!alreadyInitialized) {
            requests.init();
        }
    }

    twitch.listen("whisper-" + auth.userId, function (target, contentType, message) {

        let whisper = JSON.parse(message);

        let valid = whisper && whisper.activity;

        if (valid && whisper.activity == 'score') {
            showScore(whisper.score);
        }

        //log("whisper", message);
    });

    //twitch.send('whisper-' + auth.userId, 'application/json', { x: 991 });

});


twitch.onContext(function (context) {

    startContextExecute();

    if (canToPing()) {
        requests.ping();
    }

    contextExecuting();
});

function showScore(o) {

    var e = $("#registered");

    $("#message-start").hide();
    $("#message-score").hide();
    $("#pointing-total-one").hide();
    $("#pointing-total-n").hide();

    if (o &&
        o.pointing &&
        o.pointing.value) {

        let value = o.pointing.value;

        if (value == 0) {
            $("#message-start").show();
        }
        else {
            $("#message-score").show();
        }

        if (value == 1) {
            $("#pointing-total-one").show();
        }
        else {
            $("#pointing-total-n").show();
        }

        $("#pointing-total").text(value)

        let width = 100 + (value.toString().length * 10);

        e.css("width", width + "px");

        setTimeout(() => {
            e.css("width", "")
        }, 10000);
    }
    else
    {
        error("showScore", o)
    }

}

function contextExecuting() {

    clearTimeout(timeoutContextExecuting);

    timeoutContextExecuting = setTimeout(() => {
        pause();
    }, 10000);

}

function pause() {
    startPause = new Date();
    isContextExecuting = false;
    stopClock();
}

function startContextExecute() {

    if (!isContextExecuting) {
        isContextExecuting = true;
        setAfterStartContextExecute();
    }
}

function setAfterStartContextExecute() {

    if (alreadyInitialized) {
        if (startPause) {
            pauseCounter += (new Date()) - (startPause);
        }
        startClock();
    }
}

function clock() {

    timeoutClock = setTimeout(() => {
        seconds++;
        //log("clock", seconds)
        if (canToPing()) {
            finishedCurrentPing = false;
            requests.ping();
        }
        else {
            startClock();
        }

    }, 1000);
}

function startClock() {
    stopClock();
    clock();
}

function resetClock() {
    stopClock();
    seconds = 0;
    startClock();
}

function resetPause() {
    pauseCounter = 0;
    startPause = undefined;
}

function stopClock() {
    clearTimeout(timeoutClock);
}

function setValues(auth) {

    token = auth.token;
    tuid = auth.userId;
    channelId = auth.channelId;
    clientId = auth.clientId;
    log("token", auth);
}

function canToPing() {
    var _canToPing =
        alreadyInitialized &&
        finishedCurrentPing &&
        (seconds == pingTimeout);
    return _canToPing
}

function proccessInitSuccessResponse(r) {
    log("init success", r)

    setGiveBackHeader(r);
    setPingTimeout(r);
    registered();
    finishedCurrentPing = true;
}

function proccessInitErrorResponse(r) {

    log('Init error', r)

    if (r.status == 404) {
        noRegister(r);
    }

    setTimeout(() => {
        clearTimeout(timeoutAd);
        $("#no-register").css("display", "none");
        requests.init();
    }, 300000);

}
function setGiveBackHeader(r) {

    if (r.item) {
        giveBackHeader = r.item.giveBackHeader
    }
}

function setPingTimeout(r) {
    pingTimeout = parseInt(r.item.pingTimeout, 10) * 60;
}

function registered() {
    resetPause();
    resetClock();
    alreadyInitialized = true;
    $("#registered").fadeIn();
}

function noRegister(r) {
    $("#no-register").fadeIn();

    if (r &&
        r.responseJSON &&
        r.responseJSON.item &&
        r.responseJSON.item.title) {
        $("#title-extension").html(r.responseJSON.item.title);
    }

    controlTitleExtension();
}

function controlTitleExtension(t) {

    t = t || timeoutShowTitleExtension;

    timeoutAd = setTimeout(() => {
        var display = $("#no-register").css("display")
        if (display == 'none') {
            $("#no-register").css("display", "block");
            controlTitleExtension(timeoutShowTitleExtension);
        }
        else {
            $("#no-register").css("display", "none");
            controlTitleExtension(timeoutHiddenTitleExtension);
        }
    }, t);
}

function proccessPingSuccessResponse(r) {
    log('Ping ok', new Date());
    setGiveBackHeader(r);
}

function proccessPingErrorResponse(r) {
    log('Ping error', r);
    setGiveBackHeader(r.responseJSON);
}

function proccessPingCompleteResponse(r) {
    log('Ping complete', r)
    resetClock();
    resetPause();
    finishedCurrentPing = true;
}

function log(title, value) {
    //console.log(title, value);
}

function error(method, o) {
    console.log("method", method);
    console.log("param", o);
}

$(function () {



});
