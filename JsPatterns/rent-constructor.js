//classical patterns

// родительский конструктор
function Parent(name) {
    this.name = name || "Adam";
}
// добавление дополнительной функциональности в прототип
Parent.prototype.say = function (str) {
    console.log(str + ', ' + this.name);
};
// пустой дочерний конструктор
function Child(name) {
    Parent.apply(this, arguments);
}

var child = new Child("Alex");
child.say("hello");