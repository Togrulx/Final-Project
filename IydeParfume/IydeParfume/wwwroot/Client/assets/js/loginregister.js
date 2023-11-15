
let login=document.getElementById("log")
let register=document.getElementById("reg")
let loginShow=document.querySelector(".log")
let registerShow=document.querySelector(".reg")


$('.myaccount-tab-menu a').filter(function () {
    return this.href === location.href;
}).addClass('active');