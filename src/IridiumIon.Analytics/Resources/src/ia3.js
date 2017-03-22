class IAUtils {
  static serialize (obj) {
    var str = []
    for (var p in obj) {
      if (obj.hasOwnProperty(p)) {
        str.push(encodeURIComponent(p) + '=' + encodeURIComponent(obj[p]))
      }
    }
    return str.join('&')
  }
  static mid () {
    function s4 () {
      return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1)
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
      s4() + '-' + s4() + s4() + s4()
  }
  static sendPost (url, data, callback) {
    var xhr = new window.XMLHttpRequest()
    xhr.open('POST', url, true)
    xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded')
    xhr.onload = function () {
      // ok
      // this.responseText
      if (callback) {
        callback(this.responseText)
      }
    }
    xhr.send(IAUtils.serialize(data))
  }
}

class IAApi {
  static configure (opts) {
    this.kc = opts
  }
  static sendHit (ul) {
    IAUtils.sendPost(this.kc.s + 'k', {
      u: this.kc.ul,
      sid: this.kc.sid
    })
  }
  static sendTag (tag, data) {
    IAUtils.sendPost(this.kc.s + 'c', {
      u: this.kc.ul,
      sid: this.kc.sid,
      tag: tag,
      data: data
    })
  }
  static sendTagData (tag, data) {
    this.sendTag(tag, JSON.stringify(data))
  }
}

IAApi.Events = class {
  static click (e) {
    e = window.e || e
    if (e.target.tagName !== 'A') { // filter A elements
      return
    }
    let el = e.target
    let d = new Date().toISOString()
    IAApi.sendTagData('linkClick', {
      tgt: el.href,
      tx: el.text,
      d: d
    })
  }
}

var sid = window.localStorage.getItem('sid') || IAUtils.mid()
window.localStorage.setItem('sid', sid)

var _IAd = window._IAd || {}
var IAs = _IAd.s
if (IAs.substr(-1) !== '/') IAs += '/'
IAApi.configure({
  s: IAs,
  ul: _IAd.u,
  sid: _IAd.sid || sid
})

IAApi.sendHit()

if (document.addEventListener) {
  document.addEventListener('click', IAApi.Events.click, false)
} else {
  document.attachEvent('onclick', IAApi.Events.click)
}
