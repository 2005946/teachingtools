function hideLogin() {
    document.getElementById("container-input-login").style.display = "none";
    document.getElementById("login-text").setAttribute('style', 'color: black !important;');
    document.getElementById("container-login").style.backgroundColor = "white"; 
    showRegister();
}

function showRegister(){
    document.getElementById("container-input-register").style.display = "block";
    document.getElementById("register-text").setAttribute('style', 'color: white !important;');
    document.getElementById("container-register").style.backgroundColor = "transparent";
}

function hideRegister() {
    document.getElementById("container-input-register").style.display = "none";
    document.getElementById("register-text").setAttribute('style', 'color: black !important;');
    document.getElementById("container-register").style.backgroundColor = "white";
    showLogin();
}

function showLogin(){
    document.getElementById("container-input-login").style.display = "block";
    document.getElementById("login-text").setAttribute('style', 'color: white !important;');
    document.getElementById("container-login").style.backgroundColor = "transparent";
}


