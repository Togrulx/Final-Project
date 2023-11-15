$(document).ready(function(){
    $("#myInput").on("keyup", function() {
      var value = $(this).val().toLowerCase();
      $("#myList li").filter(function() {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
      });
    });
});



$(document).on("click", '.select-catagory', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    let category = e.target.previousElementSibling
    let CategoryId = category.value;

    $.ajax(
        {
            type: "GET",
            url: aHref,

            data: {
                CategoryId: CategoryId
            },

            success: function (response) {
                $('.filtered-area').html(response);

            },
            error: function (err) {
                $(".modalProduct").html(err.responseText);
            }

        });
})


$(document).on("click", '.select-season', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    let season = e.target.previousElementSibling
    let SeasonId = season.value;

    $.ajax(
        {
            type: "GET",
            url: aHref,

            data: {
                SeasonId: SeasonId
            },

            success: function (response) {
                $('.filtered-area').html(response);

            },
            error: function (err) {
                $(".modalProduct").html(err.responseText);
            }

        });
})

$(document).on("click", '.select-Brand', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    let brand = e.target.previousElementSibling
    let BrandId = brand.value;

    $.ajax(
        {
            type: "GET",
            url: aHref,

            data: {
                BrandId: BrandId
            },

            success: function (response) {
                $('.filtered-area').html(response);

            },
            error: function (err) {
                $(".modalProduct").html(err.responseText);
            }

        });

})

$(document).on("click", '.select-group', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    let group = e.target.previousElementSibling
    let GroupId = group.value;

    $.ajax(
        {
            type: "GET",
            url: aHref,

            data: {
                GroupId: GroupId
            },

            success: function (response) {
                $('.filtered-area').html(response);

            },
            error: function (err) {
                $(".modalProduct").html(err.responseText);
            }

        });
})

$(document).on("click", '.select-usageTime', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    let usagetime = e.target.previousElementSibling
    let UsageTimeId = usagetime.value;

    $.ajax(
        {
            type: "GET",
            url: aHref,

            data: {
                UsageTimeId: UsageTimeId
            },

            success: function (response) {
                $('.filtered-area').html(response);

            },
            error: function (err) {
                $(".modalProduct").html(err.responseText);
            }

        });
})

$(document).on("click", ".sort", function (e) {
    e.preventDefault();


    let aHref = e.target.href;

    $.ajax(
        {
            type: "GET",
            url: aHref,
            success: function (response) {
                $('.filtered-area').html(response);
            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})



//$(document).on("click", '.select-ca"', function (e) {

//    e.preventDefault();

//    let price = e.target.href
//    console.log(e)
  

//    //let maxPrice = e.target.parentElement.children[1].value;
//    //let MaxPrice = parseInt(maxPrice);
//    //console.log(MaxPrice)




//    //let aHref = document.querySelector(".searchproductPrice").href;
//    //console.log(aHref);


//    //$.ajax(
//    //    {
//    //        type: "GET",
//    //        url: aHref,

//    //        data: {
//    //            MinPrice: MinPrice,
//    //            MaxPrice: MaxPrice
//    //        },

//    //        success: function (response) {
//    //            $('.filtered-area').html(response);

//    //        },
//    //        error: function (err) {
//    //            $(".modalProduct").html(err.responseText);
//    //        }

//    //    });
//});