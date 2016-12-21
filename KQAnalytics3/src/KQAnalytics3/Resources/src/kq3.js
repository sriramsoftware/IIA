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
let ul = kc['u']
let tid = kc['tid']
let sid = window.localStorage.getItem('sid') || mkuid(26)
window.localStorage.setItem('sid', sid)
let rd = `u=${ul}&tid=${tid}`

String.prototype.format = function () {
  let args = arguments
  return this.replace(/{(\d+)}/g, function (match, number) {
    return typeof args[number] !== 'undefined' ? args[number] : match
  })
};

(function () {
  var xhr = new window.XMLHttpRequest()
  xhr.open('POST', kqs + 'k', true)
  xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded')
  xhr.onload = function () {
    // ok
    // this.responseText
  }
  xhr.send(rd)
})()
