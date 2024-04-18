<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validator.aspx.cs" Inherits="Assignment_01.Validator" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form Validation</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
        }

        .container {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h3 {
            margin-top: 0;
            margin-bottom: 20px;
            color: #333;
        }

        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            color: #555;
        }

        input[type="text"],
        input[type="email"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 3px;
            box-sizing: border-box;
        }

        .error-message {
            color: red;
            margin-top: 5px;
            margin-bottom: 15px;
        }

        .submit-button {
            background-color: #4CAF50;
            color: white;
            padding: 12px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
            margin-top: 10px;
        }

        .submit-button:hover {
            background-color: #45a049;
        }

        .success-message {
            color: #4CAF50;
            font-weight: bold;
            margin-top: 10px;
        }

        ul {
            list-style-type: none;
            padding: 0;
        }

        ul li {
            margin-bottom: 5px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="container">
            <h3>Insert Your Details:</h3>
            <label for="nameInput">Name:</label>
            <asp:TextBox ID="nameInput" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="nameValidator" runat="server" ErrorMessage="Name is Required" ControlToValidate="nameInput" ForeColor="Red" Display="Dynamic" ValidationGroup="regngrp"></asp:RequiredFieldValidator>
            <div class="error-message" id="nameError"></div>

            <label for="familyNameInput">Family Name:</label>
            <asp:TextBox ID="familyNameInput" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="familyNameValidator" runat="server" ErrorMessage="Enter Family Name" ControlToValidate="familyNameInput" ForeColor="Red" Display="Dynamic" ValidationGroup="regngrp"></asp:RequiredFieldValidator>
            <div class="error-message" id="familyNameError"></div>

            <label for="addressInput">Address:</label>
            <asp:TextBox ID="addressInput" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="addressValidator" runat="server" ErrorMessage="Enter Address" ControlToValidate="addressInput" ForeColor="Red" Display="Dynamic" ValidationGroup="regngrp"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddress" runat="server" ErrorMessage="Address should have at least 2 characters." ControlToValidate="addressInput" ValidationExpression="^.{2,}$" ForeColor="Red" ValidationGroup="regngrp"></asp:RegularExpressionValidator>
            <div class="error-message" id="addressError"></div>

            <label for="cityInput">City:</label>
            <asp:TextBox ID="cityInput" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="cityValidator" runat="server" ErrorMessage="Enter City" ControlToValidate="cityInput" ForeColor="Red" Display="Dynamic" ValidationGroup="regngrp"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorCity" runat="server" ErrorMessage="City should have at least 2 characters." ControlToValidate="cityInput" ValidationExpression="^.{2,}$" ForeColor="Red" ValidationGroup="regngrp"></asp:RegularExpressionValidator>
            <div class="error-message" id="cityError"></div>

            <label for="zipCodeInput">Zip Code:</label>
            <asp:TextBox ID="zipCodeInput" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="zipCodeValidator" runat="server" ErrorMessage="Zip Code is required" ControlToValidate="zipCodeInput" ForeColor="Red" Display="Dynamic" ValidationGroup="regngrp"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorZipCode" runat="server" ErrorMessage="Zip-code should be 5 digits." ControlToValidate="zipCodeInput" ValidationExpression="^\d{5}$" ForeColor="Red" ValidationGroup="regngrp"></asp:RegularExpressionValidator>
            <div class="error-message" id="zipCodeError"></div>

            <label for="phoneInput">Phone:</label>
            <asp:TextBox ID="phoneInput" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Zip Code is required" ControlToValidate="zipCodeInput" ForeColor="Red" Display="Dynamic" ValidationGroup="regngrp"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhone" runat="server" ErrorMessage="Phone should be in the format XX-XXXXXXXX or XXX-XXXXXXX." ControlToValidate="phoneInput" ValidationExpression="^\d{2,3}-?\d{7}$" ForeColor="Red" ValidationGroup="regngrp"></asp:RegularExpressionValidator>
            <div class="error-message" id="phoneError"></div>

            <label for="emailInput">E-Mail:</label>
            <asp:TextBox ID="emailInput" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="phoneValidator" runat="server" ErrorMessage="Phone is required" ControlToValidate="phoneInput" ForeColor="Red" Display="Dynamic" ValidationGroup="regngrp"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ErrorMessage="Invalid Email Format." ControlToValidate="emailInput" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ForeColor="Red" ValidationGroup="regngrp"></asp:RegularExpressionValidator>
            <div class="error-message" id="emailError"></div>

            <asp:ValidationSummary ID="validationSummary" runat="server" ForeColor="Red" ValidationGroup="regngrp" />

            <asp:Label ID="successLabel" runat="server" ForeColor="Green" Visible="false"></asp:Label>
            <asp:Button ID="btnSubmit" runat="server" Text="Check" CssClass="submit-button" OnClick="btnSubmit_Click" ValidationGroup="regngrp" />

            <ul id="errorList"></ul>
        </div>
    </form>

    <script type="text/javascript">
        function validateForm() {
            var name = document.getElementById("<%= nameInput.ClientID %>").value;
            var familyName = document.getElementById("<%= familyNameInput.ClientID %>").value;
            var address = document.getElementById("<%= addressInput.ClientID %>").value;
            var city = document.getElementById("<%= cityInput.ClientID %>").value;
            var zipCode = document.getElementById("<%= zipCodeInput.ClientID %>").value;
            var phone = document.getElementById("<%= phoneInput.ClientID %>").value;
            var email = document.getElementById("<%= emailInput.ClientID %>").value;

            var errors = [];

            if (name.trim() === '') {
                errors.push("Name is required.");
                document.getElementById("nameError").innerText = "Name is required.";
            } else {
                document.getElementById("nameError").innerText = "";
            }

            if (familyName.trim() === '') {
                errors.push("Family name is required.");
                document.getElementById("familyNameError").innerText = "Family name is required.";
            } else {
                document.getElementById("familyNameError").innerText = "";
            }

            if (address.trim().length < 2) {
                errors.push("Address should have at least 2 characters.");
                document.getElementById("addressError").innerText = "Address should have at least 2 characters.";
            } else {
                document.getElementById("addressError").innerText = "";
            }

            if (city.trim().length < 2) {
                errors.push("City should have at least 2 characters.");
                document.getElementById("cityError").innerText = "City should have at least 2 characters.";
            } else {
                document.getElementById("cityError").innerText = "";
            }

            if (!/^\d{5}$/.test(zipCode)) {
                errors.push("Zip code should be 5 digits.");
                document.getElementById("zipCodeError").innerText = "Zip code should be 5 digits.";
            } else {
                document.getElementById("zipCodeError").innerText = "";
            }

            if (!/^\d{2,3}-?\d{7}$/.test(phone)) {
                errors.push("Phone should be in the format XX-XXXXXXXX or XXX-XXXXXXX.");
                document.getElementById("phoneError").innerText = "Phone should be in the format XX-XXXXXXXX or XXX-XXXXXXX.";
            } else {
                document.getElementById("phoneError").innerText = "";
            }

            if (!/\S+@\S+\.\S+/.test(email)) {
                errors.push("Invalid email format.");
                document.getElementById("emailError").innerText = "Invalid email format.";
            } else {
                document.getElementById("emailError").innerText = "";
            }

            var errorList = document.getElementById("errorList");
            errorList.innerHTML = "";

            if (errors.length > 0) {
                errors.forEach(function (error) {
                    var li = document.createElement("li");
                    li.textContent = error;
                    errorList.appendChild(li);
                });
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
