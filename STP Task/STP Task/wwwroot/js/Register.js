const checkIfPassConfirmed = function () {
    let password = document.getElementById("password").value;
    let confirmed = document.getElementById('password-confirm').value;
    const passValidSpan = $('#password-validation');
    const passLengthSpan = $('#password-length');
    if (password === confirmed) {
        passValidSpan.attr('hidden', 'hidden');
        passwordConfirm = true;
    }
    else {
        passValidSpan.removeAttr('hidden', 'hidden');
        passwordConfirm = false;
    }
    if (password.length < 6) {
        passLengthSpan.removeAttr('hidden', 'hidden');
        passwordLength = false;
    }
    else {
        passLengthSpan.attr('hidden', 'hidden');
        passwordLength = true;
    }
    unlockRegister();
}

function checkIfFree(username) {
    $.ajax({
        url: '/home/checkifusernameavailable',
        type: "GET",
        data: { username: username },
        success: function (result) {
            const userSpan = $('#username-validation');
            if (result === 'unavailable') {
                userSpan.removeAttr('hidden');
                usernameAvailable = false;
                unlockRegister();
            }
            else {
                userSpan.attr('hidden', 'hidden');
                usernameAvailable = true;
                unlockRegister();

            }

        }
    });
}

function unlockRegister() {
    if (usernameAvailable && passwordLength && passwordConfirm) {
        document.getElementById("register-button").disabled = false;
    }
    else {
        document.getElementById("register-button").disabled = true;
    }
}

let usernameAvailable;
let passwordLength;
let passwordConfirm;
window.onload = unlockRegister;
