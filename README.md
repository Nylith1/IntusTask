# Rectangle SVG Drawing Webpage

This project allows users to draw, resize, and validate an SVG rectangle while displaying its perimeter. The system updates the dimensions of the rectangle in a JSON file, and upon resizing, it sends the updated dimensions to the backend for validation. If the rectangle's width exceeds its height, the backend will send an error after an artificially delayed validation of 10 seconds.

## Features

- Initial dimensions of the SVG figure are fetched from a JSON file.
- The user can resize the rectangle via mouse interactions.
- The perimeter of the rectangle is displayed next to the SVG figure.
- After resizing, the dimensions of the rectangle are updated in the JSON file.
- The backend validates the new dimensions and ensures that the width does not exceed the height.
- The backend validation takes 10 seconds to simulate long-lasting calculations.
- The user can continue resizing the rectangle while the backend validation is in progress.

## Requirements

- **Frontend**: React
- **Backend**: C# for handling the JSON and API interactions.

## Getting Started

### Frontend

1. Clone the repository to your local machine.
2. Navigate to the front-end project directory: **intus-task-frontend**
3. Run the following command to install the required dependencies:
   **npm install**
4. Create an .env file in the root directory of the project (if not already present) and add the following environment variable:
   **VITE_BASE_URL=https://localhost:7025/**
   Note: A .env_sample file is provided. You need to rename it to .env and replace https://localhost:7025/ with your actual backend URL if it differs.
6. To run the frontend development server, use:
   **npm run dev**

### Backend
1. Navigate to the IntusTask Api project folder.
2. Open the project in your preferred C# IDE (like Visual Studio or Visual Studio Code).
3. Build and run the backend project to ensure it’s up and running on https://localhost:7025/ or any other configured URL.
