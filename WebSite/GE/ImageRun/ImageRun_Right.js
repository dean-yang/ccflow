﻿var speed = 10; //数字越大速度越慢
var tab = document.getElementById("demo");
var tab1 = document.getElementById("demo1");
var tab2 = document.getElementById("demo2");
tab2.innerHTML = tab1.innerHTML;
function Marquee() {
    if (tab.scrollLeft <= 0)
        tab.scrollLeft += tab2.offsetWidth
    else {
        tab.scrollLeft--;
    }
}
var MyMar = setInterval(Marquee, speed);
tab.onmouseover = function () { clearInterval(MyMar) };
tab.onmouseout = function () { MyMar = setInterval(Marquee, speed) };
