
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

var obj3 = new Object();//var obj3 = {};
var str3 = new String();//var str3 = '';

var dateTime3 = new Date();//???

// �����������

// ������ ������
var o = new Object();
console.log(o.constructor === Object);   // true
console.log(typeof o);

// ������-�����
var o1 = new Object(1);
console.log(o1.constructor === Number);   // true
console.log(o1.toFixed(2));               // �1.00�
console.log(typeof o1);

// ������-������
var o2 = new Object("string");
console.log(o2.constructor === String);   // true
// ������� ������� �� ����� ������ substring()
// ���� ���� ����� ������� � ��������-������
console.log(typeof o2.substring);         // �function�console.log(typeof o2);

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



