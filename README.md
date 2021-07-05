# InterView_Test_CreditSuisse
C# WPF test

Your task is to extend the existing WPF application supplied using C# to display the current and average prices of 5 made up instruments. 

It should do the following:

Read the prices from the file provided (Sample Data.txt) in a continuous loop 10x a second and asynchronously update the GUI with the instrument name and current price. 

The entire file must be continuously read until the application is shut down, it should not be read just once. 

The GUI only needs to display a grid with 5 rows, 1 per instrument. 

The columns should be the instrument name, current price, and average over the last 5 prices. 

Double clicking on an instrument should launch another screen showing the history of the last 10 prices. 

The price display cells background colour should be green if increasing, red if decreasing and black if constant. 

The same goes for the cells displaying averages. 

All screens should be updating in real time. 

The solution should be zipped up (excluding binaries) and all source returned, this must be able to build in VS2017/19 using .NET >= 4.6.1. 