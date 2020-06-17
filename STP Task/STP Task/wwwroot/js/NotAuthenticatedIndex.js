const Login = function () {
    $('#home-main').load('/home/viewlogin');
}

const Register = function () {
    $('#home-main').load('/home/viewregister');
}

window.onload = Login;