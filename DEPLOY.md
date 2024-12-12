
# BeatStore Deployment Guide

This guide provides instructions to deploy the BeatStore application, along with details on how to access the system using pre-seeded user accounts.

---

## **Note on Environment Variables**
We do not use a .env file for environment variable management in this project to save time for the reviewers assessing the project. All required configuration variables are explicitly defined in the docker-compose.yml file, ensuring a seamless and straightforward deployment process without the need for additional setup.

This approach allows the application to be quickly deployed and tested without extra configuration steps.

## **Prerequisites**

Before deploying the application, ensure the following are available:
1. **Docker** and **Docker Compose** installed on your system.
2. **Environment Variables** configured for the database and application:
   - `SA_PASSWORD` for the SQL Server admin password.
   - `ConnectionStrings__DefaultConnection` for the application database connection string.
   You can find those variables in the docker-compose.yml file.

---

## **Deployment Steps**

### 1. Clone the Repository
Clone the BeatStore repository to your local environment:
```bash
git clone https://github.com/ktodorow/BeatStore_SoftUni
cd BeatStore_SoftUni
```

### 2. Build and Start the Application
Use Docker Compose to build and start the application:
```pwsh
docker compose up
```

### 3. Verify the Application
Once the containers are running:
- Access the application via your browser at:
  ```
  http://localhost:5000
  ```

### 4. Log in to the Application
The database is pre-seeded with two user accounts:
- **Admin Account**:
  - Username: `admin`
  - Email: `admin@example.com`
  - Password: `Admin@1234`
  - Role: `Admin`

- **User Account**:
  - Username: `user`
  - Email: `user@example.com`
  - Password: `User@1234`
  - Role: `User`

You can use these credentials to log in and test the application.

## **Common Issues and Troubleshooting**

### Application Not Accessible
- Ensure the Docker containers are running:
  ```pwsh
  docker ps
  ```
- Check logs for errors:
  ```pwsh
  docker logs beatstore-app
  docker logs beatstore-db
  ```

### Database Connection Issues
- Ensure `ConnectionStrings__DefaultConnection` in `docker-compose.yml` matches the database container name (`beatstore-db`).

### Reset Database
If needed, reset the database by removing the volume:
```pwsh
docker compose down -v
docker compose up
```

---
