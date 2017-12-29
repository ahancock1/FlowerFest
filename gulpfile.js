/// <binding BeforeBuild='build' Clean='clean' />

var gulp = require('gulp');
var sass = require('gulp-sass');
var ignore = require('gulp-ignore');
var rimraf = require('gulp-rimraf');
var concat = require('gulp-concat');

var webroot = './wwwroot/';

var inputs = {
    styles: './Styles/**/*.scss',
    scripts: './Scripts/**/*.js'
};

var outputs = {
    styles: webroot + 'css/**/*.css',
    scripts: webroot + 'js/**/*.js'
}

var clean = function (path) {
    return gulp.src(path, { read: false })
        .pipe(ignore('node_modules'))
        .pipe(rimraf());
};

var styles = function () {
    return gulp.src(inputs.styles)
        .pipe(sass())
        .pipe(gulp.dest(webroot + 'css'));
}

var scripts = function () {
    return gulp.src(inputs.scripts)
        .pipe(gulp.dest(webroot + 'js'));
}

gulp.task('clean', function () {
    clean(outputs.scripts);
    clean(outputs.styles);
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

    styles();
    scripts();
})
