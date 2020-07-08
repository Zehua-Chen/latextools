//@ts-check
const { readdirSync } = require("fs");
const { dirname } = require("path");
const log = require("./log");

/**
 * Move to project root if not in root already
 * @returns {void}
 */
module.exports.moveToRoot = () => {
  const cwd = process.cwd();

  for (let entry of readdirSync(cwd)) {
    if (entry == "src") {
      return;
    }
  }

  log.message("move to root");
  process.chdir(dirname(cwd));
};
