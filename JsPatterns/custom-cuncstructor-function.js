
//custom cuncstructor function

var Person = function () {
    this.firstName = "Alex";
    this.say = function (str) {
        console.log(str + ', ' + this.firstName);
    }
    //return 'text';
}

//var person = new Person();
//person.say("Hello");

var Person = function (name) {
    // ��������� ������ ������ 
    // � �������������� ��������
    // var this = {};
    // !!! �� ����� ���� var this = Object.create(Person.prototype);
    // ����������� �������� � ������
    this.firstName = "Alex";
    this.say = function (str) {
        console.log(str + ', ' + this.firstName);
    };
    // return this;
};/*�������� � ��� ������, � ������ ����������� ���� ��������������� �������� prototype, ����������� �� ����� ������ ������. *//*�����, ����� ��� ���� �����������, ����� ��� 
������, ������� ��������� � ���������. */var Employee = function () {
    this.firstName = "Alex";
}

Employee.prototype = {
    say: function (str) {
        console.log(str + ', ' + this.firstName);
    }
}
//Employee.prototype.say = function()...

var employee = new Employee();
employee.say("Hi");

//Eployee();
//ECMA Script 5
//'use strict';