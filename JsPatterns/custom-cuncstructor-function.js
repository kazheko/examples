
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
    // ��������� ������ ������ 
    // � �������������� ��������
    // var this = {};
    // !!! �� ����� ���� var this = Object.create(Person.prototype);
    // ����������� �������� � ������
    this.firstName = "Alex";
    this.say = function (str) {
        console.log(this.firstName + ' say ' + str);
    };
    // return this;
};/*�������� � ��� ������, � ������ ����������� ���� ��������������� �������� prototype, ����������� �� ����� ������ ������. *//*�����, ����� ��� ���� �����������, ����� ��� 
������, ������� ��������� � ���������. */var Employee = function () {
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