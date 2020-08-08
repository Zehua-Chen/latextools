const gulp = require("gulp");
const gulpClean = require("gulp-clean");
const gulpZip = require("gulp-zip");
const path = require("path");
const child = require("child_process");

/**
 * Returns a task that handles this rid
 * @param {string} rid
 */
function build(rid) {
  return (done) => {
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
        return;
      }

      done();
    });
  };
}

/**
 * Get the publish path for this rid
 * @param {string} rid
 */
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

/**
 * Returns a task that removes debug symbols for build of this rid
 * @param {string} rid
 */
function removeDebug(rid) {
  return () => {
    return gulp.src(path.join(getPublishPath(rid), "*.pdb")).pipe(gulpClean());
  };
}

/**
 * Returns a task that create zip file for build of this rid
 * @param {string} rid
 */
function zip(rid) {
  return () => {
    return gulp
      .src(path.join(getPublishPath(rid), "*"))
      .pipe(gulpZip(`latextools-${rid}.zip`))
      .pipe(gulp.dest("publish"));
  };
}

/**
 * Returns a task that package for this rid
 * @param {string} rid
 */
function package(rid) {
  return gulp.series(removeDebug(rid), zip(rid));
}

exports.build_osx_x64 = build("osx-x64");
exports.package_osx_x64 = package("osx-x64");
exports.publish = gulp.series(
  gulp.parallel(exports.build_osx_x64),
  gulp.parallel(exports.package_osx_x64)
);
