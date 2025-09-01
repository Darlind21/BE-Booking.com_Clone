# BookingClone.API - Enterprise-Grade Booking Platform Backend

A sophisticated .NET 8 backend implementation of a Booking.com-inspired platform, architected with enterprise patterns and modern distributed system principles. This project demonstrates advanced backend development practices through clean architecture and robust scalability patterns.

## 🏗️ Architecture & Design Philosophy

### Clean Architecture Foundation

This project implements Clean Architecture principles, creating a maintainable and testable codebase that remains independent of external frameworks and infrastructure concerns. The architecture enforces strict dependency inversion, ensuring that business logic remains at the core while external concerns (database, web framework, third-party services) exist at the outer layers.

**Why Clean Architecture?**
- **Business Logic Protection**: Core business rules are isolated from infrastructure changes
- **Testability**: Each layer can be tested independently without external dependencies
- **Framework Independence**: Easy migration between different web frameworks or databases
- **Maintainability**: Clear separation of concerns makes code easier to understand and modify

### CQRS with MediatR Implementation

The Command Query Responsibility Segregation (CQRS) pattern is implemented using MediatR, creating a clear distinction between operations that modify state (Commands) and those that retrieve data (Queries).

**Strategic Benefits:**
- **Scalability**: Read and write operations can be optimized and scaled independently
- **Complexity Management**: Complex business operations are broken down into focused, single-responsibility handlers
- **Performance Optimization**: Queries can be optimized without affecting command-side complexity
- **Maintainability**: Each operation has a dedicated handler, making the codebase easier to navigate and modify

**Implementation Decision Rationale:**
The choice to use MediatR over direct dependency injection was made to eliminate controller bloat and create a consistent request/response pipeline. This approach also enabled the implementation of cross-cutting concerns (validation, logging, caching) through pipeline behaviors without cluttering business logic.

## 🚀 Key Features & Technical Highlights

### Authentication & Authorization System
- **JWT-based authentication** with role-based access control (User/Owner roles)
- **Dual registration flow** for owners: existing users can upgrade to owner status, or new users can register directly as owners
- **Secure password handling** with proper hashing and validation

### Advanced Validation Pipeline
Implemented a **Pipeline Behavior** using FluentValidation that automatically validates all commands and queries before they reach their handlers. This approach ensures:
- Consistent validation across all operations
- Separation of validation logic from business logic
- Automatic error responses for invalid requests
- Reduced boilerplate code in handlers

### Robust Error Handling Strategy
Developed a comprehensive error handling approach using the **Results Pattern** with FluentResults package:
- **Business Logic Failures**: Handled gracefully by Results Pattern to ensure business logic integrity.
- **System Exceptions**: Reserved for actual application-breaking scenarios
- **Global Error Middleware**: Ensures consistent error responses across the API

*Design Rationale*: This dual approach prevents business rule violations from being treated as exceptions while still maintaining system stability for genuine error conditions.

### Generic Repository Pattern
Implemented a generic repository with base CRUD operations that all specific repositories inherit, following DRY principles while maintaining type safety and enabling easy unit testing through interface abstractions.

### Advanced Booking System Features

#### Multi-Entity Photo Management
- **Cloudinary Integration**: Seamless photo upload and management for apartment listings
- **Batch Operations**: Support for multiple photo uploads and deletions

#### Sophisticated Data Querying
- **Dynamic Filtering**: Multi-criteria apartment search with location, price range, and amenities
- **Flexible Sorting**: Multiple sorting options with direction control
- **Pagination**: Pagination for improved performance at scale

#### Comprehensive Booking Workflow
- **State Management**: Complete booking lifecycle from creation to completion
- **Business Rule Enforcement**: Validation for booking conflicts, availability, and authorization
- **Review System**: Post-booking review functionality with proper authorization checks

### Asynchronous Processing Architecture

#### Background Job Processing with Hangfire
Implemented Hangfire for reliable background job processing with two key patterns:

**Fire-and-Forget Jobs with Outbox Pattern:**
- Email confirmations are processed asynchronously to avoid blocking API responses
- Outbox pattern ensures reliable message delivery even if immediate processing fails

**Recurring Jobs:**
- Automated booking status updates every 5 minutes for expired bookings
- Retry mechanism for failed outbox messages (up to 5 attempts)

