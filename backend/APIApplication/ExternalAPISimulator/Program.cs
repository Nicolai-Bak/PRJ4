﻿using ExternalAPIComponent;
using ExternalAPIComponent.Callers.Salling;


// Configure logger for start up
//BackendLogger.BuildLogger();

try
{
    SallingProductCaller caller = new();

    var result = await caller.Call(new SallingRequestBuilder().Build());

    result.ForEach(obj => Console.WriteLine(obj.ToString()));

    Console.WriteLine("Hit ENTER to exit...");
    Console.ReadLine();
}
catch (Exception e)
{
    //Log.Fatal(e, "Application failed to start");
}
finally
{
    //Log.CloseAndFlush();
}