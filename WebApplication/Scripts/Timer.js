$(document).ready(function () {
    var timer = document.getElementById("timer");

    var h = 0;
    var m = 0;
    var s = 0;

    startTimer();

    function startTimer() {
        s++;
        if (s == 60) {
            s = 0;
            m++;
            if (m == 60) {
                m = 0;
                h++;
            }
        }
        timer.textContent = (h < 10 ? "0" : "") + h + ":"
            + (m < 10 ? "0" : "") + m + ":"
            + (s < 10 ? "0" : "") + s;
        setTimeout(startTimer, 1000);
    }
})