(function() {

    $(document).ready(function() {

        initIntro();


    });

    $(window).load(function() {

        // Remove the spinner from the page
        $("#spinner").fadeOut("slow",
            function() {
                $(this).remove();
            });

    });

    function initIntro() {

        // Set the intro height
        var height = $(window).height();
        $(".intro-container").css("height", height);

    };

});