//* StringBuilder *//
function StringBuilder() {
    this.strings = new Array("");
}

StringBuilder.prototype.append = function (value) {
    if (value) {
        this.strings.push(value);
    }
}

StringBuilder.prototype.clear = function () {
    this.strings.length = 1;
}

StringBuilder.prototype.toString = function () {
    return this.strings.join("");
}
//* StringBuilder *//