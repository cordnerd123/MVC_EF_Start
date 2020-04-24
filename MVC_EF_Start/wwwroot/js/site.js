// Write your JavaScript code.
function nxtslide(n) {
    var x = document.getElementsByClassName("dot").length;
    var sld = document.getElementsByClassName("slide");
    var cursl = sld[0].style.backgroundImage;
    var snum = parseInt(cursl.charAt(16)) + n;
    if (n == 1)
        if (snum == x + 1) snum = 1;
        else
            if (snum == 0) snum = x;
    var url = "url(img/Picture" + snum + ".jpg)";
    sld[0].style.backgroundImage = url;
    updatedot(snum - 1);
    updatecap(snum - 1);
}

function updatecap(n) {
    var cap = document.getElementsByClassName("slidecap");
    var x = cap.length;
    for (i = 0; i < x; i++)
        cap[i].className = "slidecap";
    cap[n].className += " displaycap";
}

function updatedot(n) {
    var dot = document.getElementsByClassName("dot");
    var x = dot.length;
    for (i = 0; i < x; i++)
        dot[i].className = "dot";
    dot[n].className += " active";
}

function dotslide(n) {
    var sld = document.getElementsByClassName("slide");
    sld[0].style.backgroundImage = "url(img/Picture" + n + ".jpg)";
    updatedot(n - 1);
    updatecap(n - 1);
}
function SignConf() {
    var name = document.getElementById("fname").value;
    var email = document.getElementById("email").value;

    if (email == "") {
        alert('Please enter an email addess');
        return;
    }
    else if (name == "") {
        alert('Please enter a first name')
        return;
    }
    alert('Thanks ' + name + ' for signing up for our life-saving monthly email!')
}
function SenTableNxt(n) {
    n = n + 1;
    window.location.href = '/ImpactData/ImpactData/' + n;
}
function SenTablePrv(n) {
    if (n > 10)
        n = n - 10;
    else n = -1;
    window.location.href = '/ImpactData/ImpactData/' + n;
}