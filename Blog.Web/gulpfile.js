"use strict";

var gulp = require("gulp");
var sass = require('gulp-sass');
var rename = require('gulp-rename');
var cleanCSS = require('gulp-clean-css');
var del = require('del');
var config = require('./gulpconfig.json');

const webpackStream = require('webpack-stream')
const webpackDevConfig = require('./webpack.development.config.js');
const webpackProdConfig = require('./webpack.production.config.js');

gulp.task("default", ['libraries', 'sass', 'minify-css', 'webpack-dev', 'webpack-prod'], function () { });

gulp.task("clean-lib", function () {
    return del(['wwwroot/lib/']);
});

gulp.task("clean-css", function () {
    return del(['wwwroot/dist/*.css']);
});

gulp.task("clean-js", function () {
    return del(['wwwroot/dist/*.js', 'wwwroot/dist/*.map']);
});


gulp.task("libraries", ['clean-lib'], function () {
    return gulp.src(config.libraries.src, { base: "node_modules" })
        .pipe(gulp.dest(config.libraries.dest));
});

gulp.task("sass", ['clean-css'], function () {
    return gulp.src('wwwroot/css/**/*.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('./wwwroot/dist'));
});

gulp.task('minify-css', ['sass'], () => {
    return gulp.src('wwwroot/dist/**/*.css', { base: "." })
        .pipe(cleanCSS())
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(gulp.dest('.'));
});

gulp.task('webpack-dev', ['clean-js'] ,() => {
    return webpackStream(webpackDevConfig)
        .pipe(gulp.dest('./wwwroot/dist'));
});

gulp.task('webpack-prod', ['clean-js'] ,() => {
    return webpackStream(webpackProdConfig)
        .pipe(gulp.dest('./wwwroot/dist'));
});

gulp.task('sass:watch', function () {
    gulp.watch('wwwroot/css/**/*.scss', ['sass']);
});