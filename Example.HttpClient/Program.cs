// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Example.HttpClient.Model;
using Qdata.Contract;
using QData.LinqConverter;

namespace Example.HttpClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Example.Data.Contract.CrmModel;

    using QData.Client;
    using QData.LinqConverter.Extentions;

    /// <summary>
    ///     The program.
    /// </summary>
    internal class Program
    {
        #region Static Fields

        /// <summary>
        ///     The _access token.
        /// </summary>
        private static Uri contactAccsessPoint = new Uri("http://localhost:1878/api/customer/contacts");
        private static Uri customerAccsessPoint = new Uri("http://localhost:1878/api/customer/projection");
        

        #endregion

        #region Methods

        /// <summary>
        ///     The main.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                ContactsTest();
                Console.ReadLine();
            }
            
        }

        private static void ContactsTest()
        {
            Console.WriteLine("ContactsTest");
            var client = new QDataClient();

            var qSet = new QSet<ContactDto>();
            var query = qSet.QueryString("a", dto => new object[] { dto.EdvNr, dto.Customer.Firma11 }).Where(x => x.Id > 0);
            var descriptor = qSet.Serialize(query);

            var contacts =client.Get<ContactDto>(contactAccsessPoint, descriptor);
            if (contacts == null)
            {
                return;
            }
            foreach (var customer in contacts)
            {
                Console.WriteLine("id={0}", customer.Id);


            }
        }

        

        #endregion
    }
}