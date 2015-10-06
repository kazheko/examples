//classical patterns

// родительский конструктор
function Parent(name) {
    this.name = name || "Adam";
}
// добавление дополнительной функциональности в прототип
Parent.prototype.say = function (str) {
    console.log(this.name + ' say ' + str);
};
// пустой дочерний конструктор
function Child(name) {
}

// здесь происходит наследования
inherit(Child, Parent);

function inherit(C, P) {
    var F = function () { };
    F.prototype = P.prototype;
    C.prototype = new F();
}

var child = new Child("Alex");
child.say("hello");


// YUI
function inherit(C, P) {
    var F = function () { };
    F.prototype = P.prototype;
    C.prototype = new F();
    C.uber = P.prototype; // ссылка на оригинального предка
    C.prototype.constructor = C; // установка указателя на функцию-конструктор
}