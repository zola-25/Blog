"use strict";

var gulp = require("gulp");
var sass = require('gulp-sass');
var del = require('del');
var config = require('./gulpconfig.json');
var cleanCSS = require('gulp-clean-css');
var rename = require('gulp-rename');
var uglify = require('gulp-uglify');


gulp.task("clean-lib", function () {
    return del(['wwwroot/lib/**'], { force: true});
});

gulp.task("clean-css", function () {
    return del(['wwwroot/css/*.css']);
});

gulp.task("clean-js", function () {
    return del(['wwwroot/js/*.min.js']);
});

gulp.task("libraries", gulp.series('clean-lib', function () {
    return gulp.src(config.libraries.src, { base: "node_modules" })
        .pipe(gulp.dest(config.libraries.dest));
}));

gulp.task("sass", gulp.series('clean-css', function () {
    return gulp.src('wwwroot/css/**/*.scss', { base: "." })
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('.'));
}));

gulp.task('compile-minify-css', gulp.series('sass', function() {
    return gulp.src('wwwroot/css/**/*.css', { base: "." })
        .pipe(cleanCSS())
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(gulp.dest('.'));
}));

gulp.task('minify-js', gulp.series('clean-js', function() {
    return gulp.src('wwwroot/js/**/*.js', { base: "." })
        .pipe(uglify())
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(gulp.dest('.'));
}));

gulp.task('sass:watch', function () {
    gulp.watch('wwwroot/css/**/*.scss', ['sass']);
});

gulp.task("default", gulp.parallel('libraries','minify-js', 'compile-minify-css'));
