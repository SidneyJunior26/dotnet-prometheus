# Simple .NET Application Demonstrating Prometheus Integration

This repository contains a simple .NET application designed to showcase the integration of Prometheus for monitoring and alerting. The application provides an example of how to expose metrics and configure Prometheus to scrape these metrics, providing valuable insights into the performance and health of the application.

## Features

* Exposes application metrics, including HTTP request duration and error counts.
* Configures Prometheus to scrape metrics from the .NET application.
* Demonstrates how to set up alerting rules in Prometheus for monitoring specific conditions.

# Getting Started
### Prerequisites
* .NET 7.0 SDK or later
* Docker (for running Prometheus)

## Installation
1. Clone the repository:
```
git clone https://github.com/yourusername/simple-dotnet-prometheus-demo.git
cd simple-dotnet-prometheus-demo
```

2. Build image of application:
```
docker build -t your-user/dotnet-website:latest .
```

3. Run the application and Prometheus with `docker-compose.yml`:
```
docker-compose up -d
```

## Configuration Files
* `Dockerfile`: Commands to create a Docker image for .NET 7 Application.
* `docker-compose.yml`: Configuration file for create and up Docker Images.
* `prometheus.yml`: Configuration file for Prometheus.

## Accessing Metrics
Once the application is running, metrics can be accessed at http://localhost:8080/metrics.
Access the Prometheus dashboard at http://localhost:9090 to analyze the metrics.
Prometheus will scrape this endpoint according to the configuration in `prometheus.yml`.
