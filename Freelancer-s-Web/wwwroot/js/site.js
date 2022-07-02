// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("GetMessagesResponse", function (res) {
    //console.log("GetMessagesResponse");
    //console.log(res);
    document.getElementById("messagesList").innerHTML = ""
    $.each(res, (k, v) => {
        var li = document.createElement("li");
        li.textContent = " [ " + v.createdAt + "] " + v.content;
        document.getElementById("messagesList").appendChild(li);
    })

});

connection.start().then(() => {
    //console.log("Connected")
    var userId = document.getElementById("userInput").value;
    connection.invoke("GetMessages", userId).catch(function (err) {
        return console.error(err.toString());
    });
});
