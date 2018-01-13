/// <binding Clean="clean" />

var gulp = require("gulp");
var sass = require("gulp-sass");
var ignore = require("gulp-ignore");
var rimraf = require("gulp-rimraf");
var concat = require("gulp-concat");

var webroot = "./wwwroot/";

var inputs = {
    styles: "./Styles/**/*.scss",
    scripts: "./Scripts/**/*.js",
    libs: [
        "./node_modules/jquery/dist/jquery.js",
        "./node_modules/jquery-validation/dist/jquery.validate.js",
        "./node_modules/tinymce/tinymce.min.js",
        "./node_modules/hamburgers/dist/hamburgers.css",
        "./node_modules/font-awesome/css/font-awesome.css",
        "./node_modules/font-awesome/css/font-awesome.css",
        "./node_modules/bootstrap/dist/js/bootstrap.js",
        "./node_modules/bootstrap/dist/css/bootstrap.css",
        "./node_modules/et-line/style.css",
        "./node_modules/wowjs/dist/wow.js",
        "./node_modules/owlcarousel-pre/owl-carousel/owl.carousel.js"
    ],
    fonts: [
        "./node_modules/font-awesome/fonts/*.*",
        "./node_modules/et-line/fonts/*.*"
    ]
};  

var outputs = {
    styles: webroot + "css",
    scripts: webroot + "js",
    libs: webroot + "lib",
    fonts: webroot + "fonts"
};

var clean = function () {
    for (var i = 0; i < arguments.length; i++) {
        gulp.src(webroot + arguments[i] + "/**/*.*", { read: false })
            .pipe(rimraf());
    }
};

var styles = function () {
    return gulp.src(inputs.styles)
        //.pipe(concat("site.scss"))
        .pipe(sass())
        .pipe(gulp.dest(outputs.styles));
};
var scripts = function () {

    return gulp.src("./Scripts/**/site.js")
        .pipe(concat("site.js"))
        .pipe(gulp.dest(outputs.scripts));

    return gulp.src(inputs.scripts)
        .pipe(concat("site.js"))
        .pipe(gulp.dest(outputs.scripts));
};
var fonts = function () {
    return gulp.src(inputs.fonts)
        .pipe(gulp.dest(outputs.fonts));
};
var libs = function () {
    return gulp.src(inputs.libs)
        .pipe(gulp.dest(outputs.libs));
};

gulp.task("clean", function () {

    clean("css", "fonts", "js", "lib");
});

gulp.task("styles", function () {
    styles();
});

gulp.task("scripts", function () {
    scripts();
});

gulp.task("fonts", function() {
    fonts();
});

gulp.task("build", function () {

    clean("css", "fonts", "js", "lib");

    styles();
    scripts();
    libs();
    fonts();
});

gulp.task("libs", function () {
    libs();
});