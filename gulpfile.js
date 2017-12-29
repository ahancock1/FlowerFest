/// <binding BeforeBuild='build' Clean='clean' />

var gulp = require('gulp');
var sass = require('gulp-sass');
var ignore = require('gulp-ignore');
var rimraf = require('gulp-rimraf');
var concat = require('gulp-concat');

var webroot = './wwwroot/';

var inputs = {
    hamburgers: './node_modules/hamburgers/dist/hamburgers.css',
    styles: './Styles/**/*.scss',
    scripts: './Scripts/**/*.js',
    libs: [
        './node_modules/jquery/dist/jquery.js',
        './node_modules/bootstrap/dist/js/bootstrap.js'
    ]
};

var outputs = {
    styles: webroot + 'css/**/*.css',
    scripts: webroot + 'js/**/*.js',
    css: webroot + 'css',
    js: webroot + 'js',
    lib: webroot + 'lib'
}

var clean = function (path) {
    return gulp.src(path, { read: false })
        .pipe(ignore('node_modules'))
        .pipe(rimraf());
};

var styles = function () {

    gulp.src(inputs.hamburgers)
        .pipe(gulp.dest(outputs.css));

    return gulp.src(inputs.styles)
        .pipe(sass())
        .pipe(gulp.dest(outputs.css));
}

var scripts = function () {
    return gulp.src(inputs.scripts)
        .pipe(gulp.dest(outputs.js));
}

var libs = function () {
    return gulp.src(inputs.libs)
        .pipe(gulp.dest(outputs.lib));
}

gulp.task('clean', function () {
    clean(outputs.scripts);
    clean(outputs.styles);
    clean(outputs.lib);
});

gulp.task('styles', function () {
    styles();
});

gulp.task('scripts', function () {
    scripts();
});

gulp.task('build', function () {
    clean(outputs.scripts);
    clean(outputs.styles);
    clean(outputs.lib);

    styles();
    scripts();
    libs();
})

gulp.task("libs", function () {
        libs();
});