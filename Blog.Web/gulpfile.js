"use strict";

var gulp = require("gulp");
var sass = require('gulp-sass');
var lec = require('gulp-line-ending-corrector');
var rename = require('gulp-rename');
var cleanCSS = require('gulp-clean-css');
var del = require('del');
var config = require('./gulpconfig.json');
var babel = require('gulp-babel');
var sourcemaps = require('gulp-sourcemaps');
var uglify = require('gulp-uglify');

gulp.task("default", ['libraries', 'sass', 'minify-css', 'babel-transpile'], function () { });

gulp.task("clean-lib", function () {
    return del(['wwwroot/lib/']);
});

gulp.task("clean-css", function () {
    return del(['wwwroot/css/*.css']);
});

gulp.task("clean-js", function () {
    return del(['wwwroot/js/**/*.min.js', 'wwwroot/js/**/*.min.js.map' ]);
});


gulp.task("libraries", ['clean-lib'], function () {
    return gulp.src(config.libraries.src, { base: "node_modules" })
        .pipe(gulp.dest(config.libraries.dest));
});

gulp.task("sass", ['clean-css'], function () {
    return gulp.src('wwwroot/css/**/*.scss', { base: "." })
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('.'));
});

gulp.task('minify-css', ['sass'], () => {
    return gulp.src('wwwroot/css/**/*.css', { base: "." })
        .pipe(cleanCSS())
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(gulp.dest('.'));
});

gulp.task('babel-transpile', ['clean-js'], function () {
    return gulp.src([
            'wwwroot/js/**/*.js',
            'wwwroot/js/**/*.jsx'
        ])
        .pipe(sourcemaps.init())
        .pipe(babel())
        .pipe(uglify({ mangle: false }))
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('wwwroot/js'));
});

gulp.task('sass:watch', function () {
    gulp.watch('wwwroot/css/**/*.scss', ['sass']);
});