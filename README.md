**InfoTrack SEO Tracker - README**

**Project Overview**

**InfoTrack SEO Tracker** is a web application designed to track the search rankings of a website for specific keywords across multiple search engines (Google, Bing). The project consists of two parts:

1. **Backend**: A **.NET 9** API that handles search logic and data storage.
1. **Frontend**: A **React** application that provides an interface for interacting with the backend.

The backend uses **SQL Server Express** (or another SQL Server instance) for data storage.

-----
**Prerequisites**

To run the project locally, you will need the following tools installed:

1. **.NET SDK 9**
   Download and install the .NET SDK from [Microsoft's official site](https://dotnet.microsoft.com/download/dotnet).
1. **Node.js and npm**
   Node.js is required to run the React frontend. Install it from [here](https://nodejs.org/).
   npm (Node Package Manager) will be installed with Node.js, and you will use it to install frontend dependencies.
1. **SQL Server Express**
   Download and install [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads). You can use **SQL Server Management Studio (SSMS)** or a similar tool to manage the database.
-----
**Setup**

**Step 1: Configure the Backend**

1. **Open the project in Visual Studio**:
- Launch **Visual Studio**.
- Open the project folder for **InfoTrackSEOTracker**.
1. **Configure the SQL Server connection**:
- Open appsettings.json in the backend project.
- Set the **connection string** under the "ConnectionStrings" section:

"ConnectionStrings": {

`"DefaultConnection": "Server=localhost;Database=InfoTrackSEO;Trusted\_Connection=True;"

}

1. **Create the Database and Apply Migrations**:
   1. Open a terminal and navigate to the backend project folder.
   1. Run the following commands to create the database and apply the initial migration:

      dotnet ef migrations add InitialCreate --project InfoTrackSEOTracker.Infrastructure --startup-project InfoTrackSEOTracker.Api

      dotnet ef database update --project InfoTrackSEOTracker.Infrastructure --startup-project InfoTrackSEOTracker.Api

1. **Run the Backend**:
   1. Press **Ctrl + F5** in Visual Studio or run the following command to start the backend API:

dotnet run --project InfoTrackSEOTracker.Api

1. The backend API will be running on http://localhost:5000.
-----
**Step 2: Configure the Frontend (React)**

1. **Navigate to the frontend folder**:
   1. Open a terminal and go to the frontend directory in the project.
1. **Install the frontend dependencies**:
   1. Run the following command to install the required packages:

      npm install

1. **Run the React App**:
   1. After the dependencies are installed, run the following command to start the React app:

      npm start

1. The frontend will be available at http://localhost:3000.
-----
**Step 3: Testing the Application**

1. **Open the app**:
   1. Go to http://localhost:3000 in your web browser.

      ![image](https://github.com/user-attachments/assets/d0c8c777-93d7-4405-9519-8d890a748eac)

1. **Perform a search**:
   1. Enter a **Keyword** and **URL** in the form.
   2.  Choose a **Search Engine** (Google, Bing,).
   3.   Click **Search** to send a request to the backend API.
   4.    If the search is successful, the keyword ranking for the provided URL will be displayed.
    ![image](https://github.com/user-attachments/assets/67751324-b772-48ba-b2d6-d7aab4d844bf)
2. **History**
    ![image](https://github.com/user-attachments/assets/bb99160a-9f51-43f7-b5c2-eaacdc6741b1)


-----
**Useful Commands**

- **npm start** – Starts the frontend React app (runs on http://localhost:3000).
- **dotnet run** – Starts the backend API (runs on http://localhost:5000).
- **dotnet ef migrations add InitialCreate** – Creates a new database migration.
- **dotnet ef database update** – Applies the migrations to the database.
-----
**Troubleshooting**

- **Backend Not Running**:
  Ensure SQL Server Express is running and the connection string in appsettings.json is correctly configured.
- **Frontend Not Connecting to Backend**:
  Ensure the backend API is running on http://localhost:5000 and check for any CORS issues in the browser console.
-----
**Conclusion**

This README provides the steps to get the **InfoTrack SEO Tracker** running locally. The app consists of a **.NET 9** backend API, a **React** frontend, and **SQL Server Express** for data storage. By following the steps in this guide, you can test and use the application to track search rankings for specific keywords.

For further help, feel free to open an issue in the repository.

