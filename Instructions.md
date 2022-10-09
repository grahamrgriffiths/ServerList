# The Windows .NET candidate coding test

## Time Limit - 3 Days
Please return your attempt of this task in 3 days from receiving it.

## Overview
This exercise is meant to test your coding skills as well as how you approach the app
development process in general. Your task is to take the provided sample app, get it
running and improve it based on the description in this document, as well as your own
expectation of code and app quality. How you choose to approach this task is up to you
and you should make your judgments about what's most important to fix or implement
first and take the best guess if unsure about something.

You should approach this app with the attitude that you and other developers may have
to expand and maintain it for an unknown number of years and will likely outlive you
working on it.

Since the time is limited, you are not expected to implement everything, so if you identify
something as taking a long time to do, just leave it.

If you identify some things you would like to do but run out of time for, feel free
to submit some notes along with your task submission.

## The app
The purpose of this app is to retrieve basic location information and list of servers
from a RESTful API and present that information in the window. Data is downloaded
automatically at app start and refreshed on request by the user. The app is able to
fall back on a cache in case the API is unavailable. The above already is in the source
code.

## The task
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
