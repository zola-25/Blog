"use strict";

var gulp = require("gulp");
var sass = require('gulp-sass');
var del = require('del');
var config = require('./gulpconfig.json');


gulp.task("clean-lib", function () {
    return del(['wwwroot/lib/']);
});

gulp.task("clean-css", function () {
    return del(['wwwroot/css/*.css']);
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

gulp.task('sass:watch', function () {
    gulp.watch('wwwroot/css/**/*.scss', ['sass']);
});

gulp.task("default", gulp.parallel('libraries', 'sass'));