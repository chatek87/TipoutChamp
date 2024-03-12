using TipoutChamp;

namespace UnitTests
{
    public class TipoutCalculatorUnitTests
    {
        private void PopulateInputModel(InputModel inputModel)
        {
            inputModel.Employees.Add(new EmployeeEntry { Name = "Bartender 1", Role = Roles.Bartender, HoursWorked = 7, Sales = 1000, ChargedTips = 200 });
            inputModel.Employees.Add(new EmployeeEntry { Name = "Bartender 2", Role = Roles.Bartender, HoursWorked = 6, Sales = 1000, ChargedTips = 200 });
            inputModel.Employees.Add(new EmployeeEntry { Name = "Server 1", Role = Roles.Server, ChargedTips = 225, CashPayments = 220, Sales = 1500 });
            inputModel.Employees.Add(new EmployeeEntry { Name = "Server 2", Role = Roles.Server, ChargedTips = 356, Sales = 1800 });
            inputModel.Employees.Add(new EmployeeEntry { Name = "Server 3", Role = Roles.Server, ChargedTips = 300, Sales = 1500, CashPayments = 27 });
            inputModel.Employees.Add(new EmployeeEntry { Name = "Support 1", Role = Roles.Support, HoursWorked = 5 });
            inputModel.Employees.Add(new EmployeeEntry { Name = "Support 2", Role = Roles.Support, HoursWorked = 3 });
            inputModel.Employees.Add(new EmployeeEntry { Name = "CellarEvent 1", Role = Roles.CellarEvent, Sales = 2000 });
            inputModel.Employees.Add(new EmployeeEntry { Name = "CellarEvent 2", Role = Roles.CellarEvent, Sales = 2000, ToBar = 2 });
        }

        [Fact]
        public void Constructor_InitializesWithValidInputModel()
        {
            // Arrange
            var inputModel = new InputModel();

            PopulateInputModel(inputModel);

            // Act
            var exception = Record.Exception(() => new TipoutCalculator(inputModel));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void TotalBarHours_CalculatesCorrectly()
        {
            // Arrange
            var inputModel = new InputModel();            
            
            PopulateInputModel(inputModel);

            var calculator = new TipoutCalculator(inputModel);
            
            // Act
            var expectedTotalBarHours = 0M;

            var totalBarHours = calculator.TotalBarHours;

            foreach (var emp in inputModel.Employees)
            {
                if (emp.Role == Roles.Bartender)
                {
                    expectedTotalBarHours += emp.HoursWorked;
                }
            }

            // Assert
            Assert.Equal(expectedTotalBarHours, totalBarHours);
        }



    }
}