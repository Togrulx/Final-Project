

$('.Blogs').slick({
    dots: false,
    infinite: true,
    speed: 700,
    slidesToShow: 3,
    slidesToScroll: 3,
    responsive: [
      
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      },
   
      // You can unslick at a given breakpoint now by adding:
      // settings: "unslick"
      // instead of a settings object
    ]
  });

  window.onscroll = function() {scrollFunction()};
  function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
      document.getElementById("navbar").style.padding = "0";
      document.getElementById("navbar").style.height = "4rem";
      document.querySelector(".logo1").style.width = "45%";
      document.querySelector(".logo3").style.width = "45%";
      document.querySelector(".logo4").style.width = "45%";
      document.querySelector(".form").style.width = "86%";
      document.querySelector(".form").style.padding = "6px";
      document.querySelector(".span1").style.display="none"
      document.querySelector(".span2").style.display="none"
      document.querySelector(".phNumber").style.fontSize ="1rem"
      document.querySelector(".bskt").style.marginLeft ="0"
    } else {
      document.getElementById("navbar").style.padding = "1.5rem 0 1.2rem";
      document.getElementById("navbar").style.height = "9rem";
      document.querySelector(".logo1").style.width = "100%";
      document.querySelector(".logo3").style.width = "100%";
      document.querySelector(".logo4").style.width = "100%";
      document.querySelector(".form").style.width = "93%";
      document.querySelector(".form").style.padding = "11px";
      document.querySelector(".span1").style.display="block"
      document.querySelector(".span2").style.display="block"
      document.querySelector(".phNumber").style.fontSize ="1.5rem"
      document.querySelector(".bskt").style.marginLeft ="3.2rem"


    }
  }

  