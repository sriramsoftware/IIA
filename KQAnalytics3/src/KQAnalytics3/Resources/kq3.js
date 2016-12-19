function mkuid (lnt) {
  var text = ''
  var possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'
  for (var i = 0; i < lnt; i++) {
    text += possible.charAt(Math.floor(Math.random() * possible.length))
  }
  return text
}

var kc = window._kqdaq || {}
var kqserver = kc['s']
if (kqserver.substr(-1) != '/') kqserver += '/'
var url = kc['u']
var trackingId = kc['tid']
var sid = window.localStorage.getItem('sid') || mkuid(26)
window.localStorage.setItem('sid', sid)

String.prototype.format = function () {
  var args = arguments
  return this.replace(/{(\d+)}/g, function (match, number) {
    return typeof args[number] !== 'undefined' ? args[number] : match
  })
};

(function () {
  // TODO
})()
