# CI/CD Pipeline with GitHub Actions, Azure, and DigitalOcean

This project demonstrates the automation of Continuous Integration and Continuous Deployment (CI/CD) processes using **GitHub Actions** to deploy an **ASP.NET Core API** application to **Azure**, along with setting up and connecting to a **PostgreSQL database** hosted on **DigitalOcean**.

## 🚀 Key Features:
- **CI/CD Automation**: Automated build, test, and deployment process using **GitHub Actions**.
- **ASP.NET Core API**: The application is built using **ASP.NET Core** and exposed via RESTful APIs.
- **Azure Deployment**: The application is deployed to **Azure Web Apps** for production.
- **PostgreSQL on DigitalOcean**: A **PostgreSQL database** is set up on **DigitalOcean** and connected to the API for data management.

## ⚙️ Workflow Overview:

### 1. **ASP.NET Core API & Azure Deployment**:
   - The **CI/CD pipeline** is triggered by a push to the `master` branch on GitHub.
   - It automatically **builds** the **ASP.NET Core API**, **publishes** the output, and **deploys** it to an **Azure Web App** using GitHub Actions.
   - The pipeline uses the **`azure/webapps-deploy@v2`** action for deploying the API.

### 2. **Database Setup on DigitalOcean**:
   - A **PostgreSQL database** is created on **DigitalOcean** by provisioning a **Droplet**.
   - The database is configured and used by the **ASP.NET Core API** to manage and store data.
   - The application is connected to the database using a secure connection string.

### 3. **GitHub Actions Workflow**:
   - The pipeline is defined in `.github/workflows/ci-cd-pipeline.yml`.
   - It includes:
     - **Restore dependencies** (`dotnet restore`).
     - **Build** the **ASP.NET Core API** application (`dotnet build`).
     - **Publish** the application (`dotnet publish`).
     - **Deploy** to **Azure Web App**.
   - Environment variables like Azure publish profile and DigitalOcean credentials are stored securely in GitHub Secrets.

## 🛠️ Technology Stack:
- **GitHub Actions**: CI/CD automation.
- **ASP.NET Core API**: Backend API built using **ASP.NET Core**.
- **Azure Web Apps**: Hosting for the ASP.NET Core API.
- **PostgreSQL**: Relational database for managing application data.
- **DigitalOcean**: Cloud provider for hosting the PostgreSQL database.

## 🚀 Getting Started:

### Prerequisites:
- **GitHub Repository** containing your **ASP.NET Core API** project.
- **Azure Account** with a **Web App** created for hosting the API.
- **DigitalOcean Account** and a **PostgreSQL Droplet** provisioned.
- **GitHub Secrets** set up for Azure publish profile and DigitalOcean credentials.

### Setup Instructions:

1. **Create a PostgreSQL Database on DigitalOcean**:
   - Provision a Droplet with the PostgreSQL image on **DigitalOcean**.
   - Set up the PostgreSQL database instance, configure the username, password, and get the connection details (host, port, etc.).

2. **Configure GitHub Secrets**:
   - Add your **Azure publish profile** and **DigitalOcean credentials** as GitHub Secrets:
     - `AZURE_PUBLISH_PROFILE`
     - `DIGITALOCEAN_DB_HOST`
     - `DIGITALOCEAN_DB_USERNAME`
     - `DIGITALOCEAN_DB_PASSWORD`
     - `DIGITALOCEAN_DB_NAME`

3. **Configure GitHub Actions Workflow**:
   - Clone this repository and navigate to `.github/workflows/`.
   - The **CI/CD pipeline** YAML file (`ci-cd-pipeline.yml`) should already be set up to handle the deployment of the **ASP.NET Core API** and the connection to the PostgreSQL database.

4. **Push to GitHub**:
   - Push your code to the `master` branch. This will trigger the **CI/CD pipeline**, which will automatically deploy the **ASP.NET Core API** to **Azure** and set up the PostgreSQL database connection.

### Example `.github/workflows/ci-cd-pipeline.yml`:

```yaml
name: CI/CD Pipeline

on:
  push:
    branches:
      - master

jobs:
  deploy:
    runs-on: ubuntu-latest

    steps:
    - name: Checkout code
      uses: actions/checkout@v3

    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '9.0.x'

    - name: Restore dependencies
      run: dotnet restore ./YourProject.sln

    - name: Build application
      run: dotnet build ./YourProject.sln --configuration Release

    - name: Publish application
      run: dotnet publish ./YourProject.sln --configuration Release --output ./publish

    - name: Deploy to Azure
      uses: azure/webapps-deploy@v2
      with:
        app-name: your-azure-app-name
        publish-profile: ${{ secrets.AZURE_PUBLISH_PROFILE }}
        package: './publish'

    - name: Setup PostgreSQL database connection
      run: |
        echo "Setting up PostgreSQL database on DigitalOcean..."
        # Add commands to connect to DigitalOcean PostgreSQL and initialize schema
