# Shopping Cart System in C#
> Suzanne Antonette F. Gurion | BSIT 1-1
## Project Description
This project is a **console-based** Shopping Cart System developed using C#. It **allows** users to select products, enter quantities, validate inputs, and manage a shopping cart while tracking the remaining stock of each item.

The system **ensures** that users cannot input invalid data, **prevents** duplicate cart entries by updating quantities instead, and **avoids** purchasing beyond available stock. Additionally, it computes the total cost of items in the cart, applies a discount when applicable, and generates a summary of the transaction.

This project **demonstrates** the application of fundamental programming concepts such as object-oriented programming, arrays of objects, loops, conditional statements, and input validation.

## Meaningful Commits
	The development of this project is reflected through the following meaningful commits, showing step-by-step progress:
	1. Initial Commit: Set up the project structure and created basic classes for Product and CartItem.
	2. "doing the product thing": Implemented a method to initialize the product list with predefined items and their stock.
	3. "doing the menu commands!": Added functionality to handle user input for product selection and quantity, including basic validation.
	4. "fixes (the validation for the Y/N is wrong..)": Improved input validation to ensure only valid product numbers and quantities are accepted.
	
## **AI Usage**: 
AI tools (ChatGPT) were used only for guidance, debugging, and explanation, in accordance with academic integrity policies. The following outlines how AI assisted in the development of this project:

### 🔹 Prompts / Questions Asked
	“How to fix validation for Y/N input in C#?”
	“How to implement input validation using TryParse?”
	“What is the difference between fields and properties in C#?”
	“How to use { get; set; } in classes?”
	“How to commit and push changes in Git?”
	“How to debug issues in my shopping cart system?”

### 🔹 Parts Where AI Helped
	-Debugging input validation, specifically restricting user input to Y or N only
	-Explaining and improving the use of fields and properties using { get; set; }
	-Assisting in understanding and applying object-oriented programming concepts
	-Helping identify and fix logical errors in the program (e.g., validation and flow issues)
	-Providing guidance on Git commands such as commit and push

### 🔹 Changes Made After Using AI
	-Implemented a reusable method (GetYesOrNo) to strictly validate Y/N inputs
	-Improved class structure by converting fields into properties with { get; set; }
	-Refined validation logic to prevent invalid inputs and incorrect transactions
	-Cleaned and organized the code for better readability and structure
	-Verified and tested all AI-suggested solutions before applying them


[placeholder]
[placeholder]