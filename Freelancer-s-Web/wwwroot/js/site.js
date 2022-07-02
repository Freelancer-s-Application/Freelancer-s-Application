// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict"

const { signalR } = require("../lib/microsoft/signalr/dist/browser/signalr")

//$(() => {
//    LoadProdData();
//    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
//    connection.start();

//    connection.on("LoadMessage", function () {
//        LoadMessageData();
//    })
//})

//LoadMessageData();

//function LoadMessageData() {
//    var tr = '';
//    $.ajax({
//        url: 'Message/GetProducts',
//        method: 'GET',
//        success: (result) => {
//            $.each(result, (k, v) => {
//                tr += `<tr>
//                    <td> ${v.ProName} </td>
//                    <td> ${v.Category} </td>
//                    <td> ${v.StockQty} </td>
//                    <td>
//                        <a href='../Products/Edit?id=${v.ProdId}'>Edit</a> |
//                        <a href='../Products/Details?id=${v.ProdId}'>Details</a> |
//                        <a href='../Products/Delete?id=${v.ProdId}'>Delete</a> |
//                    </td>
//                    <tr>`
//            })

//            $("#tableBody").html(tr);
//        },
//        error: (error) => {
//            console.log(error);
//        }
//    })
//}