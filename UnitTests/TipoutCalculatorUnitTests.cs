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
        public void TotalSupportTipout_IsEqualToSumOfAllSupportFinalPayouts()
        {
            // Arrange
            var inputModel = new InputModel();
            PopulateInputModel(inputModel);
            var calc = new TipoutCalculator(inputModel);
            
            // Act
            var finalPayouts = calc.Roster.Support.Sum(emp => emp.FinalPayout);

            // Assert
            Assert.True(calc.TotalSupportTipout == finalPayouts);
        }

        [Fact]
        public void Constructor_InitializesWithValidInputModel()
        {
            // Arrange
            var inputModel = new InputModel();

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

        [Fact]
        public void BarTipoutPercentage_IsSetCorrectlyBasedOnNumberOfSupport()
        {
            // Arrange
            var inputModel1 = new InputModel();
            inputModel1.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Bartender });
            
            var inputModel2 = new InputModel();
            inputModel2.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Bartender });
            inputModel2.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Support });

            var inputModel3 = new InputModel();
            inputModel3.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Bartender });
            inputModel3.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Support });
            inputModel3.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Support });
            inputModel3.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Support });


            // Act
            var calc1 = new TipoutCalculator(inputModel1);
            var calc2 = new TipoutCalculator(inputModel2);
            var calc3 = new TipoutCalculator(inputModel3);


            // Assert
            Assert.True(calc1.BarTipoutPercentage == 0.02M);
            Assert.True(calc2.BarTipoutPercentage == 0.02M);
            Assert.True(calc3.BarTipoutPercentage == 0.015M);
        }

        [Fact]
        public void SupportTipoutPercentage_IsSetCorrectlyBasedOnNumberOfSupport()
        {
            // Arrange
            // 0 support
            var inputModel0 = new InputModel();
            inputModel0.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Bartender});

            // 1 support
            var inputModel1 = new InputModel();
            inputModel1.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Bartender });
            inputModel1.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Support });

            // 2 support
            var inputModel2 = new InputModel();
            inputModel2.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Bartender });
            inputModel2.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Support });
            inputModel2.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Support });

            // 3 support
            var inputModel3 = new InputModel();
            inputModel3.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Bartender });
            inputModel3.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Support });
            inputModel3.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Support });
            inputModel3.Employees.Add(new EmployeeEntry { Name = "", Role = Roles.Support });

            // Act
            var calc0 = new TipoutCalculator(inputModel0);
            var calc1 = new TipoutCalculator(inputModel1);
            var calc2 = new TipoutCalculator(inputModel2);
            var calc3 = new TipoutCalculator(inputModel3);


            // Assert
            Assert.True(calc0.SupportTipoutPercentage == .00M);
            Assert.True(calc1.SupportTipoutPercentage == .01M);
            Assert.True(calc2.SupportTipoutPercentage == .02M);
            Assert.True(calc3.SupportTipoutPercentage == .03M);
        }

        [Fact]
        public void TotalSupportTipout_CalculatesCorrectly()
        {
            // Arrange
            var inputModel = new InputModel();
            PopulateInputModel(inputModel);

            // Act
            var calc = new TipoutCalculator(inputModel);
            var expectedSupportTipout = 0M;

            foreach (var emp in calc.Roster.Bartenders)
            {
                expectedSupportTipout += emp.TipoutToSupport;
            }

            foreach (var emp in calc.Roster.Servers)
            {
                expectedSupportTipout += emp.TipoutToSupport;
            }

            foreach (var emp in calc.Roster.CellarEvents)
            {
                expectedSupportTipout += emp.TipoutToSupport;
            }

            // Assert
            Assert.True(calc.TotalSupportTipout == expectedSupportTipout);
        }


    }
}