# TmxTaskUpdater
Console program to rule them all!

## Basic configuration
1. In order to run the program please:
    a) update `sharedQueryId` (MainClass) - open query, copy its id from url
    b) in `TokenGenerator` update `password` (your Azure DevOps token) and `username`
    c) update URLs in `IAzureDevOpsApi` - you should be good to go

2. For your sake, before you run the code comment out lines 34-38 in `MainClass`. Just do a test run. Debug it and see if the items are same as the one when you run the query. If it is fine, uncomment the code and voilá. Check AzureDevOps
