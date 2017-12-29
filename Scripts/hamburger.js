(function () {

    var burger = document.getElementById("hamburger");

    if (burger)
    {
        burger.onclick = function ()
        {
            burger.classList.toggle("is-active");
        }
    }

})();