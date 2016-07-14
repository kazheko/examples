
// объект, который наследуется
var parent = {
    name: "Alex"
};

// новый объект
var child = object(parent);

console.log(child.name);function object(o) {
    function F() { }
    F.prototype = o;
    return new F();
}

//ECMA Script 5
var child2 = Object.create(parent);