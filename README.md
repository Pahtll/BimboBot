# BimboBot

## Телеграмм бот на C# с использованием библиотеки Telegram.Bot

Говоря про саму библиотеку, она хоть и не супер-новая и модная, но является самой часто-используемой и проверенной временем.

Очевидно, выбрать условную, более модный и новый фреймворк https://github.com/SKitLs-dev/SKitLs.Bots.Telegram было бы круто, но позже. 
Желания разбираться в нём не особо много, хоть он и прячет много кода себе под капот, делая работу с API telegram более приятной и декларативной.

Вот ещё один фреймворк, который тоже облегчает работу с API tg https://github.com/MajMcCloud/TelegramBotFramework?tab=readme-ov-file#quick-start 
Лично не первое, ни второе не проверял, но слышал, как минимум. 

Основная суть этих фреймворках, отойти от бесконечных `if {} / else {} // switch { case: default: }` конструкций при оброботке сообщений / событий.

### По функционалу

По функционалу могу сказать, что реализован базовый эхо-бот без заморочек, отправляет, какой-то рандомный стикер при запросе, реализовано включение инлайн и реплай клавиатур.
ТЗ или идей, что именно делать нет, потому оставил так.
