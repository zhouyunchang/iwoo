
/*========================================
=            Requiring stuffs            =
========================================*/
//	这里是压缩需要的模块
var gulp = require('gulp'),
    seq = require('run-sequence'),//压缩队列
    connect = require('gulp-connect'),//连接浏览器，js压缩后自动刷新浏览器，这个没用到
    less = require('gulp-less'),//压缩less
    uglify = require('gulp-uglify'),//最小化压缩js
    sourcemaps = require('gulp-sourcemaps'),//生成map.js 方便调试
    cssmin = require('gulp-cssmin'),//css最小化
    order = require('gulp-order'),//调整任务顺序
    concat = require('gulp-concat'),//合并文件
    ignore = require('gulp-ignore'),//忽略文件
    rimraf = require('gulp-rimraf'),//删除文件
    templateCache = require('gulp-angular-templatecache'),//缓存模板
    mobilizer = require('gulp-mobilizer'),//优化css 兼容性
    ngAnnotate = require('gulp-ng-annotate'),//自动注入依赖
    replace = require('gulp-replace'),//替换文件或者路径
    ngFilesort = require('gulp-angular-filesort'),//调整模块的先后顺序，保证被依赖的模块先加载
    streamqueue = require('streamqueue'),//写入文件流的队列
    rename = require('gulp-rename'),//重命名文件
    path = require('path');//路径操作



/*================================================
=            Report Errors to Console            =
================================================*/

gulp.on('error', function (e) {
    throw (e);
});


/*=========================================
=            Clean dest folder            =
=========================================*/

gulp.task('clean', function (cb) {
    return gulp.src([
        path.join('./dest', 'js')
    ], { read: false })
        .pipe(rimraf());
});


/*====================================================================
=            使用templateCache压缩模板文件                             =
====================================================================*/

gulp.task('template', function () {
    streamqueue({ objectMode: true },
        gulp.src(['./js/**/*.html']).pipe(templateCache({ module: 'demo' }))
        )
        .pipe(concat('template.js'))
        //.pipe(uglify())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest(path.join('./dest', 'js')));
});



/*====================================================================
=            Compile and minify js generating source maps            =
====================================================================*/
// - Orders ng deps automatically

gulp.task('js', function () {
    streamqueue({ objectMode: true },
        gulp.src('./js/**/*.js').pipe(ngFilesort())
        ) 
        .pipe(sourcemaps.init())
        .pipe(concat('app.js'))
        .pipe(ngAnnotate())
        .pipe(uglify())//这里是是不是压缩到最小化
        .pipe(rename({ suffix: '.min' }))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(path.join('./dest', 'js')));
});

/*===================================================================
=            Watch for source changes and rebuild/reload            =
===================================================================*/

gulp.task('watch', function () {
    gulp.watch(['./js/**/*.js'], ['js']);
    gulp.watch(['./js/**/*.html'], ['template']);
});


/*======================================
=            Build Sequence            =
======================================*/

gulp.task('build', function (done) {
    var tasks = ['js', 'template'];//只压缩js和html
    seq('clean', tasks, done);
});


/*====================================
=            Default Task            =
====================================*/

gulp.task('default', function (done) {
    var tasks = [];

    tasks.push('watch');

    seq('build', tasks, done);
});
