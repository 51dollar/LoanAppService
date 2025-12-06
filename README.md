# Full-Stack Application
Это full-stack приложение, состоящее из ASP.NET Backend и Frontend на Vite + Vue 3 (TypeScript).
Ниже приведены инструкции по запуску проекта как в Docker, так и локально.

## Запуск в Docker
В корне проекта выполните:
```bash
docker compose up --build
```
🌐 Frontend: http://localhost:5173/
📘 API Docs (Scalar): http://localhost:5000/scalar/v1

## Локальный запуск (Backend + Frontend)
Backend (.NET)
```bash
cd backend/LoanService
dotnet run
```
Frontend (Vite + Vue)
```bash
cd frontend
npm install
npm run dev
```
🌐 Frontend: http://localhost:5173/
📘 API Docs (Scalar): http://localhost:5000/scalar

## Технологии
### Backend:
1. ASP.NET
2. Scalar API Docs
3. Sql Server

### Frontend:
1. Vite
2. Vue (TypeScript)
3. npm
4. Tailwind

### DevOps
1. Docker file
2. Docker compose
3. Health