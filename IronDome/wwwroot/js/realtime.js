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
    const h1 = document.createElement("h1")
    h1.style.color = "red"
    if (window.location.href.includes("Attack")) {
        h1.textContent = "Gift has been sent to our Zionist friend in Israel, it'll arraive in " + rt + " seconds"
    } else {
        h1.textContent = name + " has sent you a present! it'll arraive in " + rt + " seconds"
    };
    document.body.appendChild(h1)
    setTimeout(() => {
        document.body.removeChild(h1)
    }, 2500)
    const tr = document.createElement("tr")
    const td1 = document.createElement("td")
    const td2 = document.createElement("td")
    const td3 = document.createElement("td")
    td1.innerHTML = id
    td2.innerHTML = rt
    td3.innerHTML = name
    tr.appendChild(td1)
    tr.appendChild(td2)
    tr.appendChild(td3)
    document.querySelector("#def-bod-tbl").appendChild(tr)
    console.log(document.querySelector("#def-bod-tbl"))
})

// listen to intercept
