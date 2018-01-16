$(function() {

    $(document).ready(function () {

        spinner();
        header();
        intro();
        backgrounds();
        carousels();
        parallax();
    });

    $(window).scroll(function() {

        header();
    });

    $(window).resize(function() {

        intro();
        header();
        carousels();
    });

    function parallax() {
        $(".jarallax").jarallax({
            speed: 0.3
        });

        $(".jarallax-keep-img").jarallax({
            keepImg: true
        });
    };
    
    function spinner() {
        $("#spinner").fadeOut("slow", function () {
            $(this).remove();
        });
    };

    function header() {
        var scrolled = $(window).scrollTop();

        if (scrolled > 150) {
            $(".header").addClass("header-prepare");
        } else {
            $(".header").removeClass("header-prepare");
        }

        if (scrolled > 1) {
            $(".header").addClass("header-fixed");
        } else {
            $(".header").removeClass("header-fixed");
        }
    };

    function intro() {
        var height = $(window).height();
        $(".intro-container").css("height", height);
    };

    function backgrounds() {
        var sections = $(".slide-bg-image, .bg-image");
        sections.each(function(i) {
            if ($(this).attr("data-background-img")) {
                $(this).css("background-image", "url(" + $(this).data("background-img") + ")");
            }
        });
    };

    function carousels() {
        // Testimonial
        $(".testimonial-carousel").owlCarousel({
            autoPlay: true,
            autoHeight: true,
            stopOnHover: true,
            singleItem: true,
            slideSpeed: 350,
            pagination: true,  // Show pagination buttons
            navigation: false,  // Hide next and prev buttons
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            //  responsive: true
        });
    };
    
});
