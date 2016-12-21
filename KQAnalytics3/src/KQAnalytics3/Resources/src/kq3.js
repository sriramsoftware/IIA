function mkuid (ln) {
  let t = ''
  let ch = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789'
  for (let i = 0; i < ln; i++) {
    t += ch.charAt(Math.floor(Math.random() * ch.length))
  }
  return t
}

let kc = window._kqdaq || {}
let kqs = kc['s']
if (kqs.substr(-1) !== '/') kqs += '/'
let url = kc['u']
let trackingId = kc['tid']
let sid = window.localStorage.getItem('sid') || mkuid(26)
window.localStorage.setItem('sid', sid)

String.prototype.format = function () {
  let args = arguments
  return this.replace(/{(\d+)}/g, function (match, number) {
    return typeof args[number] !== 'undefined' ? args[number] : match
  })
};

(function () {
  // TODO
  var request = new window.XMLHttpRequest()
  request.open('POST', '/my/url', true)
  request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8')
  request.send({})
})()
