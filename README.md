# Table_Web_App

The file with an example of input data is named "file.txt," and the backup file of the database is named "AppTableDb.bak."

The web application implements all the specified functionalities, including filtering, sorting, inline editing of any row, and deleting rows. Validation is also implemented on the client-side (Input.cshtml) using windows on the page, as well as on the server-side in the CSVData class.

The following functionalities are present in this code:

1. "Upload" button - When clicked, this button sends the form to the server to upload the selected CSV file.

2. "Filter By" dropdown list - Users can select the field by which the data will be filtered.

3. "Filter Value" text field - Users enter the value by which the data will be filtered.

4. "Filter" button - When clicked, the filters entered by the user are applied to the data. Only rows that match the specified filters are displayed.

5. "Sort By" dropdown list - Users can select the field by which the data will be sorted.

6. "Sort Ascending" button - When clicked, the data is sorted in ascending order based on the selected field.

7. "Sort Descending" button - When clicked, the data is sorted in descending order based on the selected field.

8. "Reset All (For Sort and Filter)" button - When clicked, all applied filters and sorting are reset, removing any restrictions on the data.

9. "Delete All Records" button - When clicked, all records in the table are deleted.

Main page of the CSV Web App, route - CSV/Index:

![image](https://github.com/s7inner/Table_Web_App/assets/62800741/bc363373-8ce5-4321-93b6-66b960da8623)

Filtering by the "Date of Birth" field, you can enter 18 or 02 or 1983 in the "Filter Value" field, which represents one of the parts of the date or the first few characters of it:

![image](https://github.com/s7inner/Table_Web_App/assets/62800741/4079f1be-5d51-4a0e-89e4-64220e247e97)

Performing filtering and sorting simultaneously in ascending order based on the "Salary" field:

![image](https://github.com/s7inner/Table_Web_App/assets/62800741/d97215c6-2134-4c36-bd65-6c21fbe92072)
To perform in-line editing, you need to click on the value of the field. If filtering or sorting was applied beforehand, you need to click the respective button again to perform the action:

![image](https://github.com/s7inner/Table_Web_App/assets/62800741/5efd4be5-b3e9-4017-8751-17a6868ebb5b)

Filtering by last name and sorting by phone numbers in descending order:

![image](https://github.com/s7inner/Table_Web_App/assets/62800741/0c08ea0f-c31d-4a21-8580-495d600beff3)

Implementing client-side validation. A similar window appears for each field that will be modified incorrectly:

![image](https://github.com/s7inner/Table_Web_App/assets/62800741/1ccd17af-281c-472f-be5b-2127e0b68f84)

Server-side validation:

![image](https://github.com/s7inner/Table_Web_App/assets/62800741/70f33d98-236a-4a18-9cc8-5597ecbc0796)
