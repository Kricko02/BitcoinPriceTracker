# Bitcoin Price Tracker

## Overview

The **Bitcoin Price Tracker** is simple a .NET application designed to track the price of Bitcoin and store it in an SQLite database. The application has several core features:

- **Real-time Bitcoin Price Tracking**: Fetches the latest Bitcoin price from a public API.
- **Automatic Price Storage**: A background service runs every hour to save the current Bitcoin price to an SQLite database.
- **Historical Price Averages**: Users can request the average Bitcoin price for a specific date.
- **Seeder**: If the database is empty, a seeder populates the database with random Bitcoin prices for the last 14 days.
- **Swagger/OpenAPI Integration**: Provides API documentation and testing interface.

## Technologies Used

- **Entity Framework Core**: For database access and ORM.
- **SQLite**: Used as the database to store Bitcoin price data.
- **HttpClient**: For fetching Bitcoin prices from the external API.
- **Background Services**: For periodic fetching and storage of Bitcoin prices.
- **Swagger**: For API documentation and testing.
