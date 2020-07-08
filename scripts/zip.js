//@ts-check
const { execSync } = require("child_process");
const log = require("./log");

/**
 * Create a zip
 * @param {string} output the output
 * @param {string[]} inputs the inputs
 * @returns {void}
 */
module.exports.create = (output, inputs) => {
  let buffer = execSync(`zip -j ${output} ${inputs.join(" ")}`);
  log.output(buffer.toString());
};
