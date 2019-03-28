Feature: DevTests
	In order to check base framework want to make sure it is reliable enough.
@dev
Scenario: File and Folder Works
	Given I can get project execution directory
	Then The "logs.log" Exist Under Reprt > Logs
