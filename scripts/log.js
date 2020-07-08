//@ts-check
/**
 * Log a message
 * @param {string} message
 * @returns {void}
 */
module.exports.message = (message) => {
  console.log(`==> ${message}`);
};

/**
 * Log output of a process
 * @param {string} message
 */
module.exports.output = (message) => {
  console.log();
  console.log(message);
  console.log();
};
