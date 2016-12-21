class KQUtils {
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
    xhr.send(KQUtils.serialize(data))
  }
}

class KQApi {
  static configure (opts) {
    this.kc = opts
  }
  static sendHit (ul) {
    KQUtils.sendPost(this.kc.s + 'k', {
      u: this.kc.ul,
      sid: this.kc.sid
    })
  }
  static sendTag (tag, data) {
    KQUtils.sendPost(this.kc.s + 'c', {
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

KQApi.Events = class {
  static click (e) {
    e = window.e || e
    if (e.target.tagName !== 'A') { // filter A elements
      return
    }
    let el = e.target
    let d = new Date().toISOString()
    KQApi.sendTagData('linkClick', {
      tgt: el.href,
      tx: el.text,
      d: d
    })
  }
}

var sid = window.localStorage.getItem('sid') || KQUtils.mid()
window.localStorage.setItem('sid', sid)

var _kqd = window._kqd || {}
var kqs = _kqd.s
if (kqs.substr(-1) !== '/') kqs += '/'
KQApi.configure({
  s: kqs,
  ul: _kqd.u,
  sid: _kqd.sid || sid
})

KQApi.sendHit()

if (document.addEventListener) {
  document.addEventListener('click', KQApi.Events.click, false)
} else {
  document.attachEvent('onclick', KQApi.Events.click)
}
