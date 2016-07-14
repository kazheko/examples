/*
Цель реализации классического наследования в том, чтобы объекты, 
создаваемые одной функцией-конструктором Child(), приобретали свой-
ства, присущие другому конструктору Parent().
*/

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
function Child(name) { }

// здесь происходит наследования
inherit(Child, Parent);