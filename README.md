#  Calculator Application

## 📌 Project Description
This project is a simple  calculator that performs basic arithmetic operations:
- Addition
- Subtraction
- Multiplication
- Division


##  Technologies Used
- C#
- .NET Console Application
- Visual Studio Code

---

##  Project Structure
Calculator │ ├── Program.cs ├── Calculator.cs ├── Calculator.csproj ├── README.md
Copy code

---

##  How the Program Works
1. The user enters two numbers.
2. The user selects an operation (Add, Subtract, Multiply, Divide).
3. The selected operation is stored using an **enum**.
4. A `Calculator` object is created using a **constructor**.
5. The calculation is performed using the `Calculate()` method.
6. The result is displayed in the console.
7. Errors such as division by zero are handled gracefully.

---

##  Key Concepts Used

###  Enum
An enum is used to define allowed calculator operations:
- Add
- Subtract
- Multiply
- Divide

This prevents invalid operator input and makes the code more readable.


###  Private Fields
The calculator stores values in private fields:
- `_num1`
- `_num2`
- `_operation`

These fields cannot be accessed directly from outside the class.


###  Constructor
The constructor initializes the calculator with required values when the object is created.  
This ensures the calculator always starts in a valid state.


###  Properties
Properties are used to provide controlled access to private fields using `get` and `set`.



##  Error Handling
- Division by zero is handled using `DivideByZeroException`
- Invalid operations are handled using `InvalidOperationException`



##  Future Improvements
- Add more operations
- Add input validation
- Allow multiple calculations in one run

---

##  Author
Tshepiso Mohlabane
