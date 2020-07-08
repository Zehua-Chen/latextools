//@ts-check
const { join } = require("path");
const { release } = require("process");

module.exports.sdk = "netcoreapp3.1";
module.exports.rids = ["osx-x64"];

/**
 * Get the path to the project
 * @param {string} project the project
 * @param {string} cwd the current working directory
 * @returns {string}
 */
module.exports.getProjectPath = (project, cwd = process.cwd()) => {
  return join(cwd, "src", project);
};
