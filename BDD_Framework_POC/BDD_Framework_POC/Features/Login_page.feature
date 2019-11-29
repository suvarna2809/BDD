Feature:  Mynthra login page functionality 

Background: 
	Given Navigate to Myntra application 'https://www.myntra.com'

@test1 @Regression @p0 @sanity
Scenario Outline: Verification of succesfull login of Myntra Application			
	When the user enter the <email> and <Password>
	And the user click on submit button
	Then the Myntra home page displayed
	Examples: 
		| URL                     | email                         | Password   |
		| https://www.myntra.com  | gadhamsetty.suvarna@gmail.com | Accion@123 |

@test2 @p1 @Regression
Scenario: Verification of error message when the user enter the email and click on login 		
	When the user enter the email id 'gadhamsetty.suvarna@gmail.com'
	And the user click on submit button without entering the password
	Then the error message is displayed as 'Please enter password'

@test3 @p1 @Regression
Scenario: Verification of error message when the user enter the password and click on login 			
	When the user enter the password 'Accion@123'
	And the user click on submit button without entering the email id
	Then the error message is displayed as 'Please Enter a Valid Email Id'

