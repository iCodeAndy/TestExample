**Пример тестовой структуры для проекта с UI-тестами GreenMoney.**
---
#### [Тестовая среда](http://test.greenmoney.ru/)
---
## Используемые в проекте пакеты:
- [NUnit](http://nunit.org/)
- [NUnit3TestAdapter](https://github.com/nunit/docs/wiki/Visual-Studio-Test-Adapter)
- [Selenium.WebDriver](https://www.seleniumhq.org/)
- [Selenium.Suppport](https://www.seleniumhq.org/)
- [Selenium.WebDriver.ChromeDriver](https://github.com/jsakamoto/nupkg-selenium-webdriver-chromedriver/)
- [ExtentReports](https://github.com/extent-framework/)
---
## Структура проекта:
- Helpers
    - Settings.cs
    > Базовый класс с настройками
    - Utils.cs
    > Вспомогательные методы
- Pages
    - EditUserPage.cs
    - EnterNewPasswordPage.cs
    - HeaderPage.cs
    - MainPage.cs
    > POM-классы с описанием элементов страниц и методами взаимодействия
- Reports
    - dashboard.html
    > Общая информация о результатах UI-тестирования
    - index.html
    > Детальная информация о тестах
- ScreenShots
> Директория со скриншотами для тестов со статусом - Fail
- Tests
    - ChangeUserPassword
    > Набор UI-тестов для демонстрации
---
#### Директории Reports и ScreenShots создаются после первого запуска тестов.
---
#### На данный момент данные для тестов захардкожены.
#### В дальнейшем необходимо хранить данные в App.config и производить инициализацию необходимых переменных перед запуском тестов.
---
