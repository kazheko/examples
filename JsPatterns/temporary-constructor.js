//classical patterns

// ������������ �����������
function Parent(name) {
    this.name = name || "Adam";
}
// ���������� �������������� ���������������� � ��������
Parent.prototype.say = function (str) {
    console.log(this.name + ' say ' + str);
};
// ������ �������� �����������
function Child(name) {
}

// ����� ���������� ������������
inherit(Child, Parent);

function inherit(C, P) {
    var F = function () { };
    F.prototype = P.prototype;
    C.prototype = new F();
}

var child = new Child("Alex");
child.say("hello");


// YUI
function inherit(C, P) {
    var F = function () { };
    F.prototype = P.prototype;
    C.prototype = new F();
    C.uber = P.prototype; // ������ �� ������������� ������
    C.prototype.constructor = C; // ��������� ��������� �� �������-�����������
}