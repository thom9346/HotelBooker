﻿using CustomerApi.Data;
using CustomerApi.Models;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using SharedModels;
using SharedModels.Booking.Messages;
using SharedModels.Customer.Messages;
using SharedModels.HotelRoom.Messages;
using SharedModels.Payment.Messages;

namespace CustomerApi.Infrastructure
{
    public class MessageListener
    {
        IServiceProvider provider;
        string connectionString;
        IBus bus;

        // The service provider is passed as a parameter, because the class needs
        // access to the product repository. With the service provider, we can create
        // a service scope that can provide an instance of the product repository.
        public MessageListener(IServiceProvider provider, string connectionString)
        {
            this.provider = provider;
            this.connectionString = connectionString;
        }

        public void Start()
        {
            //for some reason im still getting the "A task was cancelled" error.
            //Introduced a thread.sleep for now, but we should prob just copy/paste the retry policy
            //I didnt do it yet, because I am clueless why its needed right now
            Thread.Sleep(10000);
            using (bus = RabbitHutch.CreateBus(connectionString))
            {
                bus.PubSub.Subscribe<HotelRoomValidMessage>("HotelRoomValid", HandleHotelRoomValid);
                bus.PubSub.Subscribe<PaymentCreatedMessage>("PaymentCreated", HandlePaymentCreated);
                bus.PubSub.Subscribe<RefundCustomerMessage>("RefundCustomer", HandleRefundCustomer);
                lock (this)
                {
                    Monitor.Wait(this);
                }
            }

        }

        private void HandleHotelRoomValid(HotelRoomValidMessage message)
        {
            Console.WriteLine("Customer found a message" + message);
            // A service scope is created to get an instance of the product repository.
            // When the service scope is disposed, the product repository instance will
            // also be disposed.
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var CustomerRepos = services.GetService<IRepository<Customer>>();


                if (IsCustomerValid(message))
                {
                    var replyMessage = new BookingAcceptedMessage
                    {
                        CustomerId = message.CustomerId,
                        BookingId = message.BookingId,
                        BookingCost = message.BookingCost
                    };

                    bus.PubSub.Publish(replyMessage);
                }
                else
                {
                    // Publish an OrderRejectedMessage
                    var replyMessage = new BookingRejectedMessage
                    {
                        BookingId = message.BookingId,
                        Reason = "Customer was not valid"
                    };

                    bus.PubSub.Publish(replyMessage);
                }
            }
        }
        private bool IsCustomerValid(HotelRoomValidMessage message)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var customerRepos = services.GetService<IRepository<Customer>>();

                var customer = customerRepos.Get(message.CustomerId);
                if (customer == null)
                {
                    Console.WriteLine("Customer not found");
                    return false;

                }
                else if (customer.Age <= 18 || customer.Age >= 100)
                {
                    Console.WriteLine("Customer out of age range");
                    return false;

                }
                else if (customer.Balance < message.BookingCost)
                {
                    Console.WriteLine("Customer not enough balance");
                    return false;
                }
                else
                {
                    customer.Balance -= message.BookingCost;
                    customerRepos.Update(customer);
                    return true;
                }
            }
        }

        public void HandlePaymentCreated(PaymentCreatedMessage message)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var CustomerRepos = services.GetService<IRepository<Customer>>();
                Customer customerToEdit = CustomerRepos.Get(message.CustomerId);
                if (customerToEdit == null)
                {
                    Console.WriteLine("Payment Cancelled, customer not found");
                }
                else
                {
                    customerToEdit.Balance += message.amount;
                    CustomerRepos.Update(customerToEdit);
                }
            }
        }
        private void HandleRefundCustomer(RefundCustomerMessage message)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var customerRepos = services.GetService<IRepository<Customer>>();

                var customer = customerRepos.Get(message.CustomerId);
                if(customer != null) 
                {
                    customer.Balance += message.Amount;
                    customerRepos.Update(customer);
                }
            }
        }
    }
}
