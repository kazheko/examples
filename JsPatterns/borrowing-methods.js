

var one = {
    name: "object one",
    //...
    print: function (greet) {
        console.log(greet + ", " + this.name);
    }
};

one.print("hi");var two = {
    name: "object two"
};

one.print.apply(two, ["hello"]);

function bind(o, m) {
    return function () {
        return m.apply(o, [].slice.call(arguments));
    };
}

var two_print = bind(two, one.print);
two_print("good day");

//ECMA Script 5
var newfunc = one.print.bind(two);
newfunc("ECMA Script 5");

//https://github.com/kazheko/examples/