$('.product-cards').slick({
    dots: true,
    infinite: false,
    speed: 300,
    slidesToShow: 1,
    slidesToScroll: 1,
    responsive: [
        {
            breakpoint: 600,
            settings: {
              slidesToShow: 1,
              slidesToScroll: 1
            }
        }
    ]
});

let show=document.getElementById("show-indigarors")
let info=document.getElementById("info-detail")

let showContent=document.querySelector(".product-detail-indicators")
let infoContent=document.querySelector(".product-detail-info")

show.onclick=()=>{
    showContent.classList.remove("d-none")
    infoContent.classList.add("d-none")
    show.style.color="black"
    info.style.color="#12121235"
    show.style.borderBottom="3px solid black"
    info.style.borderBottom="1px solid #12121230"

}
info.onclick=()=>{
    infoContent.classList.remove("d-none")
    showContent.classList.add("d-none")
    info.style.color="black"
    show.style.color="#12121235"
    info.style.borderBottom="3px solid black"
    show.style.borderBottom="1px solid #12121230"

}

let leftDot1=document.getElementById("slick-slide-control01")
let leftDot2=document.getElementById("slick-slide-control02")
let leftDot3=document.getElementById("slick-slide-control03")
let leftDot4=document.getElementById("slick-slide-control04")

let rightSize1=document.querySelector(".size-1")
let rightSize2=document.querySelector(".size-2")
let rightSize3=document.querySelector(".size-3")
let rightSize4=document.querySelector(".size-4")

rightSize1.onclick=()=>{
    leftDot1.click()
    rightSize1.style.border="1px solid black"
    rightSize2.style.border="1px solid #e9e9e9"
    rightSize3.style.border="1px solid #e9e9e9"
    rightSize4.style.border="1px solid #e9e9e9"

}
rightSize2.onclick=()=>{
    leftDot2.click()
    rightSize2.style.border="1px solid black"
    rightSize1.style.border="1px solid #e9e9e9"
    rightSize3.style.border="1px solid #e9e9e9"
    rightSize4.style.border="1px solid #e9e9e9"
}
rightSize3.onclick=()=>{
    leftDot3.click()
    rightSize3.style.border="1px solid black"
    rightSize2.style.border="1px solid #e9e9e9"
    rightSize1.style.border="1px solid #e9e9e9"
    rightSize4.style.border="1px solid #e9e9e9"
}
rightSize4.onclick=()=>{
    leftDot4.click()
    rightSize4.style.border="1px solid black"
    rightSize2.style.border="1px solid #e9e9e9"
    rightSize3.style.border="1px solid #e9e9e9"
    rightSize1.style.border="1px solid #e9e9e9"
}

