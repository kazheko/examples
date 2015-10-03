
//custom cuncstructor function

var Person = function () {
    this.firstName = "Alex";
    this.say = function (str) {
        console.log(this.firstName + ' say ' +  str);
    }
    //return 'text';
}

var person = new Person();
person.say("hello");

var Person = function (name) {
    // создаетс€ пустой объект 
    // с использованием литерала
    // var this = {};
    // !!! на самом деле var this = Object.create(Person.prototype);
    // добавл€ютс€ свойства и методы
    this.firstName = "Alex";
    this.say = function (str) {
        console.log(this.firstName + ' say ' + str);
    };
    // return this;
};/*ѕрототип Ц это объект, а кажда€ создаваема€ вами функци€получает свойство prototype, ссылающеес€ на новый пустой объект. *//*„лены, общие дл€ всех экземпл€ров, такие как 
методы, следует добавл€ть к прототипу. */var Employee = function () {
    this.firstName = "Alex";
}

Employee.prototype.say = function (str) {
    console.log(this.firstName + ' say ' + str);
}
//Employee.prototype = {say: function...}

var employee = new Employee();
employee.say("hello");

//Eployee();
//ECMA Script 5
//'use strict';