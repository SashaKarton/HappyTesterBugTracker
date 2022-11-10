## "Happy Tester" Bug tracker
Happy Tester is a bug/issue tracker that will help you to track your projects, tickets, issues and bugs that are related to these projects.
## Motivation
This website is my first .NET project. It is developing to learn ASP.NET MVC framework and try new skills and technologies i've learned.
## Screenshots
![Login](https://user-images.githubusercontent.com/101929575/200970528-0c500125-338c-4e0e-8181-01c42356ed1c.jpg)
![Projects](https://user-images.githubusercontent.com/101929575/200970607-0150116a-196f-4822-8188-f5c2e9f69b78.jpg)
## Technologies used
•	ASP.NET Core MVC framework
•	C# for back-end
•	Bootstrap, HTML/CSS for frontend
•	MSSQL for database
•	Entity Framework Core
•	Identity Core 
•	‘Send Grid” for mail-service(password recovery)
•	“Cloudinary” to store user photos 
## Features
Website have Authentication and authorization functionality. New visitors can sign up and get user role. Old users can sign in or restore password if they forget it.

At the moment project have 2 roles user (all who create new accounts become users) and admin (user can be promoted to admin)
Users have permission to create projects and tickets and update their info, statuses and priority. Users can see only projects and tickets that they created or assigned to. Also users can change their password and update their profile info and photo.

In its turn admin have extra permission to delete projects tickets, update info and delete users as well as adding users to tickets and projects. Also admin can see all projects and tickets that are existing in db. 
## Tests
At the moment project have few controller and repository Unit tests, still working on them.
Tests were developed using xUnit.
