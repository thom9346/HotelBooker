﻿namespace SharedModels
{
    public class CustomerDTO
    {
        public int CustomerId;

        public string Name;

        public string PhoneNr;

        public string Email;

        public int Age;

        //notice that we don't transfer balance... maybe
        public int Balance;
    }
}