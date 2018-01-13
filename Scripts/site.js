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
        parallax();
    });

    var parallaxPositionProperty;
    if ($(window).width() >= 1024) {
        parallaxPositionProperty = "position";
    } else {
        parallaxPositionProperty = "transform";
    }

    $(window).stellar({
        responsive: true,
        positionProperty: parallaxPositionProperty,
        horizontalScrolling: false
    });

    function spinner() {
        $("#spinner").fadeOut("slow", function () {
            $(this).remove();
        });
    };

    function header() {
        var scrolled = $(window).scrollTop();
        var height = $(window).height();

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

    function parallax() {
        //Parallax Function element
        $(".parallax").each(function () {
            var $el = $(this);
            $(window).scroll(function () {
                parallax($el);
            });
            parallax($el);
        });

        function parallax($el) {
            var diff_s = $(window).scrollTop();
            var parallax_height = $(".parallax").height();
            var yPos_p = (diff_s * 0.5);
            var yPos_m = -(diff_s * 0.5);
            var diff_h = diff_s / parallax_height;

            if ($(".parallax").hasClass("parallax-section")) {
                $el.css("top", yPos_p);
            }
            if ($(".parallax").hasClass("parallax-section2")) {
                $el.css("top", yPos_m);
            }
            if ($(".parallax").hasClass("parallax-static")) {
                $el.css("top", (diff_s * 1));
            }
            if ($(".parallax").hasClass("parallax-opacity")) {
                $el.css("opacity", (1 - diff_h * 1));
            }

            if ($(".parallax").hasClass("parallax-background1")) {
                $el.css("background-position", "left" + " " + yPos_p + "px");
            }
            if ($(".parallax").hasClass("parallax-background2")) {
                $el.css("background-position", "left" + " " + -yPos_p + "px");

            }
        };
    };

});
