# About
**ServerList** is a .NET application that was built to complete the Proton coding exercise.

# ServerList

The purpose of this app is to retrieve basic location information and list of servers
from a RESTful API and present that information in the window. Data is downloaded
automatically at app start and refreshed on request by the user. The app is able to
fall back on a cache in case the API is unavailable. T

## Planning
To assist with planning the task, I setup a [GitHub Project board](https://github.com/users/grahamrgriffiths/projects/2). Usually, I would use Trello - but I've been meaning to try [projects](https://docs.github.com/en/issues/planning-and-tracking-with-projects/learning-about-projects/about-projects)

## Expanding the application
An intern has left behind this piece of code. Refactor it as much as possible into clean 
code according to best practices and fix the bugs and missing functionalities:
- The Current location displays nothing.
- The app occasionally crashes when scrolling servers down or refreshing.
- The servers should be listed by the distance from the current location in an ascending 
order.
- The window layout becomes ugly when resizing.

Feel free to switch to .NET Core or use library/framework that can make your life easier.

Among other things, we value:
- Beautiful high-quality code.
- Usage of MVVM and dependency injection pattern.
- Usage of async APIs where available, non-blocking UI.
- Implemented logging.
- Unit tests.

### Architecture
I chose the approach of updating the application to use [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui) and .NET 6

The MVVM pattern has been implemented, along with using services for each view model.

Dependency injection and core logging concepts have also been introudced.

Async APIs have been used where available, to support a non-blocking UI.

Unit tests have been implemented for the service logic only.

## Running the application
To run the application locally, ensure you are in the directory 'CodingTest'

First, restore the dependencies.
```
dotnet restore
```

Second, build the project,
```
dotnet build
```

Finally, run the project.
```
dotnet run
```

## Testing the application
Partial test coverage has been introduced for the Provider, Client, Parsing and CSV functionality.
To test the application locally, ensure you are in the directory 'CodingTest'

Run the tests
```
dotnet test
```

Generate a test report (with coverage)
```
dotnet test --settings settings/coverlet-run.xml --logger trx --results-directory "reports"
```

## CI 
A CI pipeline has been implemented for this task using Github Actions.

The pipeline has the following workflows:
- ServerList CI
    - Builds the application.
    - Tests the application. 
        - Generates a report, with coverage.
    - Runs OWASP dependency check.
    - Runs Code QL analysis.


## Author
[Graham Griffiths](https://github.com/grahamrgriffiths)
