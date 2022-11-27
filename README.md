# "Happy Tester" Bug tracker
Happy Tester is a bug/issue tracker that will help you to track your projects, tickets, issues and bugs that are related to these projects.
## Motivation
This website is my first .NET project. It is developing to learn ASP.NET MVC framework and try new skills and technologies i've learned.
## Screenshots
Project have login and register pages. After registration visitor get user role.
![Login](https://user-images.githubusercontent.com/101929575/200970528-0c500125-338c-4e0e-8181-01c42356ed1c.jpg)
![Register](https://user-images.githubusercontent.com/101929575/204113895-8a782aff-4ed5-4e22-9899-91246518a04a.jpg)

Next sceenshots was made with admin role.
Project page with CRUD functionality.
![Projects](https://user-images.githubusercontent.com/101929575/204113944-16b1b55d-5aff-4d6d-a898-e7e1c039a409.jpg)

Project creation page. User can create ticket while creating project(ticket will belong to created project).
![CreateProject](https://user-images.githubusercontent.com/101929575/204113985-916f17ba-d156-4e7f-8bbe-bb63ef47df16.jpg)

Tickets with CRUD
![Tickets](https://user-images.githubusercontent.com/101929575/204113991-986d8482-9cd7-48a7-94f2-590a26ab74dd.jpg)

Ticket creation page. User can add ticket to existing project.
![CreateTicket](https://user-images.githubusercontent.com/101929575/204114004-362a1cfd-b07c-412b-9344-cac8c0f268e4.jpg)

Users page with CRUD(CRUD is avaliable only for admin role, user role have access only to "Delails")
![Users](https://user-images.githubusercontent.com/101929575/204114022-563ddf5e-0071-445e-b906-424eec2f01d4.jpg)

Users profile page. Where user can edit personal data and photo. Also user can see projects and tickets he assigned to.
![Profile](https://user-images.githubusercontent.com/101929575/204114026-2aaf283d-6d35-4714-8b6f-e002a00d037f.jpg)

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
