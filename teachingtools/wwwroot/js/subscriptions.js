function tickMonthly(){
    document.getElementById("monthly").checked = true;
    document.getElementById("yearly").checked = false;
    document.getElementById("price-month").style = "background-color: rgba(0,0,0,0.5)";
    document.getElementById("price-year").style = "background-color: rgba(0,0,0,0)";
    document.getElementById("checkout-button").disabled = false;
}

function tickYearly() {
    document.getElementById("yearly").checked = true;
    document.getElementById("monthly").checked = false;
    document.getElementById("price-month").style = "background-color: rgba(0,0,0,0)";
    document.getElementById("price-year").style = "background-color: rgba(0,0,0,0.5)";
    document.getElementById("checkout-button").disabled = false;
}