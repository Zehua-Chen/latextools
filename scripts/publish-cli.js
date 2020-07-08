//@ts-check

/**
 * @file Create CLI publish
 */

const path = require("path");
const cp = require("child_process");
const fs = require("fs");
const log = require("./log");
const constants = require("./constants");
const zip = require("./zip");
const { moveToRoot } = require("./utils");
const { join } = require("path");

/**
 * Get a zip
 * @param {string} rid the runtime identiifer
 * @returns {string}
 */
function getZip(rid) {
  return `latextools-${rid}.zip`;
}

moveToRoot();

const latextoolsPath = constants.getProjectPath("latextools");

// Stage 0: move to the right directory
process.chdir(latextoolsPath);

// Stage 1: remove existing builds
for (let rid of constants.rids) {
  let package = getZip(rid);

  if (fs.existsSync(package)) {
    log.message(`remove previous build ${package}`);
    fs.unlinkSync(package);
  }
}

// Stage 2: build
for (let rid of constants.rids) {
  log.message(`build rid ${rid}`);
  let buffer = cp.execSync(
    `dotnet publish -c Release -r ${rid} --self-contained=true -p:PublishTrimmed=true`
  );

  log.output(buffer.toString());

  log.message(`removing debug symbols`);

  let buildPath = path.join("bin", "Release", constants.sdk, rid, "publish");

  for (let item of fs.readdirSync(buildPath)) {
    if (path.extname(item) == ".pdb") {
      log.message(`remove debug symbol ${item}`);
      fs.unlinkSync(path.join(buildPath, item));
    }
  }

  let package = getZip(rid);
  let contents = fs
    .readdirSync(buildPath)
    .map((content) => join(buildPath, content));

  log.message(`packaging ${package}`);
  zip.create(package, contents);

  log.message(`done building for ${rid}`);
}
