const gulp = require("gulp");
const gulpClean = require("gulp-clean");
const gulpZip = require("gulp-zip");
const path = require("path");
const child = require("child_process");

function build(rid) {
  return (done) => {
    console.log("build");
    let project = path.join("src", "latextools", "latextools.csproj");

    let stream = child.spawn("dotnet", [
      "publish",
      "-c",
      "Release",
      "-r",
      rid,
      "--self-contained=true",
      "-p:PublishTrimmed=true",
      project,
    ]);

    stream.stdout.on("data", (chunk) => {
      console.log(chunk.toString());
    });

    stream.stderr.on("data", (chunk) => {
      console.log(chunk.toString());
    });

    stream.on("close", (code) => {
      if (code) {
        done(`process exit with code ${code}`);
      }
    });

    done();
  };
}

function getPublishPath(rid) {
  return path.join(
    "src",
    "latextools",
    "bin",
    "Release",
    "netcoreapp3.1",
    rid,
    "publish"
  );
}

function removeDebug(rid) {
  return () => {
    return gulp.src(path.join(getPublishPath(rid), "*.pdb")).pipe(gulpClean());
  };
}

function zip(rid) {
  return () => {
    return gulp
      .src(path.join(getPublishPath(rid), "*"))
      .pipe(gulpZip(`latextools-${rid}.zip`))
      .pipe(gulp.dest("publish"));
  };
}

function package(rid) {
  return gulp.series(removeDebug(rid), zip(rid));
}

exports.build_osx_x64 = build("osx-x64");
exports.package_osx_x64 = package("osx-x64");
exports.publish = gulp.series(
  gulp.parallel(exports.build_osx_x64),
  gulp.parallel(exports.package_osx_x64)
);
