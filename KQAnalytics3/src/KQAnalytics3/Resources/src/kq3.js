function mid () {
  function s4 () {
    return Math.floor((1 + Math.random()) * 0x10000)
      .toString(16)
      .substring(1)
  }
  return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
    s4() + '-' + s4() + s4() + s4()
}

let kc = window._kqdaq || {}
let kqs = kc['s']
if (kqs.substr(-1) !== '/') kqs += '/'
let ul = kc['u']
let tid = kc['tid']
let sid = window.localStorage.getItem('sid') || mid()
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
