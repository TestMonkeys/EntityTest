Assert
======

NUnit Assert Extensions as well as a modified version of Assert, the main difference being the exception it throws.

TestMonkey.Assertion project is a copy of NUnit with small adjustments, and a Microsoft AssertionFailed exception,
thus working perfectly with MSTest

TestMonkey.Assertion.Extensions is a project for extending the functionality and the constraints available.
Functionality:
  - PropertySet validation - comparing two objects by their properties values, with the ability to change how the 
    validation is performed using specially created attributes
