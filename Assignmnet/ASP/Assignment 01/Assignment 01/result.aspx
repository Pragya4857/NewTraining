<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="Assignment_01.Result" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Result</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h1 {
            text-align: center;
            color: #333;
        }

        #details {
            margin-top: 20px;
        }

        p {
            margin: 10px 0;
        }

        span {
            font-weight: bold;
            color: #555;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>Validation Successful</h1>
        <div id="details">
            <p>Name: <span id="nameSpan" runat="server"></span></p>
            <p>Family Name: <span id="familyNameSpan" runat="server"></span></p>
            <p>Address: <span id="addressSpan" runat="server"></span></p>
            <p>City: <span id="citySpan" runat="server"></span></p>
            <p>Zip Code: <span id="zipCodeSpan" runat="server"></span></p>
            <p>Phone: <span id="phoneSpan" runat="server"></span></p>
            <p>Email: <span id="emailSpan" runat="server"></span></p>
        </div>
    </div>
</body>
</html>
