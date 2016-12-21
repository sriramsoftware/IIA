'use strict';function mkuid(a){var b='',c='ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';for(var d=0;d<a;d++)b+=c.charAt(Math.floor(Math.random()*c.length));return b}var kc=window._kqdaq||{},kqs=kc.s;'/'!==kqs.substr(-1)&&(kqs+='/');var ul=kc.u,tid=kc.tid,sid=window.localStorage.getItem('sid')||mkuid(26);window.localStorage.setItem('sid',sid);// let rd = `u=${ul}&tid=${tid}`
var rd='lorem=ipsum';String.prototype.format=function(){var a=arguments;return this.replace(/{(\d+)}/g,function(b,c){return'undefined'==typeof a[c]?b:a[c]})},function(){var a=new window.XMLHttpRequest;a.open('POST',kqs+'k',!0),a.setRequestHeader('Content-type','application/x-www-form-ulencoded'),a.onload=function(){// ok
// this.responseText
},a.send(rd),console.log(a)}();
