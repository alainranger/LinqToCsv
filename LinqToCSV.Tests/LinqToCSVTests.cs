using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqToCSV;

using System;
using System.Collections.Generic;
using System.Text;

namespace LinqToCSV.Tests
{
    [TestClass()]
    public class LinqToCSVTests
    {
        const string expected_result_files = "expected_result_files";

        [TestMethod()]
        public void ToCsvTest_WithoutHeader()
        {
            var todoList = new ToDoItem[] 
            { 
                new ToDoItem{ Title = "Task1", Priority = Priority.UltraUrgent },
                new ToDoItem{ Title = "Task2", Priority = Priority.Urgent },
                new ToDoItem{ Title = "Task3", Priority = Priority.UltraUrgent },
            };

            var csv = todoList.ToCsv();

            var expectedResult = System.IO.File.ReadAllText(System.IO.Path.Combine(expected_result_files, "ToCsvTest_WithoutHeader.txt"));

            Assert.AreEqual(expectedResult, csv);
        }

        [TestMethod()]
        public void ToCsvTest_WithHeader()
        {
            var todoList = new ToDoItem[]
            {
                new ToDoItem{ Title = "Task1", Priority = Priority.UltraUrgent },
                new ToDoItem{ Title = "Task2", Priority = Priority.Urgent },
                new ToDoItem{ Title = "Task3", Priority = Priority.UltraUrgent },
            };

            var csv = todoList.ToCsv(true);

            var expectedResult = System.IO.File.ReadAllText(System.IO.Path.Combine(expected_result_files, "ToCsvTest_WithHeader.txt"));

            Assert.AreEqual(expectedResult, csv);
        }

        [TestMethod()]
        public void ToCsvTest_LinqToCSVHeaderAttribute()
        {
            var todoList = new ToDoItem2[]
            {
                new ToDoItem2{ Title = "Task1", Priority = Priority.UltraUrgent },
                new ToDoItem2{ Title = "Task2", Priority = Priority.Urgent },
                new ToDoItem2{ Title = "Task3", Priority = Priority.UltraUrgent },
            };

            var csv = todoList.ToCsv(true);

            var expectedResult = System.IO.File.ReadAllText(System.IO.Path.Combine(expected_result_files, "ToCsvTest_WithLinqToCSVHeaderAttribute.txt"));

            Assert.AreEqual(expectedResult, csv);
        }

        [TestMethod()]
        public void ToCsvTest_LinqToCSVHeaderAttribute_Sorted()
        {
            var todoList = new ToDoItem3[]
            {
                new ToDoItem3{ Title = "Task1", Priority = Priority.UltraUrgent, Description = "Description1", Rate = 1, Color = "Blue", AssignedTo = "User1", IsComplete = true},
                new ToDoItem3{ Title = "Task2", Priority = Priority.Urgent, Description = "Description2" , Rate = 10, Color = "Red", AssignedTo = "User3", IsComplete = false},
                new ToDoItem3{ Title = "Task3", Priority = Priority.UltraUrgent, Description = "Description3" , Rate = 5, Color = "Yellow", AssignedTo = "User2", IsComplete = true},
            };

            var csv = todoList.ToCsv(true);

            var expectedResult = System.IO.File.ReadAllText(System.IO.Path.Combine(expected_result_files, "ToCsvTest_WithLinqToCSVHeaderAttribute_Sorted.txt"));

            Assert.AreEqual(expectedResult, csv);
        }
    }

    class ToDoItem
    {
        public string? Title { get; set; }
     
        public Priority Priority { get; set; }
    }

    class ToDoItem2
    {
        [LinqToCSVHeader(HeaderName = "Titre", Order = 1)]
        public string? Title { get; set; }

        [LinqToCSVHeader(HeaderName = "Priorité", Order = 2)]
        public Priority Priority { get; set; }
    }

    class ToDoItem3
    {
        public string? AssignedTo { get; set; }

        [LinqToCSVHeader(HeaderName = "Titre", Order = 1)]
        public string? Title { get; set; }       

        [LinqToCSVHeader(HeaderName = "Priorité", Order = 3)]
        public Priority Priority { get; set; }


        [LinqToCSVHeader(HeaderName = "Description", Order = 2)]
        public string? Description { get; set; }

        public int Rate { get; set; }

        [LinqToCSVHeader(HeaderName = "Couleur", Order = 1)]
        public string? Color { get; set; }

        public bool IsComplete { get; set; }
    }

    enum Priority
    {
        UltraUrgent,
        Urgent
    }
}