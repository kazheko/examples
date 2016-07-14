//classical patterns

// ������������ �����������
function Parent(name) {
    this.name = name || "Adam";
}
// ���������� �������������� ���������������� � ��������
Parent.prototype.say = function (str) {
    console.log(str + ', ' + this.name);
};
// ������ �������� �����������
function Child(name) {
}

// ����� ���������� ������������
inherit(Child, Parent);

function inherit(C, P) {
    C.prototype = P.prototype;
}

var child = new Child("Alex");
child.say("hello");