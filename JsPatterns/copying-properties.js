
//������������� �����������
function extend(parent, child) {
    var prop;
    child = child || {};
    for (prop in parent) {
        if (parent.hasOwnProperty(prop)) {
            child[prop] = parent[prop];
        }
    }
    return child;
}
var kid = extend(dad);
    var i,
        toStr = Object.prototype.toString,
        astr = "[object Array]";

    child = child || {};

    for (i in parent) {
        if (parent.hasOwnProperty(i)) {
            if (typeof parent[i] === "object") {
                child[i] = (toStr.call(parent[i]) === astr) ? [] : {};
                extendDeep(parent[i], child[i]);
            } else {
                child[i] = parent[i];
            }
        }
    }
    return child;
}