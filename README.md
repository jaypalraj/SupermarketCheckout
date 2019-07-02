## Supermarket Checkout

### Steps to test the application:

* Build the solution
* Set CheckoutClient as the startup project
* Run the project
* It should display items to select from
* Add items to basket by entering the item number
* Enter quantity of items
* Enter the char 'N' when you wish to checkout
* It will print a customer receipt
* Further unit tests are there to play with basket items and quantities

### Solution layout
* There are 4 projects in the solution
* AppCore deals with the core stuff like 'Entities' and 'Services'
* Infrastructure simply contains fake data and fake repository
* The rest are CheckoutClient console application and UnitTests
