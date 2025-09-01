# 📚 Rawy Platform

Rawy is an audiobook platform that combines content streaming with a hybrid recommendation system to enhance user engagement and discovery.  
It supports multiple user roles, real-time interactions, secure payments, and optimized backend performance.

---

## 🚀 Tech Stack
- **Backend:** ASP.NET Core, C#, SQL Server, Entity Framework
- **Authentication & Security:** JWT, Role-Based Access Control
- **Real-Time Communication:** SignalR
- **Caching & Performance:** Redis
- **Machine Learning Service:** Python Flask with KNN Collaborative Filtering
- **Payments:** Secure Payment Gateway Integration
- **Documentation:** Swagger / OpenAPI

---

## ✨ Key Features
- 🔎 **Hybrid Recommendation System**  
  Integrated a Python Flask microservice using KNN-based collaborative filtering to improve content discovery.  

- ⚡ **Performance Optimization**  
  Redis caching reduced API response times by **40%** for frequent requests with an hourly cache refresh cycle.  

- 🔐 **Secure APIs**  
  RESTful APIs with JWT authentication and role-based access control for **Admin, Content Creators, and Users**.  

- 🔔 **Real-Time Interactions**  
  SignalR for live notifications, user interactions, and instant review updates.  

- 💳 **Payment Processing**  
  Secure subscription management through payment gateway integration.  

- 📑 **Comprehensive API Documentation**  
  Built with Swagger/OpenAPI, reducing new developer onboarding time by **30%**.  

- 🗄️ **Optimized Database Performance**  
  Efficient query design and indexing strategies for SQL Server.  

---

## 📡 API Endpoints (Sample)
- `POST /api/auth/login` → User login  
- `POST /api/auth/register` → User registration  
- `GET /api/books` → Get all audiobooks  
- `GET /api/books/{id}` → Get audiobook details  
- `POST /api/reviews` → Add a review  
