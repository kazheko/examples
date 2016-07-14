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

var child = new Child("Alex");
child.say("hello");