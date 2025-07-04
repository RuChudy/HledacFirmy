# HledacFirmy
Najde firmu dle IČO, doplní tyto informace do databáze z ARES.

* Domain-Driven Design
* C# Programming Language
* MS-SQL Server
* REST API

# Výhody
- Snadná údržba a rozšiřitelnost
- Jasná a srozumitelná implementace byznys logiky
- Snadný přístup k datům a operacím s nimi

![Screenshot](/Img/Preview.png)

# Architektura
Tento softwarový projekt využívá Domain-Driven Design (DDD) pro modelování domény a implementaci byznys logiky.
Databáze MS-SQL slouží k ukládání dat a REST API umožňuje přístup k datům a operacím s nimi.
Pro vývoj aplikace je použit jazyk C#.

## Doménová vrstva
Domain-Driven Design (DDD) vrstva je zodpovědná za reprezentaci a implementaci byznys logiky.
Je navržena dle principů DDD, s důrazem na srozumitelnost a udržitelnost.

## Datová vrstva
- MS-SQL
- REST API
 
Databáze MS-SQL slouží k ukládání dat.
REST API umožňuje přístup k datům a operacím s nimi.
API je navrženo tak, aby bylo snadno použitelné a flexibilní.

## Prezentační vrstva
Prezentační vrstva této aplikace je vyvinuta v Blazoru, což je open-source framework od Microsoftu,
který umožňuje vývoj **webových**, **desktopových** a **mobilních** aplikací s využitím C# a .NET.
Díky Blazoru je možné sdílet velkou část kódu mezi platformami, čímž se snižuje redundance
a zjednodušuje se vývoj a údržba aplikace.

### Hlavní vlastnosti Blazoru:
* _Jednotný kód_: Blazor umožňuje vývojářům psát kód v jazyce C#, který se pak kompiluje do WebAssembly
pro webové aplikace a do nativního kódu pro desktopové a mobilní platformy.
Díky tomu je možné sdílet velkou část kódu mezi platformami a šetřit tak čas a úsilí.
* _Interaktivní uživatelské rozhraní_: Blazor podporuje vykreslování na straně klienta i serveru,
čímž umožňuje vytvářet interaktivní a responzivní uživatelská rozhraní.
* _Bohatá sada komponent_: Blazor nabízí širokou škálu komponent pro běžné úkoly,
 jako je validace formulářů, navigace a práce s daty.
* _Snadné začlenění do stávajících projektů_: Blazor lze snadno integrovat do stávajících .NET projektů.
* _Sdílení kódu_: Díky Blazoru je možné sdílet velkou část kódu mezi webovou, desktopovou a mobilní verzí aplikace.
To snižuje redundanci a zjednodušuje vývoj a údržbu aplikace.
