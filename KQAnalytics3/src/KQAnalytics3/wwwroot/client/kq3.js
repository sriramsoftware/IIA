function mkuid(lnt) {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    for (var i = 0; i < lnt; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));
    return text;
}

var kqserver = _kqdaq['s'];
var url = _kqdaq['u'];
var trackingId = _kqdaq['tid'];
var sessid = localStorage.getItem("sid") || mkuid(26);
localStorage.setItem("sid", sessid);

String.prototype.format = function () {
    var args = arguments;
    return this.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
};

(function () {
    // TODO
})();