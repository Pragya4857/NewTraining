using System;
using System.Web.UI;

namespace Assignment_01
{
    public partial class Result : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get query parameters from the URL
                string name = Request.QueryString["name"];
                string familyName = Request.QueryString["familyName"];
                string address = Request.QueryString["address"];
                string city = Request.QueryString["city"];
                string zipCode = Request.QueryString["zipCode"];
                string phone = Request.QueryString["phone"];
                string email = Request.QueryString["email"];

                // Display details in HTML elements
                nameSpan.InnerText = name;
                familyNameSpan.InnerText = familyName;
                addressSpan.InnerText = address;
                citySpan.InnerText = city;
                zipCodeSpan.InnerText = zipCode;
                phoneSpan.InnerText = phone;
                emailSpan.InnerText = email;
            }
        }
    }
}
