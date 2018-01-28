$(function() {

    $(document).ready(function () {

        spinner();
        header();
        intro();
        backgrounds();
        carousels();
        parallax();
        gravatars();
        navigation();
    });

    $(window).scroll(function() {

        header();
    });

    $(window).resize(function() {

        intro();
        header();
        carousels();
    });

    function gravatars() {
        // TODO - implement loading gravatars
         

    };

    function parallax() {

        var isMobile = /Android|iPhone|iPad|iPod|BlackBerry|Windows Phone/g.test(navigator.userAgent || navigator.vendor || window.opera);
        if (!isMobile) {

            $(".jarallax").jarallax({
                speed: 0.3
            });

            $(".jarallax-keep-img").jarallax({
                keepImg: true
            });
        }
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

    function navigation() {

        var icon = $(".nav-mobile");
        var menu = $(".nav-menu");

        icon.click(function() {

            if (!$(this).hasClass("active")) {
                icon.addClass("active");
                menu.addClass("active");
            } else {
                icon.removeClass("active");
                menu.removeClass("active");
            }

        });
    }

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
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"]
            //  responsive: true
        });

        // Support
        $(".support-carousel").owlCarousel({
            autoPlay: 2500,
            stopOnHover: true,
            items: 6,
            itemsDesktop: [1170, 5],
            itemsDesktopSmall: [1024, 4],
            itemsTabletSmall: [768, 3],
            itemsMobile: [480, 2],
            pagination: false,  // hide pagination buttons
            navigation: false,  // hide next and prev buttons
            navigationText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"]
        });
    };
    
});
