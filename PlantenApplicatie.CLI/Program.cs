﻿using PlantenApplicatie.Data;
using PlantenApplicatie.Domain;
using System;
using System.Collections.Generic;

namespace PlantenApplicatie.CLI
{
    class Program
    {
        // Testomgeving om een plant te vinden
        private static PlantenDao _plantenDao;

        static void Main(string[] args)
        {
            _plantenDao = PlantenDao.Instance;

           // PrintPlanten(_plantenDao.SearchPlants("vivassen", "FABACEAE", "Baptisia", "australis", "", "faba"));
        }

        //private static void PrintPlanten(List<Plant> planten)
        //{
        //    foreach (var plant in planten)
        //    {
        //        Console.WriteLine($"{plant.Fgsv}");
        //    }
        //}
    }
}