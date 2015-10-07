
// types

console.log(typeof 1);

console.log(typeof 'text');

console.log(typeof true);

console.log(typeof null);

console.log(typeof undefined);

console.log(typeof new Object());

// params

var globalParam1 = 1;
function fun() {
    var localParam2 = 2;
    globalParam2 = 3;

    function f1() {
        function f2() {
            var p1;
            var p2 = localParam2;
        }
    }
}

/*
в Java Script, определ€€ переменную, 
вы уже имеете дело с объектом. ¬о-первых, переменна€ автоматически 
становитс€  свойством  внутреннего  объекта,  так  называемого  объек-
та активации (Activation Object) (или свойством глобального объекта, 
если определ€етс€ глобальна€ переменна€)
*/  

//object

var obj = {
    p: 1,
    o: {},
    f: function () {
    }
}

/*
ќбъект Ц это всего лишь коллекци€  именованных  свойств,
список  пар  ключ-значение  (во  многом 
идентичный ассоциативным массивам в других €зыках программировани€)
*/
