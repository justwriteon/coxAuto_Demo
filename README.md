# coxAuto_Demo
Coding test for cox auto
This content also available under Notes in the application menu.

This application has been developed solely for the purpose of demonstrating software architecture for the given assignment.
Foreseeing future requirement, file uploading is decoupled from Vehicle data to facilitate user to upload a file that can be parsed and saved in a database.  Though as per the current requirement, only CSV file has to be uploaded and saved in file system, following implementations expected in future.
FILE_CSV/FILE_XML : as long as file to be saved to file system the implementation would be the same irrespective of file type
DB_CSV : Parse CSV file and save to DB
DB_XML : : Parse XML file and save to DB
For simplicity, logging and Error handling has been globalized using default logging options.
Omissions/Limitations:
Ideally, this application has to be implemented as RESTful API but keeping the scope the requirement this has been developed as monolithic application for   ease of deployment for code evaluation. Http 404 error is not handled.
Data uploaded most recently is one that serves as data for this application. Previous data file will be backed up and not made available for this application.
If data uploaded to database this should not be the case. Data uploaded will be appended to existing data.
Data file to be uploaded should conform to template provided.
