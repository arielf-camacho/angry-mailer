# Angry mailer

### Introduction
Angry mailer is an application to be used for your own sake, to prevent your from yelling or rebuking another person,
when you write emails. It will let you provide the email address to which you want to write to, a subject and the
content itself. 

It will validate your email data is correct, and avoid letting you send emails to wrong address, or
miss either the content or the subject.

When your email is in good shape, it will allow you to send it. BUT, before allowing the "Send" button to function, it
will determine how your mood is. If you are angry, it will disallow you from sending the email and kindly ask you to
do it later when you're calmed. If you are always calmed while you write, then your emails will reach their destinations
in no time.

### Technologies and design decisions involved

The project employs Windows Presentation Foundation (*WPF*) to construct the whole UI. With it, rich interfaces can be written with ease and with a high degree of maintainability. With WPF the project is allowed to:
- Have very powerful validation techniques by binding Views with their view models.
- Bind the state in an easy way to manage the data changes, notifications and UI updates. Also used for the validation functioning.

For the tests I decided to go with MSTest as it's a very mature testing framework and to efficiently have and configure test doubles I've used [NSubstitute](https://nsubstitute.github.io/) which is the most elegant solution I have been using for the matter so far.

The project has a simple design for its classes roles. It's based on the MVVM pattern with a portion of design touches from the *Hexagon* architecture pattern (always making sure the domain layer or core is independent as the most strict rule). [MVVM](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel) is used because it plays very well with WPF, allowing clear separation of the UI layer from the underlying ones (domain, application, infrastructure or any other), letting the state flow nicely among the layers, and also providing the system's functions as commands that are intelligent enough to express whether they can be executed or not. Most WPF controls are aware of this command feature and they enable/disable themselves in consequence. The mentioned roles for the classes are:
- `SendMailView` (a *view*): The main (and only) window displaying the Email sending UI, connected to its view model to fuel it with the state and commands.
- `SendMailViewModel` (a *view model*): that exposes the data to be displayed by the view, and that by its changes it must update itself (WPF takes care of this with Data binding). Also exposes commands (the Send email command) which are the actions that the user can trigger on the view.
- `Email` (a *domain entity*): Domain entities are the representation of real-life objects of which a portion of their properties and functions are to be modeled by the application.
- `MailService` (a *domain service*): Domain services, classes that enforce the domain rules and that execute the actual logic that's involved in the domain (e.g. sending an email).

The design strongly goes under **SOLID** principle, which is one of the strongest guidelines that a maintainable software must follow. Enumerating some of the actual sub-principles used:
- *Single responsibility*: I made sure every single class does one thing and does it well.
- *Dependency inversion*: `Microsoft.Practices.Unity` was also included to manage the dependency injection technique that allows to reverse the dependency management.

### Requirements to run the project

In order to run it you need to:

* Have [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0) installed locally. You can test your installation by, on a terminal writing the command: `dotnet help` and checking that no errors appear, just the **dotnet**'s command help.
* Preferably have [Visual Studio](https://visualstudio.microsoft.com/downloads/) also installed.
* Clone the [project](https://github.com/arielf-camacho/angry-mailer) either from Visual itself or by issuing in your terminal: `git clone https://github.com/arielf-camacho/angry-mailer` on any directory of your choosing.
* If using Visual Studio, it will restore the packages automatically, otherwise with your terminal and standing inside the clone repository's directory issue the command: `dotnet restore`.

### Executing it

* If using Visual Studio, just hit **F5** and the application will pop up.
* If on the terminal, issue: `dotnet build`, and a *.exe* executable file called **AngryMailer.exe** in `.\bin\Debug\net5.0-windows\ref\AngryMailer.exe` will appear, ready to be double clicked.

### Executing the tests

The tests on the project were written using *MSTest* and they reside in the **AngryMailer.Tests** project. To run them:
* If using Visual Studio, just open the *Test Explorer* panel and click the most top left button called `Run All Tests In View`, you will get a nicely formatted summary below in the same panel.
* If on the terminal, standing on the solution folder, issue the command: `dotnet test` and the tests will execute. After the execution ends, the report will show up.

### Changelog

To keep track of the changes, additions and removals see the [Changelog](./CHANGELOG.md).

### Future enhancements

- There will be changed the Fake implementation of the email sending service for a real one.

### License

This product contains an MIT license, and it's intended for anyone to use it for any ends.