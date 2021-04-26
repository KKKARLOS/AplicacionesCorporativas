
String.prototype.capitalize = function (all) {

    if (all) {
        return this.split(' ').map(function (e) { return e.capitalize(); }).join(' ');
    } else {
        val = this.toLowerCase();
        return val.charAt(0).toUpperCase() + val.slice(1);
    }
}


String.prototype.normalize = function () {

    var from = "ÃÀÁÄÂÈÉËÊÌÍÏÎÒÓÖÔÙÚÜÛãàáäâèéëêìíïîòóöôùúüûÑñÇç",
    to = "AAAAAEEEEIIIIOOOOUUUUaaaaaeeeeiiiioooouuuunncc",
    mapping = {};

    for (var i = 0, j = from.length; i < j; i++)
        mapping[from.charAt(i)] = to.charAt(i);

    var ret = [];
    for (var i = 0, j = this.length; i < j; i++) {
        var c = this.charAt(i);
        if (mapping.hasOwnProperty(this.charAt(i)))
            ret.push(mapping[c]);
        else
            ret.push(c);
    }
    return ret.join('');
}


String.prototype.toDate = function (splitter) {

    if (!this || this == "") return null;
    if (typeof splitter === "undefined") splitter = "/";
    var aDate = this.split(splitter);
    return new Date(aDate[2], (aDate[1] - 1), aDate[0]);

}

String.prototype.trunc = function (n) {
    return (this.length > n) ? this.substr(0, n - 1) + '&hellip;' : this;
}

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