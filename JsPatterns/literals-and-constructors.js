
//literals

var obj1 = {
    p: 1,
    o: {},
    f: function () {

    }
}

var obj2 = {};

obj2.p = 1;
obj2.o = {};
obj2.f = function () {
}

obj2["p-p"] = 1;

//constructors

var obj3 = new Object(); // var obj3 = {};
var str3 = new String(); // var str3 = "";

var dateTime3 = new Date(); // ???

// антишаблоны

// пустой объект

console.log("====object====");

var o = new Object();
console.log(o.constructor === Object);   // true
console.log(typeof o);

// объект≠-число

console.log("====number object====");

var o1 = new Object(1);
console.log(o1.constructor === Number);   // true
console.log(o1.toFixed(2));               // У1.00Ф
console.log(typeof o1);

// объект≠-строка

console.log("====string object====");

var o2 = new Object("text");
console.log(o2.constructor === String);   // true
// обычные объекты не имеют метода substring()
// зато этот метод имеетс€ у объектов-≠строк
console.log(typeof o2.substring);         // УfunctionФconsole.log(typeof o2);

// объект≠-логическое значение

console.log("====bool object====");

var o3 = new Object(true);
console.log(o3.constructor === Boolean);  // trueconsole.log(typeof o3);function fun(parm) {
    var v = new Object(parm);
    //...
}

var n = new Number(100);
var s = new String("string");
var b = new Boolean(true);

var array1 = [1, 2, 3];
var array2 = new Array(1, 2, 3);//[1,2,3]
var array3 = new Array(3);//???