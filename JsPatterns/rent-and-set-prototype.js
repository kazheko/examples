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
    Parent.apply(this, arguments);
}

inherit(Child, Parent);

function inherit(C, P) {
    C.prototype = new P();
}

var child = new Child("Alex");child.say("hello");