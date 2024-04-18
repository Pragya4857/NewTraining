using System;
using System.Web.UI;

namespace Assignment_01
{
    public partial class Validator : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Redirect to result.html
                Response.Redirect("Result.aspx?" +
                    "name=" + Server.UrlEncode(nameInput.Text) +
                    "&familyName=" + Server.UrlEncode(familyNameInput.Text) +
                    "&address=" + Server.UrlEncode(addressInput.Text) +
                    "&city=" + Server.UrlEncode(cityInput.Text) +
                    "&zipCode=" + Server.UrlEncode(zipCodeInput.Text) +
                    "&phone=" + Server.UrlEncode(phoneInput.Text) +
                    "&email=" + Server.UrlEncode(emailInput.Text));
            }
        }
    }
}