*Technical Challenge Overcome*: The main challenge was ensuring email delivery reliability without impacting API performance. The outbox pattern solved this by decoupling the booking confirmation from email sending while maintaining eventual consistency.

### Real-Time Communication System

#### SignalR Integration
Developed a comprehensive real-time notification system:
- **Generic Notification Events**: Flexible event system for different notification types
- **Connection Management**: Proper handling of client connections and disconnections
- **Offline Support**: Notifications are persisted for offline users and delivered upon reconnection
- **Scalable Architecture**: Event-driven design allows for easy extension of notification types

**Implementation Highlight**: Created a separate console application for SignalR testing, demonstrating thorough testing practices and real-world usage scenarios.

### Event-Driven Architecture Transformation
Refactored the codebase to implement event-driven patterns, improving:
- **Loose Coupling**: Components communicate through events rather than direct dependencies
- **Extensibility**: New features can subscribe to existing events without modifying core logic
- **Auditing**: All significant business events are automatically tracked

### Distributed Logging with Apache Kafka

#### Error Logging Microservice Architecture
Implemented a sophisticated logging system using Kafka:

**Components:**
- **Main API**: Produces error events to Kafka when exceptions occur
- **Worker Service**: Dedicated service for consuming and processing error events
- **Shared Library**: Common models and configurations across services

**Benefits Achieved:**
- **Non-blocking Error Logging**: Main API performance unaffected by logging operations
- **Centralized Error Management**: All errors processed by dedicated service
- **Scalability**: Can easily add more consumer instances for high-volume scenarios
- **Reliability**: Kafka's persistence ensures no error logs are lost

*Architectural Decision*: This approach was chosen over traditional logging frameworks to demonstrate understanding of distributed systems and message queues, while providing a foundation for future microservices expansion.

## 🛠️ Technology Stack

### Core Framework
- **.NET 8**: Latest LTS version with performance improvements
- **Entity Framework Core**: Code-first approach with proper migrations
- **PostgreSQL**: Robust, ACID-compliant database for data consistency

### Architecture Patterns
- **MediatR**: CQRS implementation and cross-cutting concerns
- **FluentValidation**: Declarative validation rules
- **FluentResults**: Functional error handling approach

### External Integrations
- **Cloudinary**: Media management and optimization
- **SMTP Email Service**: Reliable email delivery
- **SignalR**: Real-time web functionality
- **Apache Kafka**: Message streaming and event processing
- **Hangfire**: Background job processing

### Development Tools
- **Postman**: Comprehensive API testing and documentation
- **Docker**: Containerized application with multi-service orchestration

## 📋 API Endpoints Overview

### Account Management
- User registration and authentication
- JWT token management
- Profile management and updates
- Role-based access control

### Property Management
- Apartment listing creation and management
- Photo upload and management via Cloudinary
- Property details and search functionality

### Booking System
- Comprehensive booking lifecycle management
- Real-time availability checking
- Status tracking and automated updates

### Review & Rating System
- Post-booking review submission
- Rating aggregation and display
- Review management for property owners

### Notification System
- Real-time notifications via SignalR
- Persistent notification storage
- Notification state management

## 🧪 Testing & Quality Assurance

### Validation Coverage
Every endpoint includes comprehensive validation:
- **Input Validation**: All request parameters validated using FluentValidation
- **Business Rule Validation**: Domain-specific rules enforced at the business layer
- **Authorization Validation**: Role and ownership-based access control

### Testing Strategy
- **Postman Collections**: Comprehensive API testing using Postman
- **SignalR Console Client**: Real-time functionality testing
- **Error Scenario Testing**: Validation of error handling and recovery mechanisms

## 🔄 Business Logic Integrity

The entire application maintains strict adherence to business rules:
- **Data Consistency**: Proper transaction management ensures data integrity
- **State Validation**: All state transitions validated according to business requirements
- **Authorization Enforcement**: Multi-layer authorization prevents unauthorized access
- **Error Recovery**: Graceful handling of all failure scenarios

## 🚀 Performance & Scalability Features

- **Asynchronous Processing**: Non-blocking operations for improved throughput
- **Connection Pooling**: Optimized database connections
- **Background Processing**: CPU-intensive operations moved to background jobs
- **Event-Driven Patterns**: Loose coupling enables horizontal scaling

---

This project demonstrates enterprise-level backend development practices, showcasing the ability to build scalable, maintainable, and robust systems that can handle real-world complexity while maintaining clean code principles and architectural best practices.
