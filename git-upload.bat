@echo off
chcp 65001 >nul
cd /d "%~dp0"

echo === Загрузка BookStoreCS в GitHub ===
echo.

git --version >nul 2>&1
if errorlevel 1 (
    echo Git не установлен. Скачай: https://git-scm.com/download/win
    pause
    exit /b 1
)

if not exist ".git" (
    git init
    git branch -M main
    git remote add origin https://github.com/ArgentBen/progect.git
)

git add .
git status

echo.
echo Коммит...
git commit -m "Дем экзамен: BookStore C# WinForms + SQL Server"

echo.
echo Отправка на GitHub...
git push -u origin main

if errorlevel 1 (
    echo.
    echo Если ошибка авторизации:
    echo 1. Установи GitHub Desktop: https://desktop.github.com
    echo 2. Войди в аккаунт ArgentBen
    echo 3. File - Add local repository - выбери эту папку
    echo 4. Publish repository / Push origin
)

echo.
pause
