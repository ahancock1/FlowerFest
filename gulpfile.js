/// <binding Clean='clean' />

var gulp = require('gulp');
var sass = require('gulp-sass');
var ignore = require('gulp-ignore');
var rimraf = require('gulp-rimraf');
var concat = require('gulp-concat');

var webroot = './wwwroot/';

var inputs = {
    styles: './Styles/**/*.scss',
    scripts: './Scripts/**/*.js',
    libs: [
        './node_modules/jquery/dist/jquery.js',
        './node_modules/tinymce/tinymce.min.js',
        './node_modules/hamburgers/dist/hamburgers.css',
        './node_modules/font-awesome/css/font-awesome.css',
        './node_modules/font-awesome/css/font-awesome.css',
        './node_modules/bootstrap/dist/js/bootstrap.js',
        './node_modules/bootstrap/dist/css/bootstrap.css'
    ],
    fonts: [
        './node_modules/font-awesome/fonts/*.*'
    ]
};  

var outputs = {
    styles: webroot + 'css/**/*.css',
    scripts: webroot + 'js/**/*.js',
    css: webroot + 'css',
    js: webroot + 'js',
    lib: webroot + 'lib',
    libs: webroot + 'lib/**/*.*',
    fonts: webroot + 'fonts'
}

var clean = function (path) {
    return gulp.src(path, { read: false })
        .pipe(ignore('node_modules'))
        .pipe(rimraf());
};

var styles = function () {
    return gulp.src(inputs.styles)
        .pipe(concat('site.scss'))
        .pipe(sass())
        .pipe(gulp.dest(outputs.css));
}

var scripts = function () {
    return gulp.src(inputs.scripts)
        .pipe(concat('site.js'))
        .pipe(gulp.dest(outputs.js));
}

var fonts = function () {
    return gulp.src(inputs.fonts)
        .pipe(gulp.dest(outputs.fonts));
}

var libs = function () {
    return gulp.src(inputs.libs)
        .pipe(gulp.dest(outputs.lib));
}

gulp.task('clean', function () {
    clean(outputs.scripts);
    clean(outputs.styles);
    clean(outputs.libs);
});

gulp.task('styles', function () {
    styles();
});

gulp.task('scripts', function () {
    scripts();
});

gulp.task('fonts', function() {
    fonts();
})

gulp.task('build', function () {
    clean(outputs.scripts);
    clean(outputs.styles);
    clean(outputs.libs);

    styles();
    scripts();
    libs();
})

gulp.task("libs", function () {
        libs();
});