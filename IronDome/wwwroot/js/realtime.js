﻿const connection = new signalR.HubConnectionBuilder().withUrl("/rt").build()

connection.start()
    .then(function () {
        // do somthing once connected
    })
    .catch(function (err) {
        return console.error(err.toString());
    });


// invoke launch
function invokeLaunch(id, rt, name) {
    connection.invoke("AttackAlert", id, rt, name)
    console.log("I am inisde the launch invoke func")
}

// invoke inretcept

// listen to lauch
connection.on("RedAlert", function (id, rt, name) {
    if (!window.location.href.includes("Deffence")) retrun;
    const h1 = document.createElement("h1")
    h1.style.color = "red"
    h1.textContent = name + " has sent you a present! it'll arraive in " + rt + " seconds"
    document.body.appendChild(h1)
    setTimeout(() => {
        document.body.removeChild(h1)
    }, 1500)

})

// listen to intercept
