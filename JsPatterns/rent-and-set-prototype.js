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
    Parent.apply(this, arguments);
}
Child.prototype = new Parent();

var child = new Child("Alex");child.say("hello");