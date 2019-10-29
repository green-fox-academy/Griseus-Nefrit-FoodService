using FoodService.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public EmailService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task SendMail(Order submittedOrder)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var msg = MessageAfterOrderSubmitted(submittedOrder);
           
            var response = await client.SendEmailAsync(msg);
        }

        public SendGridMessage MessageAfterOrderSubmitted(Order submittedOrder)
        {
            var messageAfterOrderSubmitted = new SendGridMessage()
            {
                From = new EmailAddress("admin@dotnetpincer.com", "DotnetPincer Team"),
                Subject = "New order was submitted to Restaurant: " + submittedOrder.Restaurant.Name,
                PlainTextContent = submittedOrder.Restaurant.Manager.Email,
                HtmlContent = CreateEmailBodyHtmlFromOrder(submittedOrder)
            };
            messageAfterOrderSubmitted.AddTo(new EmailAddress(submittedOrder.Restaurant.Manager.Email));
            return messageAfterOrderSubmitted;
        }

        public string CreateEmailBodyHtmlFromOrder(Order submittedOrder){
            string orderHtml = "<div><h4>" + submittedOrder.Restaurant.Name + "</h4></div>";
            orderHtml += "<table><thead><tr><th>Order ID</th><th>Items</th><th>Quantity</th><th>Date and time of the order</th></tr></thead><tbody>";
            orderHtml += "<tr><td>" + submittedOrder.OrderId + "</td><td>";
            for (int i = 0; i < submittedOrder.CartItems.Count; i++)
            {
                orderHtml += submittedOrder.CartItems[i].Meal.Name + "<br/>";
            }
            orderHtml += "</td><td>";
            for (int i = 0; i < submittedOrder.CartItems.Count; i++)
            {
                orderHtml += submittedOrder.CartItems[i].Quantity + "<br/>";
            }
            orderHtml += "</td><td>" + submittedOrder.DateSubmitted + "</td>";
            orderHtml += "</tr></tbody></table>";
            orderHtml += "<br/><br/>";
            orderHtml += "<table><tbody><tr><th>Name</th><td>" + submittedOrder.User.Email + "</td></tr>";
            orderHtml += "<tr><th>Country</th><td>" + submittedOrder.Address.Country + "</td></tr>";
            orderHtml += "<tr><th>City</th><td>" + submittedOrder.Address.City + "</td></tr>";
            orderHtml += "<tr><th>Zip Code</th><td>" + submittedOrder.Address.ZipCode + "</td></tr>";
            orderHtml += "<tr><th>Address</th><td>" + submittedOrder.Address.Street + "</td></tr></tbody></table>";
            orderHtml += "<br/><br/>";
            orderHtml += "You can check all your current open orders at the following link: ";
            orderHtml += "<a href=\"http://localhost:53967/Order/CurrentOrder\">Current orders</a>";
            return orderHtml;
        }
    }
}
