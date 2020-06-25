import sys
import math
import cmath
#python program that hosts multiple calculators for simple applications
def quad1(a, b, c):
    d = (b**2) - (4*a*c)
    x = (-b + math.sqrt(d))/(2*a)
    return x
def quad2(a, b, c):
    d = (b**2) - (4*a*c)
    x = (-b - math.sqrt(d))/(2*a)
    return x
number = 0
while number != "999":
    print("Enter 1 to calculate simple interest ")
    print("Enter 2 to calculate compound interest ")
    print("Enter 3 to use a simple calculator ")
    print("Enter 4 to use the pythagoreon theorum ")
    print("Enter 5 to find the distance between 2 points ")
    print("Enter 6 to use the quadratic formula ")
    print("Enter 999 to quit the program ")
    number = input()
    
    if number == "1":
        #simple interest
        principle = input("\nEnter the original amount: ")
        interestRate = input("\nEnter the interest rate as a decimal: ")
        time = input("\nEnter the length of time for interest to accrue: ")
        finalAmount = float(principle) * (1 + float(interestRate) * float(time))
        print("Your original amount of {0} will have increased to {1} over {2} years\n".format(principle, finalAmount, time))
   
    elif number == "2":
        #compound interest
        principle = input("\nEnter the original amount: ")
        interestRate = input("\nEnter the interest rate as a decimal: ")
        time = input("\nEnter the length of time for interest to accrue: ")
        compounded = input("\nEnter the number of times per year the interest is compounded: ")
        finalAmount = float(principle) * (1 + float(interestRate) / float(compounded))**(float(time)*float(compounded))
        print("Your original amount of {0} will have increased to {1} over {2} years\n".format(principle, finalAmount, time))
    
    elif number == "3":
        #simple calculator
        number1 = float(input("\nPlease enter the first number "))
        print("Now choose an operation ")
        print("add = +")
        print("subtract = -")
        print("multiply = *")
        print("divide = /")
        print("Exponent = ^")
        choice = input()
        if choice in ('+', '-', '*', '/' , '^'):
            if choice == "+":
                number2 = float(input("Please enter the second number "))
                answer = number1 + number2
                print("The sum of {0} and {1} is {2}".format(number1, number2, answer))
            if choice == "-":
                number2 = float(input("Please enter the second number "))
                answer = number1 - number2
                print("The difference of {0} and {1} is {2}".format(number1, number2, answer))
            if choice == "*":
                number2 = float(input("Please enter the second number "))
                answer = number1 * number2
                print("The product of {0} and {1} is {2}".format(number1, number2, answer))
            if choice == "/":
                number2 = float(input("Please enter the second number "))
                answer = number1 / number2
                print("The quotient of {0} and {1} is {2}".format(number1, number2, answer))
            if choice == "^":
                number2 = float(input("Please enter the second number "))
                answer = number1 ** number2
                print("The product of {0} to the {1} is {2}".format(number1, number2, answer))
        else:
            print("Operation is invalid")
        print()

    elif number == "4":
        #pythagoreon theorum
        a = float(input("\nEnter the length of the first side of the triangle "))
        b = float(input("Enter the length of the second side of the triangle "))
        c = math.sqrt((a**2) + (b**2))
        print("The hypotenuse of the triangle is {0}\n".format(c))

    elif number == "5":
        #distance formula
        x1 = float(input("\nEnter the length of the first x coordinate "))
        y1 = float(input("Enter the length of the first y coordinate "))
        x2 = float(input("Enter the length of the second x coordinate "))
        y2 = float(input("Enter the length of the second x coordinate "))
        d = math.sqrt(((x2 - x1)**2) + ((y2 - y1)**2))
        print("The distace between ({0}, {1}) and ({2}, {3}) is {4}\n".format(x1, y1, x2, y2, d))

    elif number == "6":
        #Quadratic Formula
        num1 = float(input("\nEnter the value of a "))
        num2 = float(input("Enter the value of b "))
        num3 = float(input("Enter the value of c "))
        positive = quad1(num1, num2, num3)
        negative = quad2(num1, num2, num3)
        print("The solutions are x = {0} and x = {1}\n".format(positive, negative))