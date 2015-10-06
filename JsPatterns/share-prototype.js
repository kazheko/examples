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
    C.prototype = P.prototype;
}

var child = new Child("Alex");
child.say("hello");